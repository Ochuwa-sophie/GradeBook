using GradeBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace GradeBook.Pages
{
    public class GradeBookModel : PageModel
    {
        [BindProperty]
        public SchoolClass SchoolClass { get; set; }
        [BindProperty]
        public Rubric Rubric { get; set; }
        [BindProperty]
        public int NextClassID { get; set; }

        public List<Rubric> AllRubrics { get; set; }
        public List<SchoolClass> AllClasses { get; set; }

        private ApplicationDbContext DbContext;
        UserManager<ApplicationUser> UserManager;
        SignInManager<ApplicationUser> SignInManager;
        IDataProtector Protector;
        IDataProtectionProvider Provider;

        public GradeBookModel(ApplicationDbContext context, UserManager<ApplicationUser> manager,
            IDataProtectionProvider provider, SignInManager<ApplicationUser> signInMgr)
        {
            DbContext = context;
            UserManager = manager;
            SignInManager = signInMgr;
            Provider = provider;
        }

        public void OnGet(int classID = 0)
        {
            if (SignInManager.IsSignedIn(HttpContext.User))
            {
                if (classID != 0)
                {
                    NextClassID = classID;
                }

                GetModel();
            }
        }

        public IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Account", "/GradeBook");
        }

        void GetModel()
        {
            var teacherID = UserManager.GetUserId(HttpContext.User);
            Protector = Provider.CreateProtector("TeacherClass" + teacherID);

            AllRubrics = DbContext.Rubrics
                .Where(r => r.TeacherID == teacherID)
                .OrderBy(r => r.Name).ToList();

            AllClasses = DbContext.SchoolClasses
                        .Where(sc => sc.TeacherId == teacherID)
                        .OrderBy(sc => sc.Name).ToList();

            if (AllClasses.Any())
            {
                var classID = AllClasses.First().ID;
                if (NextClassID != 0)
                {
                    classID = NextClassID;
                }

                GetCurrentClass(classID);
            }
            else
            {
                SchoolClass = new SchoolClass
                {
                    TeacherId = teacherID,
                    Name = "New Class",
                    AssignmentSchoolClasses = new List<AssignmentSchoolClass>(),
                    Students = new List<Student>()
                };
                DbContext.Add(SchoolClass);
                DbContext.SaveChanges();
            }
        }

        void GetCurrentClass(int classID)
        {
            SchoolClass = DbContext.SchoolClasses
                .Include("AssignmentSchoolClasses.Assignment.Rubric.Assessments")
                .Include("Students.AssessmentGrades")
                .Single(c => c.ID == classID);

            if (SchoolClass != null)
            {
                SchoolClass.Students.ForEach(student =>
                {
                    try
                    {
                        student.UserName = Protector.Unprotect(student.UserName);
                    }
                    catch
                    {
                        student.UserName = "Encrypted";
                    }
                });
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SaveClass();

            return RedirectToPage("/Index", new { classID = NextClassID });
        }

        void SaveClass()
        {
            DbContext.Update(SchoolClass);

            DbContext.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GradeBook.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

namespace GradeBook.Controllers
{
    public class GradebookController : Controller
    {
        ApplicationDbContext DbContext;
        UserManager<ApplicationUser> UserManager;
        IDataProtector Protector;
        IDataProtectionProvider Provider;

        public GradebookController(ApplicationDbContext context, 
            UserManager<ApplicationUser> manager,
            IDataProtectionProvider provider)
        {
            DbContext = context;
            UserManager = manager;
            Provider = provider;
        }

        public IActionResult AddClass(string teacherID)
        {
            var newClass = new SchoolClass
            {
                Name = "New Class",
                TeacherId = teacherID
            };

            DbContext.Add(newClass);
            DbContext.SaveChanges();

            return RedirectToPage("/Index", new { classID = newClass.ID });
        }

        public IActionResult DeleteClass(int classID)
        {
            var schoolClass = DbContext.SchoolClasses
                .Single(c => c.ID == classID);
            DbContext.Remove(schoolClass);
            DbContext.SaveChanges();

            return RedirectToPage("/Index");
        }

        public IActionResult AddStudent(int classID)
        {
            var teacherID = UserManager.GetUserId(HttpContext.User);
            Protector = Provider.CreateProtector("TeacherClass" + teacherID);

            var schoolClass = DbContext.SchoolClasses
                .Include("Students")
                .Single(s => s.ID == classID);

            var newStudent = new Student
            {
                SchoolClassID = classID,
                UserName = Protector.Protect("New Student"),
                StudentClassNumber = schoolClass.Students.Count + 1
            };

            DbContext.Add(newStudent);
            DbContext.SaveChanges();

            return RedirectToPage("/Index", new { classID = classID });
        }

        public IActionResult RenameStudent(string studentID, string name)
        {
            var teacherID = UserManager.GetUserId(HttpContext.User);
            Protector = Provider.CreateProtector("TeacherClass" + teacherID);

            var student = DbContext.Students
                .Single(s => s.Id == studentID);
            student.UserName = Protector.Protect(name);

            DbContext.Update(student);
            DbContext.SaveChanges();

            return RedirectToPage("/Index", new { classID = student.SchoolClassID });
        }

        public IActionResult DeleteStudent(string id, int classID)
        {
            var student = DbContext.Students.Single(s => s.Id == id);
            DbContext.Remove(student);
            DbContext.SaveChanges();

            return RedirectToPage("/Index", new { classID = classID });
        }

        public void AddAssignment(string title, DateTime date, int classID)
        {
            var newAssignment = new Assignment
            {
                DueDate = date,
                Title = title,
                MaxScore = 10
            };

            DbContext.Add(newAssignment);
            DbContext.SaveChanges();

            var newASC = new AssignmentSchoolClass
            {
                SchoolClassID = classID,
                AssignmentID = newAssignment.ID
            };
            DbContext.Add(newASC);
            DbContext.SaveChanges();
        }

        public void UpdateAssignment(int assignmentID, string field, string value)
        {
            var assignment = DbContext.Assignments
                .Single(a => a.ID == assignmentID);

            switch (field)
            {
                case "Title":
                    assignment.Title = value;
                    break;
                case "Date":
                    assignment.DueDate = DateTime.Parse(value);
                    break;
                case "Rubric":
                    assignment.RubricID = int.Parse(value);
                    break;
                case "Weight":
                    assignment.Weight = int.Parse(value);
                    break;
                case "MaxScore":
                    assignment.MaxScore = int.Parse(value);
                    break;
            }

            DbContext.Update(assignment);
            DbContext.SaveChanges();
        }

        public void UpdateScore(string studentID, int assignmentID, int assessmentID, int score)
        {
            var student = DbContext.Students
                .Single(s => s.Id == studentID);

            AssessmentGrade grade = null;

            if (DbContext.AssessmentGrades.Where(ag => ag.AssignmentID == assignmentID && ag.StudentID == studentID).Any())
            {
                if (assessmentID > 0 && DbContext.AssessmentGrades.Where(ag => ag.AssessmentID == assessmentID && ag.StudentID == studentID).Any())
                {
                    grade = DbContext.AssessmentGrades.Single(ag => ag.AssessmentID == assessmentID && ag.StudentID == studentID);
                }
                else if (assessmentID == 0)
                {
                    grade = DbContext.AssessmentGrades
                        .Single(ag => ag.AssignmentID == assignmentID && ag.StudentID == studentID);
                }
            }

            if (grade == null)
            {
                grade = new AssessmentGrade();
                DbContext.Add(grade);
                DbContext.SaveChanges();
            }

            grade.StudentID = studentID;
            grade.AssignmentID = assignmentID;
            grade.Score = score;
            grade.Graded = true;
            grade.Completed = true;
            if (assessmentID > 0)
            {
                grade.AssessmentID = assessmentID;
            }

            DbContext.Update(grade);
            DbContext.SaveChanges();
        }
    }
}

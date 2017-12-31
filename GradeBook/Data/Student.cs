using GradeBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace GradeBook.Data
{
    public class Student : ApplicationUser
    {
        public virtual List<AssessmentGrade> AssessmentGrades { get; set; }
        public int StudentClassNumber { get; set; }
        public int SchoolClassID { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}

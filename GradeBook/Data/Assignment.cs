using GradeBook.Data;


using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.Data
{
    public class Assignment
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public int? RubricID { get; set; }
        public Rubric Rubric { get; set; }
        public List<AssessmentGrade> AssessmentGrades { get; set; }
        public int? MaxScore { get; set; }
        public List<AssignmentSchoolClass> AssignmentSchoolClasses { get; set; }
        public double Weight { get; set; }
    }
}

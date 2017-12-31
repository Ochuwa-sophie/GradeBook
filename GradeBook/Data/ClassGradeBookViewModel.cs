using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.Data
{
    public class ClassGradeBookViewModel
    {
        public string ClassName { get; set; }
        public int ClassID { get; set; }
        public List<Student> Students { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<SchoolClass> AllClasses { get; set; }
        public List<Rubric> TeacherRubrics { get; set; }
        public string TeacherID { get; set; }
    }
}

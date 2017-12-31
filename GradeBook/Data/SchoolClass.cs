using GradeBook.Data;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeBook.Data
{
    public class SchoolClass
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual Teacher Teacher { get; set; }
        public string TeacherId { get; set; }

        public virtual List<Student> Students { get; set; }
        public List<RubricSchoolClass> RubricSchoolClasses { get; set; }

        public List<AssignmentSchoolClass> AssignmentSchoolClasses { get; set; }

        public SchoolClass()
        {
            Students = new List<Student>();
        }
    }
}

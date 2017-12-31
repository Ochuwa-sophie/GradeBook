

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeBook.Data
{
    public class Rubric
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Assessment> Assessments { get; set; }
        public List<RubricSchoolClass> RubricSchoolClasses { get; set; }
        public virtual List<Assignment> Assignments { get; set; }
        public string TeacherID { get; set; }
        public virtual Teacher Teacher { get; set; }
        public List<RubricScoreHeader> RubricScoreHeaders { get; set; }
        public string Instructions { get; set; }
    }
}

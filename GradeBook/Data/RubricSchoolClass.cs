using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeBook.Data
{
    public class RubricSchoolClass
    {
        public int RubricId { get; set; }
        public virtual Rubric Rubric { get; set; }
        public int SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}

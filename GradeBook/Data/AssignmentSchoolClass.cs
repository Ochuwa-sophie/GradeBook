
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.Data
{
    public class AssignmentSchoolClass
    {
        public int AssignmentID { get; set; }
        public Assignment Assignment { get; set; }
        public int SchoolClassID { get; set; }
        public SchoolClass SchoolClass { get; set; }
    }
}

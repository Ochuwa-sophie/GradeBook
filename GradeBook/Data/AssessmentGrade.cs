using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeBook.Data
{
    public class AssessmentGrade
    {
        public int ID { get; set; }
        public int? AssessmentID { get; set; }
        public Assessment Assessment { get; set; }
        public string StudentID { get; set; }
        public virtual Student Student { get; set; }
        public int Score { get; set; }
        public bool Completed { get; set; }
        public int? AssignmentID { get; set; }
        public Assignment Assignment { get; set; }
        public bool Graded { get; set; }
    }
 }

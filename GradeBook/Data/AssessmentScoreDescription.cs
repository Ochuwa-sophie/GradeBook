using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.Data
{
    public class AssessmentScoreDescription
    {
        public int ID { get; set; }
        public Assessment Assessment { get; set; }
        public int AssessmentID { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
    }
}

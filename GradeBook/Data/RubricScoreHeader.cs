using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.Data
{
    public class RubricScoreHeader
    {
        public int ID { get; set; }
        public int RubricID { get; set; }
        public virtual Rubric Rubric { get; set; }
        public string Header { get; set; }
        public int Score { get; set; }
    }
}

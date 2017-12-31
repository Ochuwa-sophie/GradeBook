using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.Data
{
    public class RubricViewModel
    {
        public int RubricID { get; set; }
        public string RubricName { get; set; }
        public List<AssessmentViewModel> Assessments { get; set; }
        public List<RubricScoreHeader> Headers { get; set; }
        public List<Tuple<int, string>> AllRubrics { get; set; }
        public string Instructions { get; set; }
    }
}

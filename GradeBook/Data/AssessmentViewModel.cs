using System.Collections.Generic;

namespace GradeBook.Data
{
    public class AssessmentViewModel
    {
        public int AssessmentID { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public List<DescriptionViewModel> Descriptions { get; set; }
        public int? StudentScore { get; set; }
        public int? RubricID { get; set; }
    }
}
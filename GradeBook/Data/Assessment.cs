
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeBook.Data
{
    public class Assessment
    {
        public int ID { get; set; }
        public int RubricID { get; set; }
        public virtual Rubric Rubric { get; set; }
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public virtual List<AssessmentGrade> AssessmentGrades { get; set; }
        public List<AssessmentScoreDescription> AssessmentScoreDescriptions { get; set; }
    }
}

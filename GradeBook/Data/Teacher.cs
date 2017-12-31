



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeBook.Data;

namespace GradeBook.Data
{
    public class Teacher: ApplicationUser
    {
        public List<SchoolClass> SchoolClasses { get; set; }
        public List<Rubric> Rubrics { get; set; }
    }
}

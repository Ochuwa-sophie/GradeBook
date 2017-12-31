using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SchoolClass> SchoolClasses { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<AssessmentGrade> AssessmentGrades { get; set; }
        public virtual DbSet<Rubric> Rubrics { get; set; }
        public virtual DbSet<RubricSchoolClass> RubricSchoolClasses { get; internal set; }
        public virtual DbSet<AssessmentScoreDescription> AssessmentScoreDescriptions { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentSchoolClass> AssignmentSchoolClasses { get; set; }
        public virtual DbSet<RubricScoreHeader> RubricScoreHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RubricSchoolClass>()
                .HasKey(rsb => new { rsb.RubricId, rsb.SchoolClassId });

            builder.Entity<RubricSchoolClass>()
                .HasOne(rsb => rsb.Rubric)
                .WithMany(rsb => rsb.RubricSchoolClasses)
                .HasForeignKey(rsb => rsb.RubricId);

            builder.Entity<RubricSchoolClass>()
                .HasOne(rsb => rsb.SchoolClass)
                .WithMany(rsb => rsb.RubricSchoolClasses)
                .HasForeignKey(rsb => rsb.SchoolClassId);

            builder.Entity<AssignmentSchoolClass>()
                .HasOne(asc => asc.Assignment)
                .WithMany(a => a.AssignmentSchoolClasses)
                .HasForeignKey(asc => asc.AssignmentID);

            builder.Entity<AssignmentSchoolClass>()
                .HasOne(asc => asc.SchoolClass)
                .WithMany(sc => sc.AssignmentSchoolClasses)
                .HasForeignKey(asc => asc.SchoolClassID);

            builder.Entity<AssignmentSchoolClass>()
                .HasKey(asc => new { asc.AssignmentID, asc.SchoolClassID });


        }
    }
}

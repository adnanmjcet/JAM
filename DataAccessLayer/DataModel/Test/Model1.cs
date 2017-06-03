namespace DataAccessLayer.DataModel.Test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Course_Test> Course_Test { get; set; }
        public virtual DbSet<Course_Test_Answer> Course_Test_Answer { get; set; }
        public virtual DbSet<CourseSession> CourseSessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Question)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Answer1)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Answer2)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Answer3)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Answer4)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.CorrectAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Mark)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Course_Test_Answer>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<CourseSession>()
                .Property(e => e.Topic)
                .IsUnicode(false);

            modelBuilder.Entity<CourseSession>()
                .Property(e => e.Document1)
                .IsUnicode(false);

            modelBuilder.Entity<CourseSession>()
                .Property(e => e.Document2)
                .IsUnicode(false);

            modelBuilder.Entity<CourseSession>()
                .Property(e => e.AudioLink)
                .IsUnicode(false);

            modelBuilder.Entity<CourseSession>()
                .Property(e => e.VideoLink)
                .IsUnicode(false);
        }
    }
}

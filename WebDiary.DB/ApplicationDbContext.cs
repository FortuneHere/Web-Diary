using System.Data.Entity;
using WebDiary.DB.Models;

namespace WebDiary.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInfo> CourseInfos { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonMark> LessonMarks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Student>();
            entity.HasRequired(x => x.User)
                  .WithOptional(x => x.Student);
            var entity2 = modelBuilder.Entity<Teacher>();
            entity2.HasRequired(x => x.User)
                   .WithOptional(x => x.Teacher);
            base.OnModelCreating(modelBuilder);
        }
    }
}
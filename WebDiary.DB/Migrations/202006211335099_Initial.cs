using System.Data.Entity.Migrations;

namespace WebDiary.DB.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.CourseInfos",
                    c => new
                    {
                        Id = c.Long(false, true),
                        Title = c.String(),
                        Department = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Courses",
                    c => new
                    {
                        Id = c.Long(false, true),
                        GroupId = c.Long(false),
                        CourseInfoId = c.Long(false),
                        ClassType = c.Int(false),
                        TeacherId = c.Long(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseInfos", t => t.CourseInfoId, true)
                .ForeignKey("dbo.Groups", t => t.GroupId, true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, true)
                .Index(t => t.GroupId)
                .Index(t => t.CourseInfoId)
                .Index(t => t.TeacherId);

            CreateTable(
                    "dbo.Groups",
                    c => new
                    {
                        Id = c.Long(false, true),
                        StudyYear = c.Int(false),
                        Number = c.String(false, 4)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.GroupStudents",
                    c => new
                    {
                        Id = c.Long(false, true),
                        GroupId = c.Long(false),
                        StudentId = c.Long(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, true)
                .ForeignKey("dbo.Students", t => t.StudentId, true)
                .Index(t => t.GroupId)
                .Index(t => t.StudentId);

            CreateTable(
                    "dbo.Lessons",
                    c => new
                    {
                        Id = c.Long(false, true),
                        CourseId = c.Long(false),
                        Date = c.DateTime(false),
                        StartTime = c.Time(false, 7),
                        EndTime = c.Time(false, 7),
                        ClassroomNumber = c.String(),
                        BuildNumber = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, true)
                .Index(t => t.CourseId);

            CreateTable(
                    "dbo.Homeworks",
                    c => new
                    {
                        Id = c.Long(false, true),
                        LessonId = c.Long(false),
                        Text = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId, true)
                .Index(t => t.LessonId);

            CreateTable(
                    "dbo.LessonMarks",
                    c => new
                    {
                        Id = c.Long(false, true),
                        LessonId = c.Long(false),
                        StudentId = c.Long(false),
                        IsAbsent = c.Boolean(false),
                        Mark = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId, true)
                .ForeignKey("dbo.Students", t => t.StudentId, true)
                .Index(t => t.LessonId)
                .Index(t => t.StudentId);

            CreateTable(
                    "dbo.Students",
                    c => new
                    {
                        Id = c.Long(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        Id = c.Long(false, true),
                        FirstName = c.String(false, 20),
                        LastName = c.String(false, 20),
                        MiddleName = c.String(maxLength: 20),
                        Login = c.String(false),
                        Password = c.String(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Teachers",
                    c => new
                    {
                        Id = c.Long(false),
                        IsAdministrator = c.Boolean(false),
                        Post = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Students", "Id", "dbo.Users");
            DropForeignKey("dbo.Teachers", "Id", "dbo.Users");
            DropForeignKey("dbo.Courses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.LessonMarks", "StudentId", "dbo.Students");
            DropForeignKey("dbo.GroupStudents", "StudentId", "dbo.Students");
            DropForeignKey("dbo.LessonMarks", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Homeworks", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupStudents", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Courses", "CourseInfoId", "dbo.CourseInfos");
            DropIndex("dbo.Teachers", new[] {"Id"});
            DropIndex("dbo.Students", new[] {"Id"});
            DropIndex("dbo.LessonMarks", new[] {"StudentId"});
            DropIndex("dbo.LessonMarks", new[] {"LessonId"});
            DropIndex("dbo.Homeworks", new[] {"LessonId"});
            DropIndex("dbo.Lessons", new[] {"CourseId"});
            DropIndex("dbo.GroupStudents", new[] {"StudentId"});
            DropIndex("dbo.GroupStudents", new[] {"GroupId"});
            DropIndex("dbo.Courses", new[] {"TeacherId"});
            DropIndex("dbo.Courses", new[] {"CourseInfoId"});
            DropIndex("dbo.Courses", new[] {"GroupId"});
            DropTable("dbo.Teachers");
            DropTable("dbo.Users");
            DropTable("dbo.Students");
            DropTable("dbo.LessonMarks");
            DropTable("dbo.Homeworks");
            DropTable("dbo.Lessons");
            DropTable("dbo.GroupStudents");
            DropTable("dbo.Groups");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseInfos");
        }
    }
}
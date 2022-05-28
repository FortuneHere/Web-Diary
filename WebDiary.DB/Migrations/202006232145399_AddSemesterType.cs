namespace WebDiary.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSemesterType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "SemesterType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "SemesterType");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chapterCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "Created");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixChapterStore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChapterStores", "ApplicationUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChapterStores", "ApplicationUserId");
        }
    }
}

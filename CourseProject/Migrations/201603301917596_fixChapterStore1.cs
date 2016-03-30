namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixChapterStore1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ChapterStores", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ChapterStores", "ApplicationUserId");
            RenameColumn(table: "dbo.ChapterStores", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.ChapterStores", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ChapterStores", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ChapterStores", new[] { "ApplicationUserId" });
            AlterColumn("dbo.ChapterStores", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ChapterStores", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.ChapterStores", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ChapterStores", "ApplicationUser_Id");
        }
    }
}

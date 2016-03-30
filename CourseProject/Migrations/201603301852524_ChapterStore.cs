namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChapterStore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChapterStores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreativeId = c.Int(nullable: false),
                        ChapterId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChapterStores", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ChapterStores", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ChapterStores");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentcreativeIdfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Comments", new[] { "Creative_Id" });
            RenameColumn(table: "dbo.Comments", name: "Creative_Id", newName: "CreativeId");
            AlterColumn("dbo.Comments", "CreativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "CreativeId");
            AddForeignKey("dbo.Comments", "CreativeId", "dbo.Creatives", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Comments", new[] { "CreativeId" });
            AlterColumn("dbo.Comments", "CreativeId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "CreativeId", newName: "Creative_Id");
            CreateIndex("dbo.Comments", "Creative_Id");
            AddForeignKey("dbo.Comments", "Creative_Id", "dbo.Creatives", "Id");
        }
    }
}

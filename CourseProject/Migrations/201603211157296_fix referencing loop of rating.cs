namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixreferencingloopofrating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Ratings", new[] { "Creative_Id" });
            RenameColumn(table: "dbo.Ratings", name: "Creative_Id", newName: "CreativeId");
            AlterColumn("dbo.Ratings", "CreativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ratings", "CreativeId");
            AddForeignKey("dbo.Ratings", "CreativeId", "dbo.Creatives", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Ratings", new[] { "CreativeId" });
            AlterColumn("dbo.Ratings", "CreativeId", c => c.Int());
            RenameColumn(table: "dbo.Ratings", name: "CreativeId", newName: "Creative_Id");
            CreateIndex("dbo.Ratings", "Creative_Id");
            AddForeignKey("dbo.Ratings", "Creative_Id", "dbo.Creatives", "Id");
        }
    }
}

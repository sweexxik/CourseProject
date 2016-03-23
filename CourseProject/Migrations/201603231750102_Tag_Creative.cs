namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tag_Creative : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "Creative_Id" });
            RenameColumn(table: "dbo.Tags", name: "Creative_Id", newName: "CreativeId");
            AlterColumn("dbo.Tags", "CreativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tags", "CreativeId");
            AddForeignKey("dbo.Tags", "CreativeId", "dbo.Creatives", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "CreativeId" });
            AlterColumn("dbo.Tags", "CreativeId", c => c.Int());
            RenameColumn(table: "dbo.Tags", name: "CreativeId", newName: "Creative_Id");
            CreateIndex("dbo.Tags", "Creative_Id");
            AddForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives", "Id");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagsonetomay1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagCreatives", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagCreatives", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.TagCreatives", new[] { "Tag_Id" });
            DropIndex("dbo.TagCreatives", new[] { "Creative_Id" });
            AddColumn("dbo.Tags", "Creative_Id", c => c.Int());
            CreateIndex("dbo.Tags", "Creative_Id");
            AddForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives", "Id");
            DropTable("dbo.TagCreatives");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagCreatives",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Creative_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Creative_Id });
            
            DropForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "Creative_Id" });
            DropColumn("dbo.Tags", "Creative_Id");
            CreateIndex("dbo.TagCreatives", "Creative_Id");
            CreateIndex("dbo.TagCreatives", "Tag_Id");
            AddForeignKey("dbo.TagCreatives", "Creative_Id", "dbo.Creatives", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagCreatives", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}

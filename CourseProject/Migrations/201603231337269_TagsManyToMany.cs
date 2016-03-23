namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagsManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "Creative_Id" });
            CreateTable(
                "dbo.TagCreatives",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Creative_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Creative_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Creatives", t => t.Creative_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Creative_Id);
            
            DropColumn("dbo.Tags", "Creative_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Creative_Id", c => c.Int());
            DropForeignKey("dbo.TagCreatives", "Creative_Id", "dbo.Creatives");
            DropForeignKey("dbo.TagCreatives", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagCreatives", new[] { "Creative_Id" });
            DropIndex("dbo.TagCreatives", new[] { "Tag_Id" });
            DropTable("dbo.TagCreatives");
            CreateIndex("dbo.Tags", "Creative_Id");
            AddForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives", "Id");
        }
    }
}

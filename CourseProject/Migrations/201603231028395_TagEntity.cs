namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Creative_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creatives", t => t.Creative_Id)
                .Index(t => t.Creative_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "Creative_Id" });
            DropTable("dbo.Tags");
        }
    }
}

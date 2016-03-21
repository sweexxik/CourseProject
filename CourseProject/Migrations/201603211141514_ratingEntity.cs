namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Creative_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Creatives", t => t.Creative_Id)
                .Index(t => t.Creative_Id);
            
            DropColumn("dbo.Creatives", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Creatives", "Rating", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ratings", "Creative_Id", "dbo.Creatives");
            DropIndex("dbo.Ratings", new[] { "Creative_Id" });
            DropTable("dbo.Ratings");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creativecategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreativeCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Creatives", "Category_Id", c => c.Int());
            CreateIndex("dbo.Creatives", "Category_Id");
            AddForeignKey("dbo.Creatives", "Category_Id", "dbo.CreativeCategories", "Id");
            DropColumn("dbo.Creatives", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Creatives", "Category", c => c.String());
            DropForeignKey("dbo.Creatives", "Category_Id", "dbo.CreativeCategories");
            DropIndex("dbo.Creatives", new[] { "Category_Id" });
            DropColumn("dbo.Creatives", "Category_Id");
            DropTable("dbo.CreativeCategories");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagsManyToManyNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "Tag_Id", c => c.Int());
            CreateIndex("dbo.Tags", "Tag_Id");
            AddForeignKey("dbo.Tags", "Tag_Id", "dbo.Tags", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.Tags", new[] { "Tag_Id" });
            DropColumn("dbo.Tags", "Tag_Id");
        }
    }
}

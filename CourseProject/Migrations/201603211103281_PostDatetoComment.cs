namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostDatetoComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "PostDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "PostDate");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creativeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creatives", "Created");
        }
    }
}

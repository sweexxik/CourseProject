namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreativeDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creatives", "Description");
        }
    }
}

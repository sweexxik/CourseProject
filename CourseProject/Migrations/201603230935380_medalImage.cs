namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medalImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medals", "ImageUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medals", "ImageUri");
        }
    }
}

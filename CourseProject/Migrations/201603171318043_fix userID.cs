namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixuserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creatives", "UserId");
        }
    }
}

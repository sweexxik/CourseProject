namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creativeUserIdToUserIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creatives", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Creatives", "User_Id");
            AddForeignKey("dbo.Creatives", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Creatives", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Creatives", "UserId", c => c.String());
            DropForeignKey("dbo.Creatives", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Creatives", new[] { "User_Id" });
            DropColumn("dbo.Creatives", "User_Id");
        }
    }
}

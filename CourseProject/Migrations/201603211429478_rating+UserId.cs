namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ratings", "User_Id");
            AddForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropColumn("dbo.Ratings", "User_Id");
        }
    }
}

namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityUserToLikes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Likes", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Likes", "User_Id");
            AddForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Likes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Likes", "UserId", c => c.String());
            DropForeignKey("dbo.Likes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropColumn("dbo.Likes", "User_Id");
        }
    }
}

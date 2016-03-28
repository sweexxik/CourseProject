namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableTagCreativeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "CreativeId" });
            AlterColumn("dbo.Tags", "CreativeId", c => c.Int());
            CreateIndex("dbo.Tags", "CreativeId");
            AddForeignKey("dbo.Tags", "CreativeId", "dbo.Creatives", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "CreativeId", "dbo.Creatives");
            DropIndex("dbo.Tags", new[] { "CreativeId" });
            AlterColumn("dbo.Tags", "CreativeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tags", "CreativeId");
            AddForeignKey("dbo.Tags", "CreativeId", "dbo.Creatives", "Id", cascadeDelete: true);
        }
    }
}

namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editBlogComment1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogComment", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BlogComment", new[] { "UserId" });
            DropPrimaryKey("dbo.BlogComment");
            AddColumn("dbo.BlogComment", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.BlogComment", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.BlogComment", "ID");
            CreateIndex("dbo.BlogComment", "UserId");
            AddForeignKey("dbo.BlogComment", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComment", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.BlogComment", new[] { "UserId" });
            DropPrimaryKey("dbo.BlogComment");
            AlterColumn("dbo.BlogComment", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.BlogComment", "ID");
            AddPrimaryKey("dbo.BlogComment", new[] { "UserId", "BlogPostID" });
            CreateIndex("dbo.BlogComment", "UserId");
            AddForeignKey("dbo.BlogComment", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}

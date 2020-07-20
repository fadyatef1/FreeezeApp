namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBlogComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComment",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        BlogPostID = c.Int(nullable: false),
                        Comment = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BlogPostID })
                .ForeignKey("dbo.BlogPost", t => t.BlogPostID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BlogPostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComment", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComment", "BlogPostID", "dbo.BlogPost");
            DropIndex("dbo.BlogComment", new[] { "BlogPostID" });
            DropIndex("dbo.BlogComment", new[] { "UserId" });
            DropTable("dbo.BlogComment");
        }
    }
}

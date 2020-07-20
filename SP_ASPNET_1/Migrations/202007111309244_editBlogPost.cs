namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editBlogPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlogLike", "BlogPost_BlogPostID", "dbo.BlogPost");
            DropIndex("dbo.BlogLike", new[] { "BlogPost_BlogPostID" });
            RenameColumn(table: "dbo.BlogLike", name: "BlogPost_BlogPostID", newName: "BlogPostID");
            DropPrimaryKey("dbo.BlogLike");
            AlterColumn("dbo.BlogLike", "BlogPostID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BlogLike", new[] { "UserId", "BlogPostID" });
            CreateIndex("dbo.BlogLike", "BlogPostID");
            AddForeignKey("dbo.BlogLike", "BlogPostID", "dbo.BlogPost", "BlogPostID", cascadeDelete: true);
            DropColumn("dbo.BlogLike", "BlogID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogLike", "BlogID", c => c.Int(nullable: false));
            DropForeignKey("dbo.BlogLike", "BlogPostID", "dbo.BlogPost");
            DropIndex("dbo.BlogLike", new[] { "BlogPostID" });
            DropPrimaryKey("dbo.BlogLike");
            AlterColumn("dbo.BlogLike", "BlogPostID", c => c.Int());
            AddPrimaryKey("dbo.BlogLike", new[] { "UserId", "BlogID" });
            RenameColumn(table: "dbo.BlogLike", name: "BlogPostID", newName: "BlogPost_BlogPostID");
            CreateIndex("dbo.BlogLike", "BlogPost_BlogPostID");
            AddForeignKey("dbo.BlogLike", "BlogPost_BlogPostID", "dbo.BlogPost", "BlogPostID");
        }
    }
}

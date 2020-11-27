namespace FeedBackDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllModelAndSeedDataAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 250),
                        CreateDate = c.DateTime(nullable: false),
                        PostId = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.PostId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Votes", new[] { "CommentId" });
            DropIndex("dbo.Posts", new[] { "CreatedBy" });
            DropIndex("dbo.Comments", new[] { "CreatedBy" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropTable("dbo.Votes");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}

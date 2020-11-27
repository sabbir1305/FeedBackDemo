namespace FeedBackDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllModelAndSeedDataAdded2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "DisLikeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "DisLikeCount");
            DropColumn("dbo.Comments", "LikeCount");
        }
    }
}

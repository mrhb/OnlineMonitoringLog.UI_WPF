namespace OnlineMonitoringLog.UI_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringIp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Posts", new[] { "BlogId" });
            AddColumn("dbo.Units", "StringIp", c => c.String());
            DropTable("dbo.Blogs");
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BlogId);
            
            DropColumn("dbo.Units", "StringIp");
            CreateIndex("dbo.Posts", "BlogId");
            AddForeignKey("dbo.Posts", "BlogId", "dbo.Blogs", "BlogId", cascadeDelete: true);
        }
    }
}

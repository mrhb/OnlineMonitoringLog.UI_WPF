namespace OnlineMonitoringLog.UI_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProtocolType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "Type",
                c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "Type");
        }
    }
}

namespace OnlineMonitoringLog.UI_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIpToUnits : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Units", "LastUpdateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Units", "LastUpdateTime", c => c.String());
        }
    }
}

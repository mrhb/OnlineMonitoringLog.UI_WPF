namespace OnlineMonitoringLog.UI_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUnitEntityName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Units", newName: "UnitEntities");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UnitEntities", newName: "Units");
        }
    }
}

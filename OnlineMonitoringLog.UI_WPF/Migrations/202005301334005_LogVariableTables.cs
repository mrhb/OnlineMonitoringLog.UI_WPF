namespace OnlineMonitoringLog.UI_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogVariableTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredVarConfigs",
                c => new
                    {
                        VarConfigID = c.Int(nullable: false, identity: true),
                        Fk_UnitEntityId = c.Int(nullable: false),
                        resourceName = c.String(),
                    })
                .PrimaryKey(t => t.VarConfigID);
            
            CreateTable(
                "dbo.VariableLogs",
                c => new
                    {
                        VariableLogId = c.Guid(nullable: false),
                        FK_varaiableConfigID = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        _imeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VariableLogId)
                .ForeignKey("dbo.RegisteredVarConfigs", t => t.FK_varaiableConfigID, cascadeDelete: true)
                .Index(t => t.FK_varaiableConfigID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VariableLogs", "FK_varaiableConfigID", "dbo.RegisteredVarConfigs");
            DropIndex("dbo.VariableLogs", new[] { "FK_varaiableConfigID" });
            DropTable("dbo.VariableLogs");
            DropTable("dbo.RegisteredVarConfigs");
        }
    }
}

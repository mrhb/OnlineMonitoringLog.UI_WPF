namespace OnlineMonitoringLog.UI_WPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alarmableObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredOccConfigs",
                c => new
                    {
                        OccConfigID = c.Int(nullable: false, identity: true),
                        Fk_AlarmableObjId = c.Int(nullable: false),
                        SetPointType = c.String(),
                        SerializedSetPoint = c.String(),
                        IsAlarm = c.Boolean(nullable: false),
                        OccKindName = c.String(),
                        HysterisisOffset = c.Int(nullable: false),
                        OnDelay = c.Int(nullable: false),
                        OffDelay = c.Int(nullable: false),
                        OccSeverity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OccConfigID);
            
            CreateTable(
                "dbo.OccCultureInfoes",
                c => new
                    {
                        CultureInfoId = c.Int(nullable: false, identity: true),
                        FK_occConfigId = c.Int(nullable: false),
                        Culture = c.String(),
                        Template = c.String(),
                    })
                .PrimaryKey(t => t.CultureInfoId)
                .ForeignKey("dbo.RegisteredOccConfigs", t => t.FK_occConfigId, cascadeDelete: true)
                .Index(t => t.FK_occConfigId);
            
            CreateTable(
                "dbo.OccurenceLogs",
                c => new
                    {
                        OccLogId = c.Guid(nullable: false),
                        FK_occConfigID = c.Int(nullable: false),
                        SetValuesAsJson = c.String(),
                        state = c.Int(nullable: false),
                        SetTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ClearTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Acknowledge = c.Boolean(nullable: false),
                        AckUser = c.String(),
                        Comment = c.String(),
                        AlarmMessage = c.String(),
                    })
                .PrimaryKey(t => t.OccLogId)
                .ForeignKey("dbo.RegisteredOccConfigs", t => t.FK_occConfigID, cascadeDelete: true)
                .Index(t => t.FK_occConfigID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OccurenceLogs", "FK_occConfigID", "dbo.RegisteredOccConfigs");
            DropForeignKey("dbo.OccCultureInfoes", "FK_occConfigId", "dbo.RegisteredOccConfigs");
            DropIndex("dbo.OccurenceLogs", new[] { "FK_occConfigID" });
            DropIndex("dbo.OccCultureInfoes", new[] { "FK_occConfigId" });
            DropTable("dbo.OccurenceLogs");
            DropTable("dbo.OccCultureInfoes");
            DropTable("dbo.RegisteredOccConfigs");
        }
    }
}


using AlarmBase.DomainModel.Entities;
using System.Collections.Generic;
using System.Data.Entity;


namespace OnlineMonitoringLog.UI_WPF.model
{
    public class LoggingContext : AlarmableContext
    {
        public LoggingContext() : base("name = Default") { }
        public virtual DbSet<UnitEntity> Units { get; set; }      
    }
}

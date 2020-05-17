
using System.Collections.Generic;
using System.Data.Entity;


namespace OnlineMonitoringLog.UI_WPF.model
{
    public class LoggingContext : DbContext
    {
        public LoggingContext() : base("name = Default") { }
        public virtual DbSet<Unit> Units { get; set; }      
    }
}


using System.Collections.Generic;
using System.Data.Entity;


namespace OnlineMonitoringLog.UI_WPF.model
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class LogingContext : DbContext
    {
        public LogingContext() : base("name = Default") { }
        public virtual DbSet<Unit> Units { get; set; }
      
    }
}

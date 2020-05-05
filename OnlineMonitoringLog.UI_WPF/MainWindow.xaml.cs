using OnlineMonitoringLog.Core;
using OnlineMonitoringLog.UI_WPF.model;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;



namespace OnlineMonitoringLog.UI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CharmShahr Station;
      
        public MainWindow()
        {

            using (var db = new BloggingContext())
            {
               

                var blog = new Blog { Name = "M.Reza" };
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

         
                
            }



            InitializeComponent();
            

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station = (CharmShahr)this.Resources["_Station"];
        
        }
    }
}

using OnlineMonitoringLog.UI_WPF.model;
using System;
using System.Windows;
using System.Windows.Controls;
using OnlineMonitoringLog.UI_WPF.model;

using System.Linq;


namespace OnlineMonitoringLog.UI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StationViewModel Station;
      
        public MainWindow()
        {
           
                InitializeComponent();
           
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station = (StationViewModel)this.Resources["_Station"];
        
        }
    }
}

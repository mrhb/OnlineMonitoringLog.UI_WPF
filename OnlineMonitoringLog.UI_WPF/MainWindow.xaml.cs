using System.Windows;
using System.Windows.Controls;

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
           
            InitializeComponent();
            

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station = (CharmShahr)this.Resources["_Station"];
        
        }
    }
}

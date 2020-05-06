using OnlineMonitoringLog.UI_WPF.model;
using System;
using System.Windows;
using System.Windows.Controls;
using OnlineMonitoringLog.UI_WPF.model;

using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Windows.Data;

namespace OnlineMonitoringLog.UI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            InitializeComponent();


            using (var db = new LogingContext())
            {
                var UnitEntities = db.Units.ToList();

                StationViewModel Station = new StationViewModel();
                Station.StationViewModelBy(new Station(UnitEntities));

                DataContext = Station;      

            }            
        }

    }
}

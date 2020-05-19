using OnlineMonitoringLog.UI_WPF.model;
using System;
using System.Windows;
using System.Windows.Controls;

using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Windows.Data;
using System.Threading;
using lib60870.CS101;
using lib60870.CS104;
using lib60870;

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
   
            using (var db = new LoggingContext())
            {
                var UnitEntities = db.Units.ToList();

                List<IUnit> iunits=new List<IUnit>();
                foreach (var item in UnitEntities) {
                    if (item.Ip.ToString() == "127.0.0.0")
                         iunits.Add(new IEC104Unit(item.Ip));
                    else
                        iunits.Add(new coapUnit(item.Ip)); }

                StationViewModel Station = new StationViewModel();
                Station.StationViewModelBy(new Station(iunits));

                DataContext = Station;      

            }            
        }

    }  
}

using OnlineMonitoringLog.UI_WPF.model;
using System;
using System.Windows;

using System.Linq;
using System.Collections.Generic;

using OnlineMonitoringLog.Core;
using OnlineMonitoringLog.Core.Interfaces;
using OnlineMonitoringLog.Drivers;

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
                var UnitEntities = db.UnitEntity.ToList();

                List<IUnit> iunits=new List<IUnit>();
                foreach (var item in UnitEntities) {
                        iunits.Add(
                               UnitFactory.getUnit(item.ID, item.Type, item.Ip)
                               );
                }

                StationViewModel Station = new StationViewModel();
                Station.StationViewModelBy(new Station(iunits));

                DataContext = Station;      

            }            
        }

    }  
}

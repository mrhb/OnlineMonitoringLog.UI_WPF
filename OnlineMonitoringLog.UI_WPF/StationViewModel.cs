// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OnlineMonitoringLog.UI_WPF.model;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace OnlineMonitoringLog.UI_WPF
{
    public class StationViewModel: ViewModelBase
    {
        public Station st;
        ReadOnlyObservableUnitCollection _units;
        public StationViewModel()
        {
            st = new Station(new List<Unit>() {new Unit(IPAddress.Parse("192.168.1.19")) });

            //using (var db = new LogingContext())
            //{
            //    //db.Units.Add(new Unit(System.Net.IPAddress.Parse("192.168.1.1")));
            //    var UnitEntities = db.Units.ToList();

            //    st = new Station(UnitEntities);


            //}






            //Units.Add(new Unit(IPAddress.Parse("192.168.1.19")));
            //Units.Add(new Unit(IPAddress.Parse("192.168.1.29")));

            //selectedUnit = Units[0];

            //IEnumerable<Unit> obsCollection = (IEnumerable<Unit>)Units;
            //var list = new List<Unit>(obsCollection);



        }
        Unit selectedUnit;
        public Unit SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                if (value != selectedUnit)
                {
                    selectedUnit = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ReadOnlyObservableUnitCollection Units
        {
            get { return st.Units; }
            set
            {
                if (value != _units)
                {
                    _units = value;
                    this.NotifyPropertyChanged();
                }
            }

        }

    }
}
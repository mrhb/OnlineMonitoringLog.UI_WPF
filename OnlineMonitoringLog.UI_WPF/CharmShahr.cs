// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.


using OnlineMonitoringLog.Core;
using OnlineMonitoringLog.UI_WPF.model;
using System;
using System.Collections.Generic;
using System.Net;

namespace OnlineMonitoringLog.UI_WPF
{
    public class CharmShahr : Station
    {
        public CharmShahr()
        {

            Units.Add(new Unit(IPAddress.Parse("192.168.1.29")));
            Units.Add(new Unit(IPAddress.Parse("192.168.1.19")));

            selectedUnit = Units[0];
           
            IEnumerable<Unit> obsCollection = (IEnumerable<Unit>)Units;
            var list = new List<Unit>(obsCollection);

            this.Start(list);
            
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
    }
}
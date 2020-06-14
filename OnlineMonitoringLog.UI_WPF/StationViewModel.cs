// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using OnlineMonitoringLog.UI_WPF.model;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using OnlineMonitoringLog.Core.Interfaces;

namespace OnlineMonitoringLog.UI_WPF
{
    public class StationViewModel: ViewModelBase
    {
        public Station st;
        ReadOnlyObservableUnitCollection _units;

        public StationViewModel StationViewModelBy(Station _St)
        {
            st = _St;
            SelectedUnit = Units.FirstOrDefault();
            return this;
        }
        IUnit selectedUnit;
        public IUnit SelectedUnit
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
            get {
               
                return new ReadOnlyObservableUnitCollection(st.UnitList);
            }
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
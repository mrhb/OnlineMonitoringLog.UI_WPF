// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Net;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace OnlineMonitoringLog.UI_WPF.model
{

    public class Station : INotifyPropertyChanged
    {
        const string _name="CharmShahr";
        private List<Unit> unitList;

        public Station(List<Unit> units)
        {
            unitList = units;
        }
        public List<Unit> UnitList
        {
            get
            {
                return unitList;
            }
        }             
       
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
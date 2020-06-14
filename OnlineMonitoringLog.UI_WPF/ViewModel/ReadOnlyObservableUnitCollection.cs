using OnlineMonitoringLog.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMonitoringLog.UI_WPF.model
{
    public class ReadOnlyObservableUnitCollection : ReadOnlyObservableCollection<IUnit>
    {
        public ReadOnlyObservableUnitCollection(List<IUnit> list) : base(new ObservableCollection<IUnit>(list))
        {
        }
    }
}

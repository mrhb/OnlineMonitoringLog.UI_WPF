using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMonitoringLog.UI_WPF.model
{
    public class ReadOnlyObservableUnitCollection : ReadOnlyObservableCollection<Unit>
    {
        public ReadOnlyObservableUnitCollection(List<Unit> list) : base(new ObservableCollection<Unit>(list))
        {
        }
    }
}

// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CollectionBinding;
using System.Collections.ObjectModel;

namespace OnlineMonitoringLog.UI_WPF
{
    public class Station : ObservableCollection<Unit>
    {
        public Station()
        {
            Add(new Unit("Michael", "Alexander", "Bellevue"));
            Add(new Unit("Jeff", "Hay", "Redmond"));
            Add(new Unit("Christina", "Lee", "Kirkland"));
            Add(new Unit("Samantha", "Smith", "Seattle"));
        }

        public void add()
        {
            Add(new Unit("Mohammad", "Hajjar", "Seattle"));
        }
    }
}
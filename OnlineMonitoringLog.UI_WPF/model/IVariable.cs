using System;
using System.ComponentModel;

namespace OnlineMonitoringLog.UI_WPF.model
{
    public interface IVariable : INotifyPropertyChanged
    {
        string name { get; set; }
        string value { get; set; }
        DateTime timeStamp { get; set; }
        string ToString();

    }
}
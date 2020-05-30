using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlarmBase.DomainModel.repository;
using AlarmBase.DomainModel;
using OnlineMonitoringLog.UI_WPF.model;
using AlarmBase.DomainModel.generics;

namespace OnlineMonitoringLog.UI_WPF.model
{

// public abstract class AlarmableObj<StateType> : INotifyPropertyChanged, IAlarmableObj
public  class LoggableObj : AlarmableObj<int>
    {
      

        public LoggableObj(int objId, ILoggRepository Repo) : base(objId, Repo)
        {
        }
        public override List<Occurence<int>> ObjOccurences()
        {
            return new List<Occurence<int>>() { new hi(1) { setpoint = 50 } };
        }
    }
}

﻿
using OnlineMonitoringLog.Core.DataRepository.Entities;
using OnlineMonitoringLog.Core.Interfaces;
using OnlineMonitoringLog.Drivers.IEC104;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMonitoringLog.UI_WPF.model
{
    public class UnitFactory
    {
        public static IUnit getUnit(int UnitId, ProtocolType pt, IPAddress ip)
        {
            switch (pt)
            {
                case ProtocolType.Test:
                    return new IEC104Unit(UnitId, ip); //    return new TestUnit(UnitId, ip);
                //case ProtocolType.CoAp:
                //    return new coapUnit(UnitId, ip);
                case ProtocolType.IEC104:
                    return new IEC104Unit(UnitId, ip);
                default:
                  throw new ApplicationException(string.Format("Unit of Type '{0}' not implemented to be created", pt.ToString()));
                  
            }
         
        }
    }
}

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
        public static IUnit getUnit(ProtocolType pt, IPAddress ip)
        {
            switch (pt)
            {
                case ProtocolType.CoAp:
                  return new coapUnit(ip);
                case ProtocolType.IEC104:
                    return new IEC104Unit(ip);
                default:
                  throw new ApplicationException(string.Format("Unit of Type '{0}' not implemented to be created", pt.ToString()));
                  
            }
         
        }
    }
    public enum ProtocolType
    {
        CoAp,
        IEC104
    }
}

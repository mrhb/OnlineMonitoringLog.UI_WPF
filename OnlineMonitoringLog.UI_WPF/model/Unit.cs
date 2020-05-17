using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMonitoringLog.UI_WPF.model
{
 
    public class Unit 
    {

        public Unit() { }
        public int ID { get; set; }

        private IPAddress _Ip;

        [NotMapped]
        public IPAddress Ip
        {
            get { return _Ip; }
            private set
            {
                _Ip = value;
              
            }
        }

        public string StringIp
        {
            get
            {
                return _Ip.ToString();
            }
            set
            {
                Ip = IPAddress.Parse(value);
            }
        }

    }
}

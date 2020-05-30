using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMonitoringLog.UI_WPF.model.Generics
{
    public abstract class  Unit:IUnit, INotifyPropertyChanged
    {
        private ObservableCollection<IVariable> _coapVariables = new ObservableCollection<IVariable>();
        private string _LastUpdateTime;
        protected IPAddress _Ip;
        public Unit(IPAddress ip)
        {
            Ip = ip;
            Initialize();
        }
        public void Initialize()
        {
            var resources = UnitVariables();

            foreach (var res in resources)
            {
                res.PropertyChanged += valuChange;
                _coapVariables.Add(res);
            }
        }

        private void valuChange(object sender, PropertyChangedEventArgs e)
        {
            LastUpdateTime = DateTime.Now.ToString();
        }

        public string LastUpdateTime
        {
            get { return _LastUpdateTime; }
            private set
            {
                _LastUpdateTime = value;
                NotifyPropertyChanged("LastUpdateTime");
            }
        }

        public ObservableCollection<IVariable> Variables
        {
            get { return _coapVariables; }
            set
            {
                _coapVariables = value;
                NotifyPropertyChanged("Units");
            }
        }
        public int ID { get; set; }

        public IPAddress Ip
        {
            get { return _Ip; }
            private set
            {
                _Ip = value;
                NotifyPropertyChanged("ip");
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
        public abstract override string ToString();
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract List<IVariable> UnitVariables();
    }
}

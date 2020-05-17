// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CoAP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.CompilerServices;

namespace OnlineMonitoringLog.UI_WPF.model
{

    public class coapUnit : INotifyPropertyChanged, IUnit
    {
        private ObservableCollection<IVariable> _coapVariables= new ObservableCollection<IVariable>();
        private string _LastUpdateTime;
        private IPAddress _Ip;
        public coapUnit() { }
        public coapUnit(IPAddress ip)
        {
            Ip = ip;
            Initialize();         
        }
   public void Initialize()
    {

        var resources = new List<string>() { "ServerTime", "TimeOfDay", "helloworld" };

        for (int i = 0; i < 3; i++)
        {
            resources.Add("TimeOfDay" + i.ToString());
        }
        foreach (var res in resources)
        {
            var Client = new coapVariable(_Ip, res);
            Client.Respond += Respond;
            _coapVariables.Add(Client);
        }
    }
        private void Respond(object sender, ResponseEventArgs e)
        {
            LastUpdateTime =DateTime.Now.ToString();
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

        [NotMapped]
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

        [NotMapped]
        public IPAddress Ip
        {
            get { return _Ip; }
            private set
            {
                _Ip = value;
                NotifyPropertyChanged("ip");
            }
        }
        public string StringIp {
            get
            {
                return _Ip.ToString();
            }
            set
            {
                Ip = IPAddress.Parse(value);
            }
        }
        public override string ToString() { return Ip.ToString(); }
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     private Uri ResourceUri(string resourceName)
        {
            return new Uri("coap://" + _Ip.ToString() + "/" + resourceName);
        }
    }

    public class coapVariable : CoapClient, IVariable
    {
        string _value= "Not assigned";
        DateTime _timeStamp =new DateTime();
        string _resource = "Not assigned";
        public coapVariable(IPAddress ip,string resourceName) : base() {
            name = resourceName;
            Uri = new Uri("coap://" + ip.ToString() + "/" + resourceName);
            ObserveAsync();
            this.Respond += RecievedRespond;
        }
        private void RecievedRespond(object sender, ResponseEventArgs e)
        {
            value  = e.Response.ResponseText;
            timeStamp = DateTime.Now;
        }
        public string name
        {
            get
            {
                return Resource;
            }
            set
            {
                Resource = value;
                NotifyPropertyChanged("value");
            }
        }
        public string value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
                NotifyPropertyChanged("value");
            }
        }
        public DateTime timeStamp
        {
            get
            {
                return TimeStamp;
            }
            set
            {
                TimeStamp = value;
                NotifyPropertyChanged("timeStamp");
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }

            set
            {
                _timeStamp = value;
            }
        }

        public string Resource
        {
            get
            {
                return _resource;
            }

            set
            {
                _resource = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return value;
        }
    }

}
  
 
// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CoAP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;

namespace OnlineMonitoringLog.Core
{

    public class Unit : INotifyPropertyChanged
    {
        private coapVariable _Client;
        private ObservableCollection<coapVariable> _coapVariables= new ObservableCollection<coapVariable>();
        private string _LastUpdateTime;
        private IPAddress _ip;



        public Unit(IPAddress ip)
        {

            _ip = ip;
            ICoapConfig config = new CoapConfig();
            // ...

            // create a new client with custom config
            _Client = new coapVariable(config);

            _Client.Uri = ResourceUri("ServerTime");
           var observerRel= _Client.ObserveAsync();
          
            //_Client..Timeout = 2000;
            _Client.Respond += Respond;

            var resources = new List<string>() { "ServerTime", "TimeOfDay", "helloworld" };
            foreach (var res in resources)
            {
               var  Client = new coapVariable(config);

                Client.Uri = ResourceUri(res);
                Client.ObserveAsync();

                Client.Respond += Respond;

                _coapVariables.Add(Client);
            }

        }

        private void Respond(object sender, ResponseEventArgs e)
        {
            LastUpdateTime = e.Response.ResponseText;
        }

        public string LastUpdateTime
        {
            get { return _LastUpdateTime; }
            set
            {
                _LastUpdateTime = value;
                NotifyPropertyChanged("LastUpdateTime");
            }
        }
        public ObservableCollection<coapVariable> coapVariables
        {
            get { return _coapVariables; }
            set
            {
                _coapVariables = value;
                NotifyPropertyChanged("Units");
            }
        }
        public IPAddress ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                NotifyPropertyChanged("ip");
            }
        }
        private Uri ResourceUri(string resourceName)
        {
           return new Uri("coap://" + _ip.ToString() + "/"+ resourceName);
        }
        public override string ToString() { return ip.ToString(); }
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class coapVariable:CoapClient,INotifyPropertyChanged
    {
        string _value= "Not assigned";
        string _resource = "Not assigned";
        public coapVariable(ICoapConfig config) : base(config) {
            this.Respond += RecievedRespond;
        }

        private void RecievedRespond(object sender, ResponseEventArgs e)
        {
         value  = e.Response.ResponseText;
        }
        public string resource
        {
            get
            {
                return this.Uri.AbsolutePath;
            }
          
        }
        public string value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("value");
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
  
 
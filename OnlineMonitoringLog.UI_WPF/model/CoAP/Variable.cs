// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AlarmBase.DomainModel.Entities;
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
    public abstract class Variable :  IVariable
    {
        protected LoggableObj _loggableObj;
        RegisteredVarConfig _RegisteredVaraiableConfig;
        int _unitId;
        string _value = "Not assigned";
        DateTime _timeStamp = new DateTime();
        string _resource = "Not assigned";
        public Variable(string resourceName) : base()
        {
            name = resourceName;
           
        }
        public Variable(int unitId) : base()
        {
            _unitId= unitId;

        }


        public void RecievedData(int val, DateTime dt)
        {
            _loggableObj.State = val;
            value = val.ToString();
            timeStamp = dt;
        }

        private void RecievedRespond(object sender, ResponseEventArgs e)
        {
            value = e.Response.ResponseText;
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

        public int UnitId
        {
            get
            {
                return _unitId;
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

         public void SetConfig(object occConfig)
        {
            throw new NotImplementedException();
        }
    }

}


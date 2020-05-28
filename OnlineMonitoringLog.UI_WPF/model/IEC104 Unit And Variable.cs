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
using lib60870;
using lib60870.CS101;
using lib60870.CS104;
using System.Threading;
using System.Linq;
using AlarmBase.DomainModel.repository;
using AlarmBase.DomainModel;
using AlarmBase.DomainModel.generics;

namespace OnlineMonitoringLog.UI_WPF.model
{

    public class IEC104Unit : INotifyPropertyChanged, IUnit
    {
       private ILoggRepository repo = new LoggRepositry(new LoggingContext());
        private Timer ConnectionTimer;
        private ObservableCollection<IVariable> _iec104Variables = new ObservableCollection<IVariable>();
        private string _LastUpdateTime;
        private IPAddress _Ip;
        public IEC104Unit() { }
        public IEC104Unit(IPAddress ip)
        {
            Ip = ip;
            Initialize();
            ConnectionTimer = new Timer(ConnectToIec104Server, null, 0, 5000);
           
        }

        private void ConnectToIec104Server(object state)
    
        {
            Console.WriteLine("Connect to Iec104Server Using lib60870.NET version " + LibraryCommon.GetLibraryVersionString());
            Connection con = new Connection(Ip.ToString());

            con.DebugOutput = false;

            con.SetASDUReceivedHandler(asduReceivedHandler, null);
            con.SetConnectionHandler(ConnectionHandler, null);

            try
            {
                con.Connect();
                ConnectionTimer = null;
            }
            catch (Exception)
            {

                ConnectionTimer = new Timer(ConnectToIec104Server, null, 0, 5000);
            }
           
        }
        public void Initialize()
        {
            var resources = new List<IVariable>() {
            new iec104Variable(ObjAddress.InputWaterTemp, "InputWaterTemp",repo),
            new iec104Variable(ObjAddress.OutputWaterTemp, "OutputWaterTemp",repo),
            new iec104Variable(ObjAddress.OilPress, "OilPress",repo),
            new iec104Variable(ObjAddress.AdvanceSpark, "AdvanceSpark",repo),
            new iec104Variable(ObjAddress.ValvePosition, "ValvePosition",repo),
            new iec104Variable(ObjAddress.ValveFlow, "ValveFlow",repo),
            new iec104Variable(ObjAddress.ExhaustTemp, "ExhaustTemp",repo),
            new iec104Variable(ObjAddress.ElecPower, "ElecPower",repo),
            new iec104Variable(ObjAddress.ElecEnergy, "ElecEnergy",repo),
            new iec104Variable(ObjAddress.WorkTime, "WorkTime",repo),
            new iec104Variable(ObjAddress.frequency, "frequency",repo),
            new iec104Variable(ObjAddress.PowerFactor, "PowerFactor",repo),
            };

            foreach (var res in resources){_iec104Variables.Add(res);}
   
           
            ConnectToIec104Server(null);



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
            get { return _iec104Variables; }
            set
            {
                _iec104Variables = value;
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
        public override string ToString() { return "IEC104: "+ Ip.ToString(); }
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private  void ConnectionHandler(object parameter, ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OPENED:
                    Console.WriteLine("Connected");
                    ConnectionTimer = null;
                    break;
                case ConnectionEvent.CLOSED:
                    Console.WriteLine("Connection closed");
                    ConnectionTimer = new Timer(ConnectToIec104Server, null, 0, 5000);
                    break;
                case ConnectionEvent.STARTDT_CON_RECEIVED:
                    Console.WriteLine("STARTDT CON received");
                   
                    break;
                case ConnectionEvent.STOPDT_CON_RECEIVED:
                    Console.WriteLine("STOPDT CON received");
                    break;
            }
        }

        private  bool asduReceivedHandler(object parameter, ASDU asdu)
        {
            Console.WriteLine(asdu.ToString());

            if (asdu.TypeId == TypeID.M_ME_TF_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var val = (MeasuredValueShortWithCP56Time2a)asdu.GetElement(i);
                    var item = _iec104Variables.Where(p => ((iec104Variable)p).ObjectAddress == val.ObjectAddress).First();
                    item.value = val.Value.ToString();
                    item.timeStamp = DateTime.Now;
                }
            }
            else
            {
                Console.WriteLine("Unknown message type!");
            }
            LastUpdateTime = DateTime.Now.ToString();
            return true;
        }

        public class Receiver : IFileReceiver
        {
            public void Finished(FileErrorCode result)
            {
                Console.WriteLine("File download finished - code: " + result.ToString());
            }


            public void SegmentReceived(byte sectionName, int offset, int size, byte[] data)
            {
                Console.WriteLine("File segment - sectionName: {0} offset: {1} size: {2}", sectionName, offset, size);
            }
        }

      
    }
    public class iec104Variable : LoggableObj<int>, IVariable
    {
        string _value = "Not assigned";
        DateTime _timeStamp = new DateTime();
        string _resource = "Not assigned";
        int _ObjectAddress;
        public iec104Variable(int ObjectAddress, string resourceName,ILoggRepository Repo) : base(1,Repo)
        {
            name = resourceName;
            _ObjectAddress = ObjectAddress;
        }
        public void RecievedData(int val,DateTime dt)
        {
            value = val.ToString();
            timeStamp =dt;
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
                return _timeStamp;
            }
            set
            {
                _timeStamp = value;
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

        public int ObjectAddress {get { return _ObjectAddress; }}

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

        public override List<Occurence<int>> ObjOccurences()
        {
            return new List<Occurence<int>>() { new hi(1) { setpoint = 50 } };
        }
        class hi : IntThreshold
        {
            public hi(int _objId) : base(_objId)
            {
            }
        }
    }
    public class ObjAddress
    {
        public const int InputWaterTemp = 1;
        public const int OutputWaterTemp = 2;
        public const int OilPress = 3;
        public const int AdvanceSpark = 4;
        public const int ValvePosition = 5;
        public const int ValveFlow = 6;
        public const int ExhaustTemp = 7;
        public const int ElecPower = 8;
        public const int ElecEnergy = 9;
        public const int WorkTime = 10;
        public const int frequency = 11;
        public const int PowerFactor = 12;
    }
}


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

namespace OnlineMonitoringLog.UI_WPF.model
{

    public class IEC104Unit : INotifyPropertyChanged, IUnit
    {
        private ObservableCollection<IVariable> _iec104Variables = new ObservableCollection<IVariable>();
        private string _LastUpdateTime;
        private IPAddress _Ip;
        public IEC104Unit() { }
        public IEC104Unit(IPAddress ip)
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
                var Client = new iec104Variable(_Ip, res);
                Client.Respond += Respond;
                _iec104Variables.Add(Client);
            }
   
            Console.WriteLine("Using lib60870.NET version " + LibraryCommon.GetLibraryVersionString());

            Connection con = new Connection("127.0.0.1");

            con.DebugOutput = true;

            con.SetASDUReceivedHandler(asduReceivedHandler, null);
            con.SetConnectionHandler(ConnectionHandler, null);

            con.Connect();

            


            Console.WriteLine("CLOSE");

            con.Close();

            Console.WriteLine("RECONNECT");

            con.Connect();

            Console.WriteLine("CLOSE 2");

            con.Close();

            Console.WriteLine("Press any key to terminate...");
            Console.ReadKey();
        }
        private void Respond(object sender, ResponseEventArgs e)
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
        public override string ToString() { return Ip.ToString(); }
        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private static void ConnectionHandler(object parameter, ConnectionEvent connectionEvent)
        {
            switch (connectionEvent)
            {
                case ConnectionEvent.OPENED:
                    Console.WriteLine("Connected");
                    break;
                case ConnectionEvent.CLOSED:
                    Console.WriteLine("Connection closed");
                    break;
                case ConnectionEvent.STARTDT_CON_RECEIVED:
                    Console.WriteLine("STARTDT CON received");
                    break;
                case ConnectionEvent.STOPDT_CON_RECEIVED:
                    Console.WriteLine("STOPDT CON received");
                    break;
            }
        }

        private static bool asduReceivedHandler(object parameter, ASDU asdu)
        {
            Console.WriteLine(asdu.ToString());

            if (asdu.TypeId == TypeID.M_SP_NA_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var val = (SinglePointInformation)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + val.ObjectAddress + " SP value: " + val.Value);
                    Console.WriteLine("   " + val.Quality.ToString());
                }
            }
            else if (asdu.TypeId == TypeID.M_ME_TE_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var msv = (MeasuredValueScaledWithCP56Time2a)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + msv.ObjectAddress + " scaled value: " + msv.ScaledValue);
                    Console.WriteLine("   " + msv.Quality.ToString());
                    Console.WriteLine("   " + msv.Timestamp.ToString());
                }

            }
            else if (asdu.TypeId == TypeID.M_ME_TF_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var mfv = (MeasuredValueShortWithCP56Time2a)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + mfv.ObjectAddress + " float value: " + mfv.Value);
                    Console.WriteLine("   " + mfv.Quality.ToString());
                    Console.WriteLine("   " + mfv.Timestamp.ToString());
                    Console.WriteLine("   " + mfv.Timestamp.GetDateTime().ToString());
                }
            }
            else if (asdu.TypeId == TypeID.M_SP_TB_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var val = (SinglePointWithCP56Time2a)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + val.ObjectAddress + " SP value: " + val.Value);
                    Console.WriteLine("   " + val.Quality.ToString());
                    Console.WriteLine("   " + val.Timestamp.ToString());
                }
            }
            else if (asdu.TypeId == TypeID.M_ME_NC_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var mfv = (MeasuredValueShort)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + mfv.ObjectAddress + " float value: " + mfv.Value);
                    Console.WriteLine("   " + mfv.Quality.ToString());
                }
            }
            else if (asdu.TypeId == TypeID.M_ME_NB_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var msv = (MeasuredValueScaled)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + msv.ObjectAddress + " scaled value: " + msv.ScaledValue);
                    Console.WriteLine("   " + msv.Quality.ToString());
                }

            }
            else if (asdu.TypeId == TypeID.M_ME_ND_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var msv = (MeasuredValueNormalizedWithoutQuality)asdu.GetElement(i);

                    Console.WriteLine("  IOA: " + msv.ObjectAddress + " scaled value: " + msv.NormalizedValue);
                }

            }
            else if (asdu.TypeId == TypeID.C_IC_NA_1)
            {
                if (asdu.Cot == CauseOfTransmission.ACTIVATION_CON)
                    Console.WriteLine((asdu.IsNegative ? "Negative" : "Positive") + "confirmation for interrogation command");
                else if (asdu.Cot == CauseOfTransmission.ACTIVATION_TERMINATION)
                    Console.WriteLine("Interrogation command terminated");
            }
            else if (asdu.TypeId == TypeID.F_DR_TA_1)
            {
                Console.WriteLine("Received file directory:\n------------------------");
                int ca = asdu.Ca;

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    FileDirectory fd = (FileDirectory)asdu.GetElement(i);

                    Console.Write(fd.FOR ? "DIR:  " : "FILE: ");

                    Console.WriteLine("CA: {0} IOA: {1} Type: {2}", ca, fd.ObjectAddress, fd.NOF.ToString());
                }

            }
            else
            {
                Console.WriteLine("Unknown message type!");
            }

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
    public class iec104Variable : CoapClient, IVariable
    {
        string _value = "Not assigned";
        DateTime _timeStamp = new DateTime();
        string _resource = "Not assigned";
        public iec104Variable(IPAddress ip, string resourceName) : base()
        {
            name = resourceName;
            Uri = new Uri("coap://" + ip.ToString() + "/" + resourceName);
            ObserveAsync();
            this.Respond += RecievedRespond;
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


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
using OnlineMonitoringLog.UI_WPF.model.Generics;

namespace OnlineMonitoringLog.UI_WPF.model
{

    public class IEC104Unit : Unit
    {
      
        private Timer ConnectionTimer;
        private ObservableCollection<IVariable> _iec104Variables = new ObservableCollection<IVariable>();
        
        public IEC104Unit(IPAddress ip):base(ip)
        {          
            ConnectionTimer = new Timer(ConnectToIec104Server, null, 0, 5000);
             ConnectToIec104Server(null);
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
  
        
        public override string ToString() { return "IEC104: " + Ip.ToString(); }
       

        private void ConnectionHandler(object parameter, ConnectionEvent connectionEvent)
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

        private bool asduReceivedHandler(object parameter, ASDU asdu)
        {
            Console.WriteLine(asdu.ToString());

            if (asdu.TypeId == TypeID.M_ME_TF_1)
            {

                for (int i = 0; i < asdu.NumberOfElements; i++)
                {

                    var val = (MeasuredValueShortWithCP56Time2a)asdu.GetElement(i);
                    var item = Variables.Where(p => ((iec104Variable)p).ObjectAddress == val.ObjectAddress).First();
                    item.RecievedData((int)val.Value, DateTime.Now);
                }
            }
            else
            {
                Console.WriteLine("Unknown message type!");
            }
           return true;
        }

        public override List<IVariable> UnitVariables()
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

            return resources;
        }

       


    }
   
}


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
using OnlineMonitoringLog.UI_WPF.model.Generics;

namespace OnlineMonitoringLog.UI_WPF.model
{

    public class coapUnit : Unit
    {
        public coapUnit(int unitId, IPAddress ip) : base(unitId,ip) { }       
        
        public override string ToString() { return "CoAP: " + Ip.ToString(); }

        public override List<IVariable> UnitVariables()
        {
           var names= new List<string>() { "ServerTime", "TimeOfDay", "helloworld" };

            List<IVariable> resources=new List<IVariable>();

            foreach (var res in names)
            {
                var Client = new coapVariable(ID,_Ip, res, repo);
                resources.Add(Client);
            }
            return resources;
        }
    }
}


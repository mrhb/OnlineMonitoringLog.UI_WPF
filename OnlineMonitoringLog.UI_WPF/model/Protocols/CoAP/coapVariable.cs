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

        public class coapVariable : Variable
    {
        CoapClient _CoapClient;
       public coapVariable(int unitId, IPAddress ip, string resourceName, ILoggRepository repo) : base(unitId, resourceName,repo)
        {
            //_CoapClient = new CoapClient();
            name = resourceName;
            //_CoapClient.Uri = new Uri("coap://" + ip.ToString() + "/" + resourceName);
            //_CoapClient.ObserveAsync();
            //_CoapClient.Respond += RecievedRespond;
        }
        private void RecievedRespond(object sender, ResponseEventArgs e)
        {
            value = e.Response.ResponseText;
            timeStamp = DateTime.Now;
        }
   

        public override string ToString()
        {
            return value;
        }

    }

}


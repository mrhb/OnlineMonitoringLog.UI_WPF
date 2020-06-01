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
    public class iec104Variable :Variable
    {

        int _ObjectAddress;
        public iec104Variable(int unitId, int ObjectAddress, string resourceName, ILoggRepository Repo) : base(unitId,resourceName,Repo)
        {
            name = resourceName;
            _ObjectAddress = ObjectAddress;
        }


        public int ObjectAddress { get { return _ObjectAddress; } }
        
        public override string ToString()
        {
            return value;
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


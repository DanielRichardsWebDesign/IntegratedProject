﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PatientApp
{
    public class Messaging : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}
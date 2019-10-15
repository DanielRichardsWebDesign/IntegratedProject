using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HealthApp.Models
{
    public class messages : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}
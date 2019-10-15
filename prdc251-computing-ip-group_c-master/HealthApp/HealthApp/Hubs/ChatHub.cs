using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HealthApp
{
    public class chatHub : Hub
    {
        public void join(string match)
        {
            Groups.Add(Context.ConnectionId, match);
        }
        //placeholder method to send message to all clients
        public void send(string match, string name, string message)
        {
            Clients.Group(match).addNewMessageToPage(name, message);
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);
        }
    }
}
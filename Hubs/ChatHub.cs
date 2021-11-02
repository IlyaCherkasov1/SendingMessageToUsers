using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SendingMessageToUsers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SendingMessageToUsers.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
       
          public async Task Send(string message)
          {
            await Clients.All.SendAsync("Send", message);
            
           }

        public async Task Notify()
        {
            string userName = Context.User.FindFirstValue(ClaimTypes.GivenName);
            string time = DateTime.Now.ToString("t");
            await Clients.All.SendAsync("Notify", $"Sent by {userName} at {time} ");
        }


 
        public async Task Counter(string buttonId, int broCounter, int sisCounter )
        {
            if (buttonId == "butBro")
            {
                broCounter++;
            }
            if (buttonId == "butSis")
            {
                sisCounter++;
            }
            await Clients.All.SendAsync("Counter", $"{broCounter}", $"{sisCounter}");
        }
    }
}

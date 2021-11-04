using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SendingMessageToUsers.Data;
using SendingMessageToUsers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SendingMessageToUsers.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Send(string data)
        {
            string userName = Context.User.FindFirstValue(ClaimTypes.GivenName);

            Message message = new Message(userName, data);
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("Send", data);
        }

        public async Task Notify()
        {
            string userName = Context.User.FindFirstValue(ClaimTypes.GivenName);
            await Clients.All.SendAsync("Notify", $"{userName}", $"{DateTime.Now:t}");
        }

        public async Task Counter()
        {
            int broCounter = _context.Messages.Where(b => b.SendMessage == "Bro!").Count();
            int sisCounter = _context.Messages.Where(b => b.SendMessage == "Sis!").Count();
            await Clients.All.SendAsync("Counter", $"{broCounter}", $"{sisCounter}");
        }

        public override async Task OnConnectedAsync()
        {
            Message[] messages = _context.Messages.ToArray();

            if (messages.Length != 0)
            {
                await Clients.All.SendAsync("ConnectedMessages", messages);
            }

            await Counter();
            await base.OnConnectedAsync();  
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Counter();
            await base.OnDisconnectedAsync(exception);
        }
    }
}

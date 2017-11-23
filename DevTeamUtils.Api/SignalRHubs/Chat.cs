using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.SignalRHubs
{
    public class Chat : Hub
    {
        public async Task SendAll(string nick, string message)
        {   
            await Clients.All.InvokeAsync("SendAll", nick, message);
        }

        public async Task SendTo(string nick, string message)
        {
            await Clients.All.InvokeAsync("SendTo", nick, message);
        }

        public async Task Join(string nick, string message)
        {
            await Clients.All.InvokeAsync("Join", nick, message);
        }
    }
}

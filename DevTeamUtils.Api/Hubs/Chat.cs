using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Hubs
{
    public class Chat : Hub
    {
        private readonly IUserRepository _userRepository;
        public Chat(MongoDbContext context)
        {
            _userRepository = context.GetUserRepository();
        }

        /*public override async Task OnConnectedAsync()
        {
            //await Clients.Client(Context.ConnectionId).InvokeAsync("SetUsersOnline", await GetUsersOnline());

            await base.OnConnectedAsync();
        }*/

        public async Task OnUsersJoined()
        {
            await Clients.Client(Context.ConnectionId).InvokeAsync("UsersJoined", _userRepository.GetAllOnlineUsers());
        }

        public async Task OnUsersLeft()
        {
            await Clients.Client(Context.ConnectionId).InvokeAsync("UsersLeft", _userRepository.GetAllOnlineUsers());
        }

        public async Task SendAll(string message)
        {   
            await Clients.All.InvokeAsync("update", Context.User.Identity.Name, message);
        }

        public async Task SendTo(string toId, string message)
        {
            await Clients.Client(toId).InvokeAsync("message2Me", 
                Context.User.Identity.Name + "<span style='color: red'> in PVT</span>", 
                message);
        }

        public async Task Join()
        {   
            List<string> excludedIds = new List<string> { Context.ConnectionId };
            string message = Context.User.Identity.Name + " has join the server.";
            await Clients.Client(Context.ConnectionId).InvokeAsync("messageMe", Context.User.Identity.Name);
            await Clients.AllExcept(excludedIds).InvokeAsync("update", message);
            foreach (System.Security.Principal.IIdentity identity in Context.User.Identities)
            {
                //Context.Connection.
                string name = identity.Name;
                //identity.
            }
        }
    }
}

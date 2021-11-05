using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageCore.Controllers.api.SignalR
{
    public class ChatHUB : Hub
    {

        public async Task RegisterSession(string projectId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);
            await Clients.Group("test").SendAsync("RegisterSession", "registered");
        }

        public async Task Send(string message,string projectId)
        {
            await Clients.Group(projectId).SendAsync("message", message);
        }
    }
}
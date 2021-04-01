using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageCore.Controllers.api.SignalR
{
    public class ChatHUB : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("message",message);
        }
    }
}
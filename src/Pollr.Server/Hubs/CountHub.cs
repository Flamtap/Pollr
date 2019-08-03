using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Pollr.Server.Common;

namespace Pollr.Server.Hubs
{
    public class CountHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync(HubMethods.UserJoined, Context.ConnectionId);
        }

        [HubMethodName(HubMethods.Increment)]
        public Task Increment()
        {
            return Clients.All.SendAsync(HubMethods.Increment, Context.ConnectionId);
        }
    }
}

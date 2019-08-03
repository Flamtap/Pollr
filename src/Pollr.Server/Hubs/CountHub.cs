using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Pollr.Server.Common;
using Pollr.Server.Services;

namespace Pollr.Server.Hubs
{
    public class CountHub : Hub
    {
        private readonly StateManager _stateManager;

        public CountHub(StateManager stateManager)
        {
            _stateManager = stateManager;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync(HubEvents.UserJoined, Context.ConnectionId);

            await Clients.Caller.SendAsync(HubEvents.Count, _stateManager.GetCount());
        }

        [HubMethodName(HubEvents.Count)]
        public Task Count()
        {
            var newCount = _stateManager.BumpCount();

            return Clients.All.SendAsync(HubEvents.Count, newCount);
        }

        [HubMethodName(HubEvents.Reset)]
        public Task Reset()
        {
            var newCount = _stateManager.ResetCount();

            return Clients.All.SendAsync(HubEvents.Count, newCount);
        }
    }
}

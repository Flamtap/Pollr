using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Pollr.Server.Common;
using Pollr.Server.Services;

namespace Pollr.Server.Hubs
{
    public class CountHub : Hub<ICountHub>
    {
        private readonly StateManager _stateManager;

        public CountHub(StateManager stateManager)
        {
            _stateManager = stateManager;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.UserJoinedAsync(Context.ConnectionId);

            await Clients.Caller.UpdateCountAsync(_stateManager.GetCount());
        }

        [HubMethodName(HubEvents.Count)]
        public Task Count()
        {
            var newCount = _stateManager.BumpCount();

            return Clients.All.UpdateCountAsync(newCount);
        }

        [HubMethodName(HubEvents.Reset)]
        public Task Reset()
        {
            var newCount = _stateManager.ResetCount();

            return Clients.All.UpdateCountAsync(newCount);
        }
    }
}

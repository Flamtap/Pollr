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

        public override Task OnConnectedAsync()
        {
            return Task.WhenAll(
                Clients.Caller.SendAsync(HubEvents.Count, _stateManager.GetCount()),
                Clients.All.SendAsync(HubEvents.Message, $"{Context.ConnectionId} has joined the party!"));
        }

        [HubMethodName(HubEvents.Count)]
        public Task Count()
        {
            var oldCount = _stateManager.GetCount();
            var newCount = _stateManager.BumpCount();

            return Task.WhenAll(
                Clients.All.SendAsync(HubEvents.Count, newCount),
                Clients.All.SendAsync(HubEvents.Message,
                    $"{Context.ConnectionId} incremented the from {oldCount} to {newCount}!"));
        }

        [HubMethodName(HubEvents.Reset)]
        public Task Reset()
        {
            var newCount = _stateManager.ResetCount();

            return Task.WhenAll(
                Clients.All.SendAsync(HubEvents.Count, newCount),
                Clients.All.SendAsync(HubEvents.Message, $"{Context.ConnectionId} reset the count!"));
        }
    }
}

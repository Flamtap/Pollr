using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Pollr.Server.Common;
using Pollr.Server.Services;

namespace Pollr.Server.Hubs
{
    public class CountHub : Hub
    {
        private readonly CountManager _countManager;

        public CountHub(CountManager countManager)
        {
            _countManager = countManager;
        }

        public override Task OnConnectedAsync()
        {
            return Task.WhenAll(
                Clients.Caller.SendAsync(CountHubEvents.Count, _countManager.GetCount()),
                Clients.All.SendAsync(CountHubEvents.Message, $"{Context.ConnectionId} has joined the party!"));
        }

        [HubMethodName(CountHubEvents.Count)]
        public Task Count()
        {
            var oldCount = _countManager.GetCount();
            var newCount = _countManager.BumpCount();

            return Task.WhenAll(
                Clients.All.SendAsync(CountHubEvents.Count, newCount),
                Clients.All.SendAsync(CountHubEvents.Message,
                    $"{Context.ConnectionId} incremented the from {oldCount} to {newCount}!"));
        }

        [HubMethodName(CountHubEvents.Reset)]
        public Task Reset()
        {
            var newCount = _countManager.ResetCount();

            return Task.WhenAll(
                Clients.All.SendAsync(CountHubEvents.Count, newCount),
                Clients.All.SendAsync(CountHubEvents.Message, $"{Context.ConnectionId} reset the count!"));
        }
    }
}

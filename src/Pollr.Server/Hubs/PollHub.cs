using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Pollr.Server.Common;
using Pollr.Server.Services;

namespace Pollr.Server.Hubs
{
    public class PollHub : Hub
    {
        private readonly PollManager _pollManager;

        public PollHub(PollManager pollManager)
        {
            _pollManager = pollManager;
        }

        public override Task OnConnectedAsync()
        {
            return Task.WhenAll(
                Clients.Caller.SendAsync(PollHubEvents.Vote, _pollManager.GetVotes()),
                Clients.All.SendAsync(PollHubEvents.Message, $"👋 {Context.ConnectionId} has joined the party!"));
        }

        [HubMethodName(PollHubEvents.Vote)]
        public Task Vote(string value)
        {
            var votes = _pollManager.GetVotes();

            if (votes.Any(v => v.Voter == Context.ConnectionId))
            {
                return Clients.All.SendAsync(PollHubEvents.Message,
                    $"😡 {Context.ConnectionId} just tried to vote twice!");
            }

            _pollManager.Vote(new Vote(Context.ConnectionId, value));

            return Task.WhenAll(
                Clients.All.SendAsync(PollHubEvents.Vote, _pollManager.GetVotes()),
                Clients.All.SendAsync(PollHubEvents.Message, $"📊 {Context.ConnectionId} voted for {value}!"));
        }

        [HubMethodName(PollHubEvents.Reset)]
        public Task Reset()
        {
            _pollManager.Reset();

            return Task.WhenAll(
                Clients.All.SendAsync(PollHubEvents.Vote, Enumerable.Empty<Vote>()),
                Clients.All.SendAsync(PollHubEvents.Message, $"🔃 {Context.ConnectionId} reset the poll!"));
        }
    }
}

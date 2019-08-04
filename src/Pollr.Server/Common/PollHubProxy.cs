using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Pollr.Server.Common
{
    public class PollHubProxy
    {
        private readonly HubConnection _connection;

        public PollHubProxy(HubConnection connection)
        {
            _connection = connection;
        }

        public Task ConnectAsync(Action<IEnumerable<Vote>> voteCallback, Action<string> messageCallback)
        {
            if (_connection.State == HubConnectionState.Connected)
                return Task.CompletedTask;

            _connection.On(PollHubEvents.Vote, voteCallback);
            _connection.On(PollHubEvents.Message, messageCallback);

            return _connection.StartAsync();
        }

        public Task VoteAsync(string value)
        {
            return _connection.SendAsync(PollHubEvents.Vote, value);
        }

        public Task ResetPollAsync()
        {
            return _connection.SendAsync(PollHubEvents.Reset);
        }
    }
}

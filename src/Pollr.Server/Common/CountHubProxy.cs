using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Pollr.Server.Common
{
    public class CountHubProxy
    {
        private readonly HubConnection _connection;

        public CountHubProxy(HubConnection connection)
        {
            _connection = connection;
        }

        public Task ConnectAsync(
            Action<string> userJoinedCallback,
            Action<int> countCallback)
        {
            _connection.On(HubEvents.UserJoined, userJoinedCallback);
            _connection.On(HubEvents.Count, countCallback);

            return _connection.StartAsync();
        }

        public Task BumpCountAsync()
        {
            return _connection.SendAsync(HubEvents.Count);
        }

        public Task ResetCountAsync()
        {
            return _connection.SendAsync(HubEvents.Reset);
        }
    }
}

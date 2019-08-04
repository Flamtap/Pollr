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
            Action<int> countCallback,
            Action<string> messageCallback)
        {
            _connection.On(HubEvents.Count, countCallback);
            _connection.On(HubEvents.Message, messageCallback);

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

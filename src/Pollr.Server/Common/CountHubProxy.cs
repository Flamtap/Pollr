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

        public Task ConnectAsync(Action<int> countCallback, Action<string> messageCallback)
        {

            if (_connection.State == HubConnectionState.Connected)
                return Task.CompletedTask;

            _connection.On(CountHubEvents.Count, countCallback);
            _connection.On(CountHubEvents.Message, messageCallback);

            return _connection.StartAsync();
        }

        public Task BumpCountAsync()
        {
            return _connection.SendAsync(CountHubEvents.Count);
        }

        public Task ResetCountAsync()
        {
            return _connection.SendAsync(CountHubEvents.Reset);
        }
    }
}

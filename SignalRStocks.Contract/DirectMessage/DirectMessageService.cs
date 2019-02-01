using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRStocks.Contract.DirectMessage
{
    public class DirectMessageService
    {
        public event Action<DirectMessage> DirectMessageReceived;

        HubConnection connection;

        public DirectMessageService(string name)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/DirectMessageHub").Build();

            connection.Closed += OnConnectionClosed;

            connection.On<DirectMessage>(nameof(IDirectMessageClient.ReceiveMessage), (message) =>
            {
                DirectMessageReceived?.Invoke(message);
            });
        }

        public async Task StartConnectionAsync()
        {
            await connection.StartAsync();
        }

        public async Task StopConnectionAsync()
        {
            await connection.StopAsync();
        }

        public async Task SendMessageAsync(string to, string message)
        {
            if (connection.State != HubConnectionState.Connected)
                await StartConnectionAsync();

            var request = new DirectMessageRequest
            {
                To = to,
                Message = message
            };

            await connection.SendAsync(nameof(IDirectMessageHub.SendDirectMessage), request);
        }

        async Task OnConnectionClosed(Exception e)
        {
            await Task.Delay(3000);
            await connection.StartAsync();
        }
    }
}

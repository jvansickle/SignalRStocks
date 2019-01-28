using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRStocks.Contract.Discussion
{
    public class DiscussionService
    {
        public event Action<string> MessageReceived;

        HubConnection connection;

        public DiscussionService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/DiscussionHub")
                .Build();

            connection.Closed += OnConnectionClosed;

            connection.On<string>(nameof(IDiscussionClient.ReceiveMessage), (message) =>
            {
                MessageReceived?.Invoke(message);
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

        public async Task SendMessage(string message)
        {
            if (connection.State != HubConnectionState.Connected)
                await StartConnectionAsync();

            await connection.SendAsync(nameof(IDiscussionHub.SendMessage), message);
        }

        async Task OnConnectionClosed(Exception e)
        {
            await Task.Delay(3000);
            await connection.StartAsync();
        }
    }
}

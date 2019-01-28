using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRStocks.Contract.Discussion
{
    public class DiscussionService
    {
        public event Action<string> MessageRecieved;

        HubConnection connection;

        public DiscussionService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/DiscussionHub")
                .Build();

            connection.On<string>(nameof(IDiscussionClient.ReceiveMessage), (message) =>
            {
                MessageRecieved?.Invoke(message);
            });
        }

        public async Task StartConnection()
        {
            await connection.StartAsync();
        }

        public async Task StopConnection()
        {
            await connection.StopAsync();
        }

        public async Task SendMessage(string message)
        {
            if (connection.State != HubConnectionState.Connected)
                await StartConnection();

            await connection.SendAsync(nameof(IDiscussionHub.SendMessage), message);
        }

        async Task OnConnectionClosed(Exception e)
        {
            await Task.Delay(3000);
            await connection.StartAsync();
        }
    }
}

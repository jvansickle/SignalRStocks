using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRStocks.Contract.Group
{
    public class GroupService
    {
        public event Action<string, string> ReceiveMessage;

        HubConnection connection;

        public GroupService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/GroupHub")
                .Build();

            connection.Closed += HandleConnectionClosedAsync;

            connection.On<string, string>(nameof(IGroupClient.ReceiveMessage), (groupName, message) =>
            {
                ReceiveMessage?.Invoke(groupName, message);
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

        public async Task JoinGroupAsync(string groupName)
        {
            await connection.SendAsync(nameof(IGroupHub.JoinGroup), groupName);
        }

        public async Task LeaveGroupAsync(string groupName)
        {
            await connection.SendAsync(nameof(IGroupHub.LeaveGroup), groupName);
        }

        public async Task SendMessageToGroupAsync(string groupName, string message)
        {
            var request = new GroupHubMessageRequest
            {
                GroupName = groupName,
                Message = message
            };

            await connection.SendAsync(nameof(IGroupHub.SendMessage), request);
        }

        async Task HandleConnectionClosedAsync(Exception arg)
        {
            Thread.Sleep(3000);
            await connection.StartAsync();
        }
    }
}

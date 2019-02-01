using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRStocks.Contract.Group;

namespace SignalRStocks.Web.Hubs
{
    public class GroupHub : Hub<IGroupClient>, IGroupHub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveMessage(groupName, $"A user has joined group {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).ReceiveMessage(groupName, $"A user has left group {groupName}");
        }

        public async Task SendMessage(GroupHubMessageRequest request)
        {
            await Clients.Group(request.GroupName).ReceiveMessage(request.GroupName, request.Message);
        }
    }
}

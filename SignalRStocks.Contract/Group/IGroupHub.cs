using System.Threading.Tasks;

namespace SignalRStocks.Contract.Group
{
    public interface IGroupHub
    {
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
        Task SendMessage(GroupHubMessageRequest request);
    }
}

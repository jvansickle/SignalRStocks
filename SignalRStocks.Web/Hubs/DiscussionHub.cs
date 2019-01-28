using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRStocks.Contract.Discussion;

namespace SignalRStocks.Web.Hubs
{
    public class DiscussionHub : Hub<IDiscussionClient>, IDiscussionHub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}

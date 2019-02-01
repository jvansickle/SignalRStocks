using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRStocks.Contract.DirectMessage;

namespace SignalRStocks.Web.Hubs
{
    public class DirectMessageHub : Hub<IDirectMessageClient>, IDirectMessageHub
    {
        public async Task SendDirectMessage(DirectMessageRequest request)
        {
            var message = new DirectMessage
            {
                From = Context.UserIdentifier,
                Message = request.Message
            };

            await Clients.User(request.To).ReceiveMessage(message);
        }
    }
}

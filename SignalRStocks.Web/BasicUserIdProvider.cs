using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace SignalRStocks.Web
{
    public class BasicUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRStocks.Contract.Stock;

namespace SignalRStocks.Web.Hubs
{
    public class StockHub : Hub<IStockClient>
    {
        internal async Task SendStockInfo(Stock stock, double value)
        {
            await Clients.All.ReceiveStockValue(stock, value);
        }
    }
}

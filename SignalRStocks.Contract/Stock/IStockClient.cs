using System.Threading.Tasks;

namespace SignalRStocks.Contract.Stock
{
    public interface IStockClient
    {
        Task ReceiveStockValue(Stock stock, double value);
    }
}

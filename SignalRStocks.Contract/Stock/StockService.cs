using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRStocks.Contract.Stock
{
    public class StockService
    {
        public event Action<Stock, double> StockValueReceived;

        HubConnection connection;

        public StockService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/StockHub")
                .Build();

            connection.Closed += HandleConnectionClosed;

            connection.On<Stock, double>(nameof(IStockClient.ReceiveStockValue), HandleStockValueReceived);
        }

        public async Task StartConnectionAsync()
        {
            await connection.StartAsync();
        }

        public async Task StopConnectionAsync()
        {
            await connection.StopAsync();
        }

        void HandleStockValueReceived(Stock stock, double value)
        {
            StockValueReceived?.Invoke(stock, value);
        }

        async Task HandleConnectionClosed(Exception e)
        {
            await Task.Delay(3000);
            await connection.StartAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRStocks.Contract.Stock;
using SignalRStocks.Web.Hubs;

namespace SignalRStocks.Web.Services
{
    public class StockGeneratorService
    {
        readonly Random rand;
        readonly CancellationToken cancellationToken;
        readonly Task generatorTask;
        readonly IHubContext<StockHub, IStockClient> stockHubContext;

        public StockGeneratorService(IHubContext<StockHub, IStockClient> stockHubContext)
        {
            rand = new Random();
            cancellationToken = new CancellationToken();
            this.stockHubContext = stockHubContext;
            generatorTask = Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Thread.Sleep(2500);
                    var newInfo = GenerateStockInfo();
                    newInfo.Keys.ToList().ForEach(async stockKey =>
                    {
                        await stockHubContext.Clients.All.ReceiveStockValue(stockKey, newInfo[stockKey]);
                    });
                }
            }, cancellationToken);
        }

        Dictionary<Stock, double> GenerateStockInfo()
        {
            var dict = new Dictionary<Stock, double>();

            foreach (Stock stock in Enum.GetValues(typeof(Stock)))
            {
                dict.Add(stock, rand.NextDouble() * 100);
            }

            return dict;
        }
    }
}

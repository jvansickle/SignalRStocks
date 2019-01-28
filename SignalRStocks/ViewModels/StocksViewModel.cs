using System;
using System.Collections.ObjectModel;
using System.Linq;
using SignalRStocks.Contract.Stock;
using SignalRStocks.Models;

namespace SignalRStocks.ViewModels
{
    public class StocksViewModel : ViewModel
    {
        ObservableCollection<StockInfo> _stocks;
        public ObservableCollection<StockInfo> Stocks
        {
            get => _stocks;
            set
            {
                _stocks = value;
                NotifyPropertyChanged();
            }
        }

        private readonly StockService stockService;

        public StocksViewModel(StockService stockService)
        {
            Stocks = new ObservableCollection<StockInfo>();
            this.stockService = stockService;
            this.stockService.StockValueReceived += HandleStockValueReceived;
            this.stockService.StartConnectionAsync();
        }

        void HandleStockValueReceived(Stock stock, double value)
        {
            var foundStockInfo = Stocks.FirstOrDefault(si => si.Stock == stock);

            if(foundStockInfo != null)
            {
                var index = Stocks.IndexOf(foundStockInfo);
                Stocks.RemoveAt(index);
                Stocks.Insert(index, new StockInfo { Stock = stock, Value = value });
            }
            else
            {
                Stocks.Add(new StockInfo { Stock = stock, Value = value });
            }
        }
    }
}

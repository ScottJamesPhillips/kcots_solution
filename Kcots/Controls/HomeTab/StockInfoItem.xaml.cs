using Kcots.Configuration;
using Kcots.Data;
using Kcots.Interfaces;
using Kcots.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;

namespace Kcots.Controls.HomeTab
{
    /// <summary>
    /// Interaction logic for StockInfoItem.xaml
    /// </summary>
    public partial class StockInfoItem : UserControl
    {
        Stocks selectedStock = null;
        LoggingWrapper logger = new LoggingWrapper();
        List<StocksMarketData> stockData = new List<StocksMarketData>();
        public StockInfoItem()
        {
            InitializeComponent();
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //On change datacontext (change stock selected)
            selectedStock = e.NewValue as Stocks;
            if (selectedStock != null)
            {
                GetCurrentStockInfo();
                candleStickChart.DataContext = stockData;
            }
        }

        private async void GetCurrentStockInfo()
        {
            try
            {
                //StocksMarketDataApiResponse stockDataApiResponse = await new DataAccess(logger, httpWrapper).GetMarketDataForStock(selectedStock.Symbol);
                var stockDataApiResponse = await Settings.serviceProvider.GetService<IDataAccess>().GetMarketDataForStock(selectedStock.Symbol);
                stockData = stockDataApiResponse.Values;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting current stock info");
                throw;
            }

        }
    }
}

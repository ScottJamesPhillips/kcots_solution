using Kcots.Configuration;
using Kcots.Data;
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

namespace Kcots.Controls.HomeTab
{
    /// <summary>
    /// Interaction logic for StockInfoItem.xaml
    /// </summary>
    public partial class StockInfoItem : UserControl
    {
        Stocks selectedStock = null;
        ILogger logger = Logging.logger;
        public StockInfoItem()
        {
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //On change datacontext (change stock selected)
            selectedStock = e.NewValue as Stocks;
            if (selectedStock != null)
                GetCurrentStockInfo();
        }

        private async void GetCurrentStockInfo()
        {
            try
            {
                StocksMarketDataApiResponse stockDataApiResponse = await new DataAccess(logger).GetMarketDataForStock(selectedStock.Symbol);
                List<StocksMarketData> stockData = stockDataApiResponse.Values;
            }
            catch (Exception ex)
            {
                Logging.WriteLog(ex.Message, Logging.LogType.error);
                throw;
            }

        }
    }
}

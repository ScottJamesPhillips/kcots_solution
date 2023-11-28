using Kcots.Configuration;
using Kcots.Models;
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
        public StockInfoItem()
        {
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Execute your function or code here when DataContext changes
            // The new DataContext is accessible through e.NewValue
            selectedStock = e.NewValue as Stocks;
            if (selectedStock != null)
                GetCurrentStockInfo();
        }

        private async void GetCurrentStockInfo()
        {
            try
            {
                // Implement your logic here
                Logging.WriteLog($"Fetching stock info for {selectedStock.Symbol}", Logging.LogType.info);
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                //Task<List<StocksMarketData>> pollingTask = Data.DataAccess.GetMarketDataPeriodically(selectedStock.Symbol, TimeSpan.FromMinutes(1), cancellationTokenSource.Token);
                // To stop polling after a certain time or when needed
                // cancellationTokenSource.Cancel();

                // Wait for the task to complete
                //List<StocksMarketData> result = await pollingTask;

            }
            catch (Exception ex)
            {
                Logging.WriteLog(ex.Message, Logging.LogType.error);
                throw;
            }

        }
    }
}

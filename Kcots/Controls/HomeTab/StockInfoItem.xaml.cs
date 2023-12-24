﻿using Kcots.Configuration;
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
        public Stocks selectedStock = null;
        public LoggingWrapper lw = new LoggingWrapper();
        public List<StocksMarketData> stockData = new List<StocksMarketData>();
        public ILoggingWrapper logger;
        public IHttpClientWrapper httpClient;
        public IDataAccess dataAccess;
        public StockInfoItem()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            Utilities.Data.LoadGraphicIPMappings();
            cboBoxAPIInterval.ItemsSource = Utilities.Data.APIIntervalOptions;
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

        public async void GetCurrentStockInfo()
        {
            try
            {
                //StocksMarketDataApiResponse stockDataApiResponse = await new DataAccess(logger, httpWrapper).GetMarketDataForStock(selectedStock.Symbol);
                var stockDataApiResponse = await Settings.serviceProvider.GetService<IDataAccess>().GetMarketDataForStock(selectedStock.Symbol);
                stockData = stockDataApiResponse.Values;
            }
            catch (Exception ex)
            {
                lw.LogError(ex, "Error getting current stock info");
                //throw;
            }
        }
    }
}

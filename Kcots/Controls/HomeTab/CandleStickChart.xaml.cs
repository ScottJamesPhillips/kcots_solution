using Kcots.Models;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Interaction logic for CandleStickChart.xaml
    /// </summary>
    public partial class CandleStickChart : UserControl
    {
        static List<StocksMarketData> marketData = new List<StocksMarketData>();

        public CandleStickChart()
        {
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            marketData = e.NewValue as List<StocksMarketData>;
            if (marketData != null)
            {
                InitializeCandleSeries();
            }
        }

        private void InitializeCandleSeries()
        {
            try
            {
                //Clear exisitng chart series
                candleStickChart.Series = new ChartSeriesCollection();
                CandleSeries series = new CandleSeries()
                {
                    ItemsSource = marketData,
                    XBindingPath = "DateTime",
                    High = "High",
                    Low = "Low",
                    Open = "Open",
                    Close = "Close"
                };
                // Add the series to the chart
                candleStickChart.Series.Add(series);
                //series.ComparisonMode = Syncfusion.UI.Xaml.Charts.FinancialPrice.Open;
            }
            catch(Exception ex)
            {

            }
        }
    }
}

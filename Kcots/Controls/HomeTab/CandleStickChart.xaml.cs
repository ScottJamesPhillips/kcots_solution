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
        CandleSeries series = new CandleSeries()
        {

            ItemsSource = marketData,

            XBindingPath = "DateTime",

            High = "High",
            Low = "Low",

            Open = "Open",
            Close = "Close",

            Interior = new SolidColorBrush(Color.FromRgb(0x4A, 0x4A, 0X4A))

        };
        public CandleStickChart()
        {
            InitializeComponent();
            //DataContext = new ViewModel();
        }

        public class CandleChartModel
        {
            public DateTime Date { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Open { get; set; }
            public double Close { get; set; }
        }

        public class ViewModel
        {
            public ViewModel()
            {
                this.StockPriceDetails = new ObservableCollection<CandleChartModel>();

                //this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(2), Open = 861, High = 868.4, Low = 835.2, Close = 843.45 });
                //this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(3), Open = 846.15, High = 853, Low = 838.5, Close = 847.5 });
                //this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(4), Open = 846, High = 860.75, Low = 841, Close = 855 });
                //this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(5), Open = 841, High = 845, Low = 827.85, Close = 838.65 });
                //this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(6), Open = 846, High = 874.5, Low = 841, Close = 860.75 });
                //this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(7), Open = 865, High = 872, Low = 865, Close = 868.9 });

                foreach(StocksMarketData md in marketData)
                {
                    this.StockPriceDetails.Add(new CandleChartModel() { Date = md.DateTime, Open = decimal.ToDouble(md.Open), High = decimal.ToDouble(md.High), Low = decimal.ToDouble(md.Low), Close = decimal.ToDouble(md.Close) });
                }
            }
            public ObservableCollection<CandleChartModel> StockPriceDetails { get; set; }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //On change datacontext (change stock selected)
            marketData = e.NewValue as List<StocksMarketData>;
            if (marketData != null)
            {
                DataContext = new ViewModel();
            }
            //foreach (StocksMarketData s in marketData)
            //{

            //}

            //if (selectedStock != null)
            //    GetCurrentStockInfo();
        }
    }
}

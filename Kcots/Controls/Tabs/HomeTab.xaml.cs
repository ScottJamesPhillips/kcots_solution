using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
using CsvHelper;
using Kcots.Configuration;
using Kcots.Data;
using Kcots.Interfaces;
using Kcots.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kcots.Controls.Tabs
{
    /// <summary>
    /// Interaction logic for HomeTab.xaml
    /// </summary>
    public partial class HomeTab : UserControl
    {
        List<Stocks> stocks = new List<Stocks>();
        LoggingWrapper lw = new LoggingWrapper();      
        public HomeTab()
        {
            InitializeComponent();
            Init();
        }
        public async void  Init()
        {
            try
            {
                lw.InitializeLogger();
                lw.LogInformation("Initialising Program");
                Settings settings = new Settings(lw);
                var stocks = await Settings.serviceProvider.GetService<IDataAccess>().GetStocks();

                DataContext = stocks;
            }catch(Exception ex)
            {
                lw.LogError(ex, "Error getting stocks");
            }
        }


        private void ItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change
            // You can access the selected item and update the TextBlock accordingly
            stocksInfoItem.DataContext = (e.AddedItems[0] as Stocks);

        }
    }
}

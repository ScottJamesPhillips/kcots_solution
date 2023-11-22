using System;
using System.Collections.Generic;
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
using Kcots.Data;
using Kcots.Models;

namespace Kcots.Controls.Tabs
{
    /// <summary>
    /// Interaction logic for HomeTab.xaml
    /// </summary>
    public partial class HomeTab : UserControl
    {
        List<Stocks> stocks = new List<Stocks>();
        public HomeTab()
        {
            InitializeComponent();
            Init();
        }
        public async void  Init()
        {
            List<Stocks> y = new List<Stocks>();
            //Fetching stocks thread
            await Task.Run(async () =>
            {
                 y = await DataAccess.GetStocks();
            });
            //dgStockList.ItemsSource = y;
            DataContext = y;
        }
    }
}

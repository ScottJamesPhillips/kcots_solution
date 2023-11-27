using Kcots.Configuration;
using Kcots.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StockInfoItem.xaml
    /// </summary>
    public partial class StockInfoItem : UserControl
    {
        Stocks selectedStock = null;
        public StockInfoItem()
        {
            InitializeComponent();
            //Init();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Execute your function or code here when DataContext changes
            // The new DataContext is accessible through e.NewValue
            selectedStock = e.NewValue as Stocks;
            YourFunctionToExecute();
        }

        private void YourFunctionToExecute()
        {
            // Implement your logic here
            Console.WriteLine("DataContext changed! Your code here.");
        }
    }
}

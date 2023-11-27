﻿using System;
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
            try
            {
                Logging.WriteLog("Initialising Program", Logging.LogType.info);
                //Fetching stocks thread
                await Task.Run(async () =>
                {
                    stocks = await DataAccess.GetStocks();
                });
                DataContext = y;
            }catch(Exception ex)
            {
                Logging.WriteLog(ex.Message, Logging.LogType.error);
            }
        }


        private void ItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change
            // You can access the selected item and update the TextBlock accordingly
            txtBlock_CurrentStock.Text = (e.AddedItems[0] as Stocks)?.Name;
        }

    }
}

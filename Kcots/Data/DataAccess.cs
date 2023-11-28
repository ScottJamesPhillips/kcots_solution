using CsvHelper;
using Kcots.Configuration;
using Kcots.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kcots.Data
{
    class DataAccess
    {
        private static string avApiKey = "4X0XTW8QRBNKICTL";
        private static string fhApiKey = "cliufv9r01qsgccbkkjgcliufv9r01qsgccbkkk0";

        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<Stocks>> GetStocks()
        {
            try
            {
                Logging.WriteLog("Getting Stocks List", Logging.LogType.info);
                List<Stocks> returnList = new List<Stocks>();

                using (HttpClient client = new HttpClient())
                {
                    string url = "https://finnhub.io/api/v1/stock/symbol?exchange=US&mic=BATS&token=cliufv9r01qsgccbkkjgcliufv9r01qsgccbkkk0";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        returnList = JsonConvert.DeserializeObject<List<Stocks>>(jsonString);
                    }
                }

                return returnList;
            }
            catch (Exception ex)
            {
                Logging.WriteLog(ex.Message, Logging.LogType.error);
                return new List<Stocks>();
            }
        }

        public static async Task<StocksMarketData> GetMarketDataPeriodically(string symbol, TimeSpan pollingInterval, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    StocksMarketData returnList = new StocksMarketData();
                    using (HttpClient client = new HttpClient())
                    {
                        string url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token=cliufv9r01qsgccbkkjgcliufv9r01qsgccbkkk0";
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        if (response != null)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            returnList = JsonConvert.DeserializeObject<StocksMarketData>(jsonString);
                        }
                    }
                    //List<StocksMarketData> monthlyPrices = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token=cliufv9r01qsgccbkkjgcliufv9r01qsgccbkkk0"
                    //                .GetStringFromUrl().FromCsv<List<StocksMarketData>>();

                    // Process the data or store it as needed

                    // Sleep for the specified interval
                    //await Task.Delay(pollingInterval, cancellationToken);
                }
                catch (Exception ex)
                {
                    Logging.WriteLog(ex.Message, Logging.LogType.error);
                    // Handle the error as needed
                }
            }

            return null; // Or some default value
        }

    }
}

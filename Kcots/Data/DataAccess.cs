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

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api.twelvedata.com/stocks?exchange=NASDAQ"),
                    //RequestUri = new Uri("https://api.twelvedata.com/time_series?apikey=d6db85bcf8dc434ea2adf66e8dda1192&interval=1min&type=stock"),    
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<ApiResponse>(responseJsonString);
                    returnList = responseJson.Data;
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
                    List<StocksMarketData> monthlyPrices = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token=cliufv9r01qsgccbkkjgcliufv9r01qsgccbkkk0"
                                    .GetStringFromUrl().FromCsv<List<StocksMarketData>>();

                    // Process the data or store it as needed

                    // Sleep for the specified interval
                    await Task.Delay(pollingInterval, cancellationToken);
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

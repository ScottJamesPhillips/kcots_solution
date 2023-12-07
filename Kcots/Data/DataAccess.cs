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
                    RequestUri = new Uri("https://api.twelvedata.com/stocks?exchange=NYSE&mic_code=ARCX"),
                    //RequestUri = new Uri("https://api.twelvedata.com/stocks?exchange=NASDAQ"),
                    //RequestUri = new Uri("https://api.twelvedata.com/time_series?apikey=d6db85bcf8dc434ea2adf66e8dda1192&interval=1min&type=stock"),    
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<StocksApiResponse>(responseJsonString);
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

        public static async Task<List<StocksMarketData>> GetMarketDataForStock(string symbol)
        {
            List<StocksMarketData> returnList = new List<StocksMarketData>();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.twelvedata.com/time_series?apikey=d6db85bcf8dc434ea2adf66e8dda1192&interval=1min&format=JSON&symbol={symbol}"),
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    StocksMarketDataApiResponse responseJson = JsonConvert.DeserializeObject<StocksMarketDataApiResponse>(responseJsonString);
                    returnList = responseJson.Values;
                }
                // Or some default value
                return returnList;
            }catch(Exception ex)
            {
                Logging.WriteLog(ex.Message, Logging.LogType.error);
                return null;
                // Handle the error as needed
            }
        }

    }
}

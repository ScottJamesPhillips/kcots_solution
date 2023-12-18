using CsvHelper;
using Kcots.Configuration;
using Kcots.Interfaces;
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
    public class DataAccess:IDataAccess
    {


        private readonly ILoggerWrapper logger;
        private readonly IHttpClientWrapper httpClient;

        public DataAccess(ILoggerWrapper logger, IHttpClientWrapper httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }


        public async Task<List<Stocks>> GetStocks()
        {
            try
            {
                logger.LogInformation("Getting Stocks List");

                List<Stocks> returnList = new List<Stocks>();

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api.twelvedata.com/stocks?exchange=NYSE&mic_code=ARCX"),
                };

                using (var response = await httpClient.SendAsync(request))
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
                logger.LogError(ex, "Error getting stocks");
                return new List<Stocks>();
            }
        }

        public async Task<StocksMarketDataApiResponse> GetMarketDataForStock(string symbol)
        {
            try
            {

                logger.LogInformation("Getting Stocks List");
                StocksMarketDataApiResponse returnObject = new StocksMarketDataApiResponse();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.twelvedata.com/time_series?apikey=d6db85bcf8dc434ea2adf66e8dda1192&interval=1day&format=JSON&symbol={symbol}"),
                };
                using (var response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var responseJsonString = await response.Content.ReadAsStringAsync();
                    returnObject = JsonConvert.DeserializeObject<StocksMarketDataApiResponse>(responseJsonString);
                }
                return returnObject;
            }
            catch(Exception ex)
            {
                logger.LogError(ex,ex.Message);
                return new StocksMarketDataApiResponse();
            }
        }
    }
}

using CsvHelper;
using Kcots.Configuration;
using Kcots.Models;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Data
{
    class DataAccess
    {
        private static string avApiKey = "4X0XTW8QRBNKICTL";


        public static async Task<List<Stocks>> GetStocks()
        {
            try
            {
                Logging.WriteLog("Getting Stocks List", Logging.LogType.info);
                List<Stocks> returnList = new List<Stocks>();
                // replace the "demo" apikey below with your own key from https://www.alphavantage.co/support/#api-key
                //string QUERY_URL = "https://www.alphavantage.co/query?function=LISTING_STATUS&apikey=4X0XTW8QRBNKICTL";
                string QUERY_URL = "https://www.alphavantage.co/query?function=LISTING_STATUS&date=2010-01-01&state=delisted&limit=10&apikey=4X0XTW8QRBNKICTL";

                Uri queryUri = new Uri(QUERY_URL);

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US"); ;
                using (WebClient client = new WebClient())
                {
                    //Default timeout 100 seconds (appaz)?
                    using (MemoryStream stream = new MemoryStream(await client.DownloadDataTaskAsync(queryUri)))
                    {
                        stream.Position = 0;

                        using (StreamReader reader = new StreamReader(stream))
                        {
                            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                                csv.Read();
                                csv.ReadHeader();
                                while (csv.Read())
                                {
                                    returnList.Add(new Stocks(csv.Parser.Record));
                                }
                            }
                        }
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

        public static async Task<List<StocksMarketData>> GetMarketData(string symbol)
        {
            try
            {
                Logging.WriteLog("Getting Stock Info", Logging.LogType.info);
                List<StocksMarketData> monthlyPrices = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey={avApiKey}&datatype=csv"
                                .GetStringFromUrl().FromCsv<List<StocksMarketData>>();

                return monthlyPrices;
            }
            catch (Exception ex)
            {
                Logging.WriteLog(ex.Message, Logging.LogType.error);
                return null;
            }
        }
    }
}

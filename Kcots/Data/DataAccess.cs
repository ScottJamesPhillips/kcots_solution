using CsvHelper;
using Kcots.Models;
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
                List<Stocks> returnList = new List<Stocks>();
                // replace the "demo" apikey below with your own key from https://www.alphavantage.co/support/#api-key
                //string QUERY_URL = "https://www.alphavantage.co/query?function=LISTING_STATUS&apikey=4X0XTW8QRBNKICTL";
                string QUERY_URL = "https://www.alphavantage.co/query?function=LISTING_STATUS&date=2022-07-10&state=delisted&apikey=4X0XTW8QRBNKICTL";

                Uri queryUri = new Uri(QUERY_URL);

                // print the output
                // This example uses the fine nuget package CsvHelper (https://www.nuget.org/packages/CsvHelper/)

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
                                //Populate a List of Firms
                                //Console.WriteLine(string.Join("\t", csv.HeaderRecord));
                                while (csv.Read())
                                {
                                    returnList.Add(new Stocks(csv.Parser.Record));
                                    //    Console.WriteLine(string.Join("\t", csv.Parser.Record));

                                }
                            }
                        }
                    }
                }
                return returnList;
            }
            catch (Exception ex)
            {
                return new List<Stocks>();
            }
        }

        public static async Task<List<StocksMarketData>> GetMarketData(string symbol)
        {
            try
            {
                List<StocksMarketData> monthlyPrices = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey={avApiKey}&datatype=csv"
                                .GetStringFromUrl().FromCsv<List<StocksMarketData>>();

                return monthlyPrices;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

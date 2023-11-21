using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Models
{
    public class AlphaVantageData
    {
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }

        public decimal High { get; set; }
        public decimal Low { get; set; }

        public decimal Close { get; set; }
        public decimal Volume { get; set; }

        public static List<AlphaVantageData> GetMarketData()
        {
            try
            {
                var symbol = "MSFT";
                string avApiKey = "4X0XTW8QRBNKICTL";
                List<AlphaVantageData> monthlyPrices = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey={avApiKey}&datatype=csv"
                                .GetStringFromUrl().FromCsv<List<AlphaVantageData>>();

                return monthlyPrices;
            }
            catch (Exception ex) {
                return null;
            }
        }

    }
}

using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Models
{
    class ApiResponse
    {
        [JsonProperty("data")]
        public List<Stocks> Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
    class Stocks
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("mic_code")]
        public string MarketIdentifierCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        //public string Symbol { get; set; }
        //public string Name { get; set; }
        //public string Exchange { get; set; }
        //public string AssetType { get; set; }
        //public DateTime? IpoDate { get; set; }
        //public DateTime? DelistingDate { get; set; }
        //public string Status { get; set; }
        //public List<StocksMarketData> StockInfo { get; set; }
        //public Stocks()
        //{
        //    Symbol = result[0];
        //    Name = result[1];
        //    Exchange = result[2];
        //    AssetType = result[3];
        //    IpoDate = String.IsNullOrEmpty(result[4])||result[4]=="null" ? (DateTime?)null : DateTime.Parse(result[4]);
        //    DelistingDate = String.IsNullOrEmpty(result[5]) || result[5] == "null" ? (DateTime?)null : DateTime.Parse(result[5]);
        //    Status = result[6];
        //}
    }
}

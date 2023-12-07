using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Models
{
    class StocksMarketDataApiResponse
    {
        [JsonProperty("meta")]
        public Stocks Meta { get; set; }

        [JsonProperty("values")]
        public List<StocksMarketData> Values { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
    public class StocksMarketData    {

        [JsonProperty("datetime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("open")]
        public decimal Open { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }


        //public decimal High { get; set; }
        //public decimal Low { get; set; }

        //public decimal Close { get; set; }
        //public decimal Volume { get; set; }

        //public StocksMarketData(DateTime ts, decimal open, decimal high, decimal low, decimal close, decimal volume)
        //{

        //}

    }
}

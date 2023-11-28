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
    public class StocksMarketData    {

        [JsonProperty("c")]
        public decimal CurrentPrice { get; set; }
        [JsonProperty("d")]
        public decimal Change { get; set; }
        [JsonProperty("dp")]
        public decimal PercentChange { get; set; }
        [JsonProperty("h")]
        public decimal DayHighPrice { get; set; }
        [JsonProperty("l")]
        public decimal DayLowPrice { get; set; }
        [JsonProperty("o")]
        public decimal DayOpenPrice { get; set; }
        [JsonProperty("pc")]
        public decimal PrevDayClosePrice { get; set; }

        //public decimal High { get; set; }
        //public decimal Low { get; set; }

        //public decimal Close { get; set; }
        //public decimal Volume { get; set; }

        //public StocksMarketData(DateTime ts, decimal open, decimal high, decimal low, decimal close, decimal volume)
        //{

        //}

    }
}

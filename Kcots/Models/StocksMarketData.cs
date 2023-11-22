using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Models
{
    public class StocksMarketData
    {
        public DateTime Timestamp { get; set; }
        public decimal Open { get; set; }

        public decimal High { get; set; }
        public decimal Low { get; set; }

        public decimal Close { get; set; }
        public decimal Volume { get; set; }

        public StocksMarketData(DateTime ts, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {

        }

    }
}

using CsvHelper;
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
    class Stocks
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string AssetType { get; set; }
        public DateTime? IpoDate { get; set; }
        public DateTime? DelistingDate { get; set; }
        public string Status { get; set; }

        public Stocks(string[] result)
        {
            //DateTime x;
            //string symbol, string exchange, string assetType, DateTime ipoDate, DateTime delistingDate, string status
            Symbol = result[0];
            Name = result[1];
            Exchange = result[2];
            AssetType = result[3];
            IpoDate = String.IsNullOrEmpty(result[4])||result[4]=="null" ? (DateTime?)null : DateTime.Parse(result[4]);
            DelistingDate = String.IsNullOrEmpty(result[5]) || result[5] == "null" ? (DateTime?)null : DateTime.Parse(result[5]);
            Status = result[6];
        }
    }
}

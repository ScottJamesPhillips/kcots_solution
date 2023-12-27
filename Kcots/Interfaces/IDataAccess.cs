using Kcots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Interfaces
{
    public interface IDataAccess
    {
        Task<List<Stocks>> GetStocks();
        Task<StocksMarketDataApiResponse> GetMarketDataForStock(string symbol, string interval="1day");
    }
}

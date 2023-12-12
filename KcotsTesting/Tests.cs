using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kcots;
using Kcots.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kcots.Configuration;
using Kcots.Data;
using Moq;
using Microsoft.Extensions.Logging;

namespace KcotsTesting
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public async Task GetStocks_ShouldReturnListOfStocks()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var stockService = new DataAccess(loggerMock.Object);
            // Act
            List<Stocks> x = await stockService.GetStocks();
            // Assert
            Assert.IsTrue((x != null) && x.Count > 0);
        }

        [TestMethod]
        public async Task GetMarketDataForStock_ShouldReturnNull()
        {
            var loggerMock = new Mock<ILogger>();
            var stockService = new DataAccess(loggerMock.Object);
            StocksMarketDataApiResponse x = await stockService.GetMarketDataForStock(null);
            Assert.IsTrue(x.Values==null);
        }

        [TestMethod]
        public async Task GetMarketDataForStock_ShouldReturnPopulatedListForSymbol()
        {
            var loggerMock = new Mock<ILogger>();
            var stockService = new DataAccess(loggerMock.Object);
            StocksMarketDataApiResponse x = await stockService.GetMarketDataForStock("AAPL");
            Assert.IsTrue(x.Values.Count != 0 && x.Meta.Symbol=="AAPL");
        }


    }

}

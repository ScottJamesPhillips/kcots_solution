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

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    //Console.WriteLine("Initializing");
        //}
        //[TestMethod]
        //public void EnsureGetStocksData()
        //{
        //    List<Stocks> foo = Kcots.Data.DataAccess.GetStocks();
        //    //Assert.IsTrue(x != new List<Stocks>());
        //    Console.Write(foo.Result);
        //}

        // Use [InternalsVisibleTo] to share internal methods with the unit test project.
        //public async Task DoLookupCommandImpl()
        //{
        //    Logging.InitializeLogger();

        //    var order = await Kcots.Data.DataAccess.GetStocks();

        //    // Close the search
        //    Console.WriteLine(order);
        //}

        //[TestMethod]
        //public async Task myTestMethod()
        //{
            //var logger = new Mock<ILogger<StockService>>();
            //var stockService = new StockService(logger.Object);
            //// Arrange
            //var logger = new ILogger<DataAccess>>();
            //var stockService = new DataAccess(logger.Object);

            // Act
            //await DataAccess.GetStocks();

            //// Assert
            //logger.Verify(x => x.LogInformation("Getting Stocks List"), Times.Once);
        //}

        [TestMethod]
        public async Task GetStocks_ShouldLogInformation()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var stockService = new DataAccess(loggerMock.Object);

            // Act
            var x = await stockService.GetStocks();

            // Assert
            //loggerMock.Verify(x => x.LogInformation("Getting Stocks List"), Times.Once);
        }

        //[TestMethod]
        //public async Task GetStocks_ShouldLogErrorOnException()
        //{
        //    // Arrange
        //    var logger = new Mock<ILogger>();
        //    var stockService = new StockService(logger.Object);

        //    // Simulate an exception during the execution of GetStocks
        //    var httpClient = new Mock<HttpClient>();
        //    httpClient.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>()))
        //        .Throws(new HttpRequestException("Simulated exception"));

        //    // Act
        //    await stockService.GetStocks();

        //    // Assert
        //    logger.Verify(x => x.LogError(It.IsAny<Exception>(), "Error getting stocks"), Times.Once);
        //}


    }

}

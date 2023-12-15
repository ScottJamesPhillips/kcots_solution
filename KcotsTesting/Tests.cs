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
using Kcots.Interfaces;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace KcotsTesting
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public async Task GetStocks_ShouldNotBeEmpty_WhenHttpResponseIsSuccessful()
        {
            // Arrange
            Mock<ILoggerWrapper> loggerMock = new Mock<ILoggerWrapper>();
            Mock<IHttpClientWrapper> httpClientMock = new Mock<IHttpClientWrapper>();
            DataAccess dataAccess = new DataAccess(loggerMock.Object, httpClientMock.Object);
            List<Stocks> expectedStocks = new List<Stocks> {new Stocks {}};
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var responseJsonString = JsonConvert.SerializeObject(new StocksApiResponse { Data = expectedStocks });
            httpResponse.Content = new StringContent(responseJsonString);
            httpClientMock.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>()))
                          .ReturnsAsync(httpResponse);

            // Act
            List<Stocks> result = await dataAccess.GetStocks();

            // Assert
            Assert.IsTrue(result.Count > 0);
            loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
            httpClientMock.Verify(client => client.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
        }

        [TestMethod]
        public async Task GetStocks_ShouldReturnEmptyList_WhenHttpResponseIsNotSuccessful()
        {
            Mock<ILoggerWrapper> loggerMock = new Mock<ILoggerWrapper>();
            Mock<IHttpClientWrapper> httpClientMock = new Mock<IHttpClientWrapper>();
            DataAccess dataAccess = new DataAccess(loggerMock.Object, httpClientMock.Object);

            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

            httpClientMock.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>()))
                          .ReturnsAsync(httpResponse);

            var result = await dataAccess.GetStocks();

            Assert.IsTrue(result.Count == 0);
            loggerMock.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
            httpClientMock.Verify(client => client.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
        }





        //[TestMethod]
        //public async Task GetStocks_ShouldReturnListOfStocks()
        //{
        //    // Arrange
        //    var loggerMock = new Mock<ILogger>();
        //    var stockService = new DataAccess(loggerMock.Object);
        //    // Act
        //    List<Stocks> x = await stockService.GetStocks();
        //    // Assert
        //    Assert.IsTrue((x != null) && x.Count > 0);
        //}

        //[TestMethod]
        //public async Task GetMarketDataForStock_ShouldReturnNull()
        //{
        //    var loggerMock = new Mock<ILogger>();
        //    var stockService = new DataAccess(loggerMock.Object);
        //    StocksMarketDataApiResponse x = await stockService.GetMarketDataForStock(null);
        //    Assert.IsTrue(x.Values==null);
        //}

        //[TestMethod]
        //public async Task GetMarketDataForStock_ShouldReturnPopulatedListForSymbol()
        //{
        //    var loggerMock = new Mock<ILogger>();
        //    var stockService = new DataAccess(loggerMock.Object);
        //    StocksMarketDataApiResponse x = await stockService.GetMarketDataForStock("AAPL");
        //    Assert.IsTrue(x.Values.Count != 0 && x.Meta.Symbol=="AAPL");
        //}


    }

}

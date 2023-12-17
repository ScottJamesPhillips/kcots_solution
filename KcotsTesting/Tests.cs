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
        //Arrange
        //Act
        //Assert
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

        [TestMethod]
        public async Task GetStockDetail_StocksMarketDataApiValuesShouldNotBeEmpty_WhenHttpResponseSuccessful()
        {
            Mock<ILoggerWrapper> loggerMock = new Mock<ILoggerWrapper>();
            Mock<IHttpClientWrapper> httpClientMock = new Mock<IHttpClientWrapper>();
            DataAccess dataAccess = new DataAccess(loggerMock.Object, httpClientMock.Object);
            List<StocksMarketData> expectedValues = new List<StocksMarketData> { new StocksMarketData { } };
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            var responseJsonString = JsonConvert.SerializeObject(new StocksMarketDataApiResponse { Values = expectedValues });
            httpResponse.Content = new StringContent(responseJsonString);
            httpClientMock.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>()))
                          .ReturnsAsync(httpResponse);


            StocksMarketDataApiResponse result = await dataAccess.GetMarketDataForStock("AAPL");

            Assert.IsTrue(result.Values.Count>0);
            loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
            httpClientMock.Verify(client => client.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
        }

        [TestMethod]
        public async Task GetStockDetail_StocksMarketDataApiValuesShouldBeNull_WhenHttpResponseNotSuccessful()
        {
            // Arrange
            Mock<ILoggerWrapper> loggerMock = new Mock<ILoggerWrapper>();
            Mock<IHttpClientWrapper> httpClientMock = new Mock<IHttpClientWrapper>();
            DataAccess dataAccess = new DataAccess(loggerMock.Object, httpClientMock.Object);
            List<StocksMarketData> expectedValues = new List<StocksMarketData> { new StocksMarketData { } };
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            httpClientMock.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>()))
                          .ReturnsAsync(httpResponse);

            StocksMarketDataApiResponse result = await dataAccess.GetMarketDataForStock("AAPL");

            Assert.IsTrue(result.Values == null);
            loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
            httpClientMock.Verify(client => client.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
        }
    }

}

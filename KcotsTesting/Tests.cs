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
using Kcots.Controls.HomeTab;
using System.Reflection;

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
            Mock<ILoggingWrapper> loggerMock = new Mock<ILoggingWrapper>();
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
            Mock<ILoggingWrapper> loggerMock = new Mock<ILoggingWrapper>();
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
            Mock<ILoggingWrapper> loggerMock = new Mock<ILoggingWrapper>();
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
            Mock<ILoggingWrapper> loggerMock = new Mock<ILoggingWrapper>();
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

        [TestMethod]
        public void GetCurrentStockInfo_Success()
        {
            // Arrange
            var mockDataAccess = new Mock<IDataAccess>();
            var mockLogger = new Mock<ILoggingWrapper>();

            // Set up mock behavior for IDataAccess
            var mockApiResponse = new StocksMarketDataApiResponse
            {
                Values = new List<StocksMarketData> { /* Populate with test data */ }
            };
            mockDataAccess.Setup(da => da.GetMarketDataForStock(It.IsAny<string>()))
                          .ReturnsAsync(mockApiResponse);


            try
            {
                // Create an instance of StockInfoItem with mocked dependencies
                var stockInfoItem = new StockInfoItem
                {
                    logger = mockLogger.Object,
                    dataAccess = mockDataAccess.Object
                };
                stockInfoItem.GetType().GetProperty("logger", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(stockInfoItem, mockLogger.Object);
                stockInfoItem.GetType().GetProperty("dataAccess", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(stockInfoItem, mockDataAccess.Object);

                // Act
                // Invoke the method you want to test
                stockInfoItem.GetCurrentStockInfo();

                // Assert
                // Verify that the stockData property is populated with the expected data
                Assert.IsNotNull(stockInfoItem.stockData);
                Assert.AreEqual(mockApiResponse.Values, stockInfoItem.stockData);
            }
            catch (Exception ex)
            {

                throw;
            };

            
        }

        [TestMethod]
        public void GetCurrentStockInfo_Error()
        {
            // Arrange
            var mockDataAccess = new Mock<IDataAccess>();
            var mockLogger = new Mock<ILogger>();

            // Set up mock behavior for IDataAccess to simulate an exception
            mockDataAccess.Setup(da => da.GetMarketDataForStock(It.IsAny<string>()))
                          .ThrowsAsync(new Exception("Simulated error"));

            // Create an instance of StockInfoItem with mocked dependencies
            var stockInfoItem = new StockInfoItem
            {
                // Consider injecting mockLogger if it's used in other parts of the class
            };
            stockInfoItem.GetType().GetProperty("logger", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(stockInfoItem, mockLogger.Object);
            stockInfoItem.GetType().GetProperty("dataAccess", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(stockInfoItem, mockDataAccess.Object);

            // Act and Assert
            // Verify that an exception is logged
            stockInfoItem.GetCurrentStockInfo();
            Assert.IsNull(stockInfoItem.stockData);
            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
    }

}

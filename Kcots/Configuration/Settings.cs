using Kcots.Data;
using Kcots.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Configuration
{
    public class Settings
    {
        private readonly ILoggingWrapper logger;
        public static IServiceProvider serviceProvider;

        public Settings(ILoggingWrapper logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            serviceProvider = ConfigureServices();
        }

        public IServiceProvider ConfigureServices()
        {
            //Configure services with interfaces
            try
            {
                logger.LogInformation("Getting Stocks List");
                // Example configuration using Microsoft.Extensions.DependencyInjection
                var serviceProvider = new ServiceCollection()
                    .AddScoped<IDataAccess, DataAccess>()  // Registering the concrete implementation of IDataAccess
                    .AddScoped<IHttpClientWrapper, HttpClientWrapper>()  // Registering the concrete implementation of IHttpClientWrapper
                    .AddScoped<ILoggingWrapper, LoggingWrapper>()  // Registering the concrete implementation of ILoggerWrapper
                    .AddHttpClient()  // Registering HttpClient as a transient service
                    .BuildServiceProvider();

                return serviceProvider;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error configuring services");
                throw;
            }
        }
    }
}

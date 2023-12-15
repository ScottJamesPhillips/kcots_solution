using Kcots.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Configuration
{
    public class LoggingWrapper:ILoggerWrapper
    {
        public static ILogger<MainWindow> logger;

        public LoggingWrapper()
        {
        }

        public void InitializeLogger()
        {
            //Create logger and configure for use
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration().WriteTo.File("Logs.txt");
                builder.AddSerilog(loggerConfiguration.CreateLogger());
            });
            logger = loggerFactory.CreateLogger<MainWindow>();
        }

        public void LogInformation(string message) {
            logger.LogInformation(message);
        }
        public void LogError(Exception ex, string message) {
            logger.LogError(ex, message);
        }
    }
}

using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Configuration
{
    public class Logging
    {
        public enum LogType
        {
            warning,
            info,
            error
        }


        public static ILogger<MainWindow> logger;
        public static void InitializeLogger()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration().WriteTo.File("Logs.txt");
                builder.AddSerilog(loggerConfiguration.CreateLogger());
            });
            //Microsoft.Extensions.Logging.ILogger testingComman = loggerFactory.CreateLogger
           logger = loggerFactory.CreateLogger<MainWindow>();  
        }

        public static void WriteLog(string message, LogType lt)
        {
            switch (lt)
            {
                case LogType.error:
                    logger.LogError(message);
                    break;
                case LogType.info:
                    logger.LogInformation(message);
                    break;
                case LogType.warning:
                    logger.LogWarning(message);
                    break;
            }
        }
    }
}

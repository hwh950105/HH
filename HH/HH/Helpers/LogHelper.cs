using log4net;
using log4net.Config;
using System;
using System.IO;

namespace MyApp
{
    public static class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogHelper));

        static LogHelper()
        {
            // Log4net 초기화
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }

        public static void Error(Exception ex)
        {
            log.Error(ex.Message, ex);
        }

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Debug(string message)
        {
            log.Debug(message);
        }

        public static void Warn(string message)
        {
            log.Warn(message);
        }

        public static void Fatal(string message)
        {
            log.Fatal(message);
        }
    }
}

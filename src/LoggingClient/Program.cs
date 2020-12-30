using Postsharp_samples.Attr;
using System;
using NLog;
using NLog.Config;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;
using LoggingClient;

[assembly: Logging(AttributePriority = 0)]

namespace LoggingClient
{
    internal class Program
    {
        private static ILogger Logger;
        static void Main(string[] args)
        {
            ConfigureLog();


            Console.WriteLine("Hello World!");

            //Logger.Info("Application terminated. Press <enter> to exit...");

            Console.ReadLine();
        }

        internal static void ConfigureLog()
        {

            // Configure NLog
            var configuration = new XmlLoggingConfiguration("nlog.config");

            LogManager.Configuration = configuration; // Set it as the default configuration
            LogManager.EnableLogging();

            // Configure PostSharp Logging to use NLog
            LoggingServices.DefaultBackend = new NLogLoggingBackend(new LogFactory(configuration));

            Logger = LogManager.GetCurrentClassLogger(); ;
        }
    }
}

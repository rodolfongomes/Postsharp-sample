using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using PostSharp.Aspects;
using PostSharp.Serialization;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;

namespace LoggingClient
{
  


    [PSerializable]
    public class LogStepAttribute : OnMethodBoundaryAspect
    {
        private ILogger Logger;

        public override void OnEntry(MethodExecutionArgs args)
        {
            ConfigureLog();
            Logger.Info("Entering ...");
        }
        public override void OnException(MethodExecutionArgs args)
        {
            ConfigureLog();
            Logger.Error(args.Exception);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            ConfigureLog();
            Logger.Info("successfully completed");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            ConfigureLog();
            Logger.Info("successfully exited");
        }

        public void ConfigureLog()
        {

            // Configure NLog
            var configuration = new XmlLoggingConfiguration("NLog.config");

            LogManager.Configuration = configuration; // Set it as the default configuration
            LogManager.EnableLogging();

            // Configure PostSharp Logging to use NLog
            LoggingServices.DefaultBackend = new NLogLoggingBackend(new LogFactory(configuration));

            Logger = LogManager.GetCurrentClassLogger(); ;
        }
    }
}

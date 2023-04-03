using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using ILogger = Services.Abstractions.ILogger;
namespace Services
{
    public class Logger : ILogger
    {
        private Serilog.ILogger seriLogger;
        public Logger(Serilog.ILogger logger)
        {
            this.seriLogger = logger;
        }
        public void Log(Exception error)
        {

            seriLogger.Error(
                $@"
Error:
{error}
StackTrace:
{error.StackTrace}
InnerException:
{error.InnerException}");

        }

        public void Log(string message)
        {
            seriLogger.Information(message);
        }
    }
}

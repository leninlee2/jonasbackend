using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;
//using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.App_Start
{
    public class LoggerManager<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            //throw new NotImplementedException();
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //throw new NotImplementedException();
        }

    }
}
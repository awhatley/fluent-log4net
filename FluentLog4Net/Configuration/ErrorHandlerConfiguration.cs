using System;

using log4net.Appender;
using log4net.Core;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the configured error handler for an appender.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorHandlerConfiguration<T>
    {
        public ErrorHandlerConfiguration(T instance)
        {
        }

        public T OnlyOnce()
        {
            throw new NotImplementedException();
        }

        public T With(IErrorHandler handler)
        {
            throw new NotImplementedException();
        }

        public void ApplyTo(AppenderSkeleton appender)
        {
        }
    }
}
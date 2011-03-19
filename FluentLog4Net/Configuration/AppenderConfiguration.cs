using System;

using FluentLog4Net.Appenders;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores configured appender definitions for a logger.
    /// </summary>
    public class AppenderConfiguration
    {
        /// <summary>
        /// Configures the logger to log to the specified appender definition.
        /// </summary>
        /// <param name="appender">An <see cref="IAppenderDefinition"/> instance.</param>
        /// <returns>The current <see cref="LoggingConfiguration"/> instance.</returns>
        public LoggerConfiguration Appender(IAppenderDefinition appender)
        {
            throw new NotImplementedException();
        }
    }
}
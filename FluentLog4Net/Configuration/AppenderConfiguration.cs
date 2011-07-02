using System.Collections.Generic;

using FluentLog4Net.Appenders;

using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores configured appender definitions for a logger.
    /// </summary>
    public class AppenderConfiguration
    {
        private readonly IList<IAppenderDefinition> _appenders = new List<IAppenderDefinition>();
        private readonly LoggerConfiguration _loggerConfiguration;

        internal AppenderConfiguration(LoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        /// <summary>
        /// Configures the logger to log to the specified appender definition.
        /// </summary>
        /// <param name="appender">An <see cref="IAppenderDefinition"/> instance.</param>
        /// <returns>The current <see cref="LoggingConfiguration"/> instance.</returns>
        public LoggerConfiguration Appender(IAppenderDefinition appender)
        {
            _appenders.Add(appender);
            return _loggerConfiguration;
        }

        internal void ApplyTo(Logger logger)
        {
            foreach(var appenderDefinition in _appenders)
                logger.AddAppender(appenderDefinition.Appender);
        }
    }
}
using FluentLog4Net.Appenders;

using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores configured appender definitions for a logger.
    /// </summary>
    public class AppenderConfiguration
    {
        private readonly LoggerConfiguration _loggerConfiguration;
        private IAppenderDefinition _appenderDefinition;

        internal AppenderConfiguration(LoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        /// <summary>
        /// Configures the logger to log to the specified appender definition.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IAppenderDefinition"/>.</typeparam>
        /// <returns>The current <see cref="LoggingConfiguration"/> instance.</returns>
        public LoggerConfiguration Appender<T>() where T : class, IAppenderDefinition, new()
        {
            return Appender(new T());
        }

        /// <summary>
        /// Configures the logger to log to the specified appender definition.
        /// </summary>
        /// <param name="appender">An <see cref="IAppenderDefinition"/> instance.</param>
        /// <returns>The current <see cref="LoggingConfiguration"/> instance.</returns>
        public LoggerConfiguration Appender(IAppenderDefinition appender)
        {
            _appenderDefinition = appender;
            return _loggerConfiguration;
        }

        internal void ApplyTo(Logger logger)
        {
            logger.AddAppender(_appenderDefinition.CreateAppender());
        }
    }
}
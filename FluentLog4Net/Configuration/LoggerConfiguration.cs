using log4net.Core;
using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Configures a logger instance.
    /// </summary>
    public class LoggerConfiguration
    {
        private readonly AppenderConfiguration _appenderConfiguration;
        private Level _level;

        internal LoggerConfiguration()
        {
            _appenderConfiguration = new AppenderConfiguration(this);
        }

        /// <summary>
        /// Specifies the logging level for this logger.
        /// </summary>
        /// <param name="threshold">A <see cref="Level"/> at which to limit logged messages.</param>
        /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
        public LoggerConfiguration At(Level threshold)
        {
            _level = threshold;
            return this;
        }

        /// <summary>
        /// Configures the logger to write to an appender definition.
        /// </summary>
        public AppenderConfiguration To
        {
            get { return _appenderConfiguration; }
        }

        internal void ApplyTo(Logger logger)
        {
            if(_level != null)
                logger.Level = _level;

            _appenderConfiguration.ApplyTo(logger);
        }
    }
}
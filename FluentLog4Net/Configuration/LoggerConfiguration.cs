using System.Collections.Generic;

using FluentLog4Net.Helpers;

using log4net.Core;
using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Configures a logger instance.
    /// </summary>
    public class LoggerConfiguration
    {
        private readonly List<AppenderConfiguration> _appenderConfigurations;
        private Level _level;
        private bool _additivity;

        internal LoggerConfiguration()
        {
            _appenderConfigurations = new List<AppenderConfiguration>();
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

        public LoggerConfiguration InheritAppenders(bool inherit)
        {
            _additivity = inherit;
            return this;
        }

        /// <summary>
        /// Configures the logger to write to an appender definition.
        /// </summary>
        public AppenderConfiguration To
        {
            get { return _appenderConfigurations.AddItem(new AppenderConfiguration(this)); }
        }

        internal void ApplyTo(Logger logger)
        {
            if(_level != null)
                logger.Level = _level;

            if(_additivity == false)
                logger.Additivity = false;

            foreach(var appenderConfiguration in _appenderConfigurations)
                appenderConfiguration.ApplyTo(logger);
        }
    }
}
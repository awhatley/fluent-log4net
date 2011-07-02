using System;

using log4net;
using log4net.Core;
using log4net.Repository;
using log4net.Util;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the log4net settings as they are being fluently configured.
    /// </summary>
    public class Log4NetConfiguration
    {
        private readonly RenderingConfiguration _renderingConfiguration;
        private readonly LoggingConfiguration _loggingConfiguration;

        private bool? _reset;
        private bool? _internalDebugging;
        private Level _threshold;

        internal Log4NetConfiguration()
        {
            _renderingConfiguration = new RenderingConfiguration(this);
            _loggingConfiguration = new LoggingConfiguration(this);
        }

        /// <summary>
        /// Enables or disables internal log4net debugging for this configuration.
        /// </summary>
        /// <param name="internalDebugging">Whether to enable internal debugging.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration InternalDebugging(bool internalDebugging)
        {
            _internalDebugging = internalDebugging;
            return this;
        }

        /// <summary>
        /// Resets the existing configuration before applying any new settings.
        /// </summary>
        /// <param name="overwrite">Whether to overwrite the existing configuration.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration Overwrite(bool overwrite)
        {
            _reset = overwrite;
            return this;
        }

        /// <summary>
        /// Applies a default threshold to all loggers across the repository.
        /// </summary>
        /// <param name="threshold">A <see cref="Level"/> at which to limit logged messages.</param>
        /// <returns></returns>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration Threshold(Level threshold)
        {
            _threshold = threshold;
            return this;
        }

        /// <summary>
        /// Registers object renderers for custom message formatting.
        /// </summary>
        public RenderingConfiguration Render
        {
            get { return _renderingConfiguration; }
        }

        /// <summary>
        /// Configures and attaches appenders to logger instances.
        /// </summary>
        public LoggingConfiguration Logging
        {
            get { return _loggingConfiguration; }
        }

        /// <summary>
        /// Ends fluent configuration and exports all settings to log4net.
        /// </summary>
        public void ApplyConfiguration()
        {
            var repository = LogManager.GetRepository();
            
            if(_internalDebugging.HasValue)
                LogLog.InternalDebugging = _internalDebugging.Value;

            if(_reset.HasValue && _reset.Value)
                repository.ResetConfiguration();

            if(_threshold != null)
                repository.Threshold = _threshold;

            _renderingConfiguration.ApplyConfigurationTo(repository);
            _loggingConfiguration.ApplyConfigurationTo(repository);
            repository.Configured = true;

            var skeleton = repository as LoggerRepositorySkeleton;
            if(skeleton != null)
                skeleton.RaiseConfigurationChanged(EventArgs.Empty);
        }
    }
}
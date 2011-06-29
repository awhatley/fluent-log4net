using System;
using System.Collections.Generic;

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
        private readonly List<Action<ILoggerRepository>> _configurationActions;

        internal Log4NetConfiguration()
        {
            _renderingConfiguration = new RenderingConfiguration(AddConfigurationAction);
            _loggingConfiguration = new LoggingConfiguration(AddConfigurationAction);
            _configurationActions = new List<Action<ILoggerRepository>>();
        }

        /// <summary>
        /// Enables or disables internal log4net debugging for this configuration.
        /// </summary>
        /// <param name="internalDebugging">Whether to enable internal debugging.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration InternalDebugging(bool internalDebugging)
        {
            _configurationActions.Add(_ => LogLog.InternalDebugging = internalDebugging);
            return this;
        }

        /// <summary>
        /// Resets the existing configuration before applying any new settings.
        /// </summary>
        /// <param name="overwrite">Whether to overwrite the existing configuration.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration Overwrite(bool overwrite)
        {
            if(overwrite)
                _configurationActions.Insert(0, repo => repo.ResetConfiguration());

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
            if(threshold != null)
                _configurationActions.Add(repo => repo.Threshold = threshold);

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
            
            _configurationActions.ForEach(configure => configure(repository));

            repository.Configured = true;

            var skeleton = repository as LoggerRepositorySkeleton;
            if(skeleton != null)
                skeleton.RaiseConfigurationChanged(EventArgs.Empty);
        }

        private Log4NetConfiguration AddConfigurationAction(Action<ILoggerRepository> configuration)
        {
            _configurationActions.Add(configuration);
            return this;
        }
    }
}
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
        private readonly RepositoryConfiguration _repositoryConfiguration;
        private readonly LoggingConfiguration _loggingConfiguration;
        private readonly RenderingConfiguration _renderingConfiguration;

        internal Log4NetConfiguration()
        {
            _repositoryConfiguration = new RepositoryConfiguration(this);
            _loggingConfiguration = new LoggingConfiguration(this);
            _renderingConfiguration = new RenderingConfiguration(this);
        }

        /// <summary>
        /// Configures global log4net settings.
        /// </summary>
        public RepositoryConfiguration Global
        {
            get { return _repositoryConfiguration; }
        }

        /// <summary>
        /// Configures and attaches appenders to logger instances.
        /// </summary>
        public LoggingConfiguration Logging
        {
            get { return _loggingConfiguration; }
        }

        /// <summary>
        /// Registers object renderers for custom message formatting.
        /// </summary>
        public RenderingConfiguration Render
        {
            get { return _renderingConfiguration; }
        }

        /// <summary>
        /// Ends fluent configuration and exports all settings to log4net.
        /// </summary>
        public void ApplyConfiguration()
        {
            var repository = LogManager.GetRepository();
            repository.ResetConfiguration();
            
            _repositoryConfiguration.ApplyConfigurationTo(repository);
            _renderingConfiguration.ApplyConfigurationTo(repository);
            _loggingConfiguration.ApplyConfigurationTo(repository);

            repository.Configured = true;

            var skeleton = repository as LoggerRepositorySkeleton;
            if(skeleton != null)
                skeleton.RaiseConfigurationChanged(EventArgs.Empty);
        }
    }
}
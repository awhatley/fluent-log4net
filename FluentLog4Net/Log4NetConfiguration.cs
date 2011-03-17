using System;

using log4net;
using log4net.Repository;

namespace FluentLog4Net
{
    /// <summary>
    /// Stores the log4net settings as they are being fluently configured.
    /// </summary>
    public class Log4NetConfiguration
    {
        private readonly RepositoryConfiguration _repositoryConfiguration = new RepositoryConfiguration();
        private readonly RenderingConfiguration _renderingConfiguration = new RenderingConfiguration();

        /// <summary>
        /// Configures the default log4net repository.
        /// </summary>
        /// <param name="repo">A method that configures the repository settings.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration Repository(Action<RepositoryConfiguration> repo)
        {
            repo(_repositoryConfiguration);
            return this;
        }

        /// <summary>
        /// Registers object renderers for custom message formatting.
        /// </summary>
        /// <param name="rendering">A method that configures object renderers.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration Rendering(Action<RenderingConfiguration> rendering)
        {
            rendering(_renderingConfiguration);
            return this;
        }

        /// <summary>
        /// Ends fluent configuration and exports all settings to log4net.
        /// </summary>
        public void ApplyConfiguration()
        {
            var repository = LogManager.GetRepository();
            
            _repositoryConfiguration.ApplyConfigurationTo(repository);
            _renderingConfiguration.ApplyConfigurationTo(repository);
            repository.Configured = true;

            var skeleton = repository as LoggerRepositorySkeleton;
            if(skeleton != null)
                skeleton.RaiseConfigurationChanged(EventArgs.Empty);
        }
    }
}
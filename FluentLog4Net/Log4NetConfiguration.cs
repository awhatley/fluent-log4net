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

        /// <summary>
        /// Configures the default log4net repository.
        /// </summary>
        /// <param name="repo">A method that configures the repository settings.</param>
        public Log4NetConfiguration Repository(Action<RepositoryConfiguration> repo)
        {
            repo(_repositoryConfiguration);
            return this;
        }

        /// <summary>
        /// Ends fluent configuration and exports all settings to log4net.
        /// </summary>
        public void ApplyConfiguration()
        {
            var repository = LogManager.GetRepository();
            
            _repositoryConfiguration.ApplyConfigurationTo(repository);
            repository.Configured = true;

            var skeleton = repository as LoggerRepositorySkeleton;
            if(skeleton != null)
                skeleton.RaiseConfigurationChanged(EventArgs.Empty);
        }
    }
}
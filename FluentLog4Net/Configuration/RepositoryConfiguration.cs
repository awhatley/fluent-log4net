using log4net.Core;
using log4net.Repository;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the settings for the default log4net repository.
    /// </summary>
    public class RepositoryConfiguration
    {
        private readonly Log4NetConfiguration _log4NetConfiguration;

        private Level _threshold;

        internal RepositoryConfiguration(Log4NetConfiguration log4NetConfiguration)
        {
            _log4NetConfiguration = log4NetConfiguration;
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
            return _log4NetConfiguration;
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            if(_threshold != null)
                repository.Threshold = _threshold;
        }
    }
}
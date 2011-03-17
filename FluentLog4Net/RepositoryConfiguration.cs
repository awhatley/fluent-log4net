using log4net.Core;
using log4net.Repository;
using log4net.Util;

namespace FluentLog4Net
{
    /// <summary>
    /// Stores the settings for the default log4net repository.
    /// </summary>
    public class RepositoryConfiguration
    {
        private bool? _reset;
        private bool? _internalDebugging;
        private Level _threshold;

        /// <summary>
        /// Enables or disables internal log4net debugging for this configuration.
        /// </summary>
        /// <param name="internalDebugging">Whether to enable internal debugging.</param>
        /// <returns>The current <see cref="RepositoryConfiguration"/> instance.</returns>
        public RepositoryConfiguration InternalDebugging(bool internalDebugging)
        {
            _internalDebugging = internalDebugging;
            return this;
        }

        /// <summary>
        /// Resets the existing configuration before applying any new settings.
        /// </summary>
        /// <param name="overwrite">Whether to overwrite the existing configuration.</param>
        /// <returns>The current <see cref="RepositoryConfiguration"/> instance.</returns>
        public RepositoryConfiguration Overwrite(bool overwrite)
        {
            _reset = overwrite;
            return this;
        }

        /// <summary>
        /// Applies a default threshold to all loggers across the repository.
        /// </summary>
        /// <param name="threshold">A <see cref="Level"/> at which to limit logged messages.</param>
        /// <returns></returns>
        /// <returns>The current <see cref="RepositoryConfiguration"/> instance.</returns>
        public RepositoryConfiguration Threshold(Level threshold)
        {
            _threshold = threshold;
            return this;
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            if(_internalDebugging.HasValue)
                LogLog.InternalDebugging = _internalDebugging.Value;

            if(_reset.HasValue && _reset.Value)
                repository.ResetConfiguration();

            if(_threshold != null)
                repository.Threshold = _threshold;
        }
    }
}
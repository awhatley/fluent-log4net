using System;
using System.Collections.Generic;

using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the settings for loggers and their attached appenders.
    /// </summary>
    public class LoggingConfiguration
    {
        private readonly Log4NetConfiguration _log4NetConfiguration;
        private readonly LoggerConfiguration _rootLoggerConfiguration = new LoggerConfiguration();
        private readonly IDictionary<string, LoggerConfiguration> _childLoggerConfigurations = new Dictionary<string, LoggerConfiguration>();

        internal LoggingConfiguration(Log4NetConfiguration log4NetConfiguration)
        {
            _log4NetConfiguration = log4NetConfiguration;
        }

        /// <summary>
        /// Configures the root logger instance.
        /// </summary>
        /// <param name="log">A method that configures the root logger.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration Default(Action<LoggerConfiguration> log)
        {
            log(_rootLoggerConfiguration);
            return _log4NetConfiguration;
        }

        /// <summary>
        /// Configures a logger instance for the specified type.
        /// </summary>
        /// <param name="log">A method that configures the logger.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration For<T>(Action<LoggerConfiguration> log)
        {
            return For(typeof(T), log);
        }

        /// <summary>
        /// Configures a logger instance for the specified type.
        /// </summary>
        /// <param name="type">The type for which to configure a logger.</param>
        /// <param name="log">A method that configures the logger.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration For(Type type, Action<LoggerConfiguration> log)
        {
            return For(type.FullName, log);
        }

        /// <summary>
        /// Configures a logger instance with the specified name.
        /// </summary>
        /// <param name="name">The name of the logger to configure.</param>
        /// <param name="log">A method that configures the logger.</param>
        /// <returns>The current <see cref="Log4NetConfiguration"/> instance.</returns>
        public Log4NetConfiguration For(string name, Action<LoggerConfiguration> log)
        {
            if(!_childLoggerConfigurations.ContainsKey(name))
                _childLoggerConfigurations.Add(name, new LoggerConfiguration());

            log(_childLoggerConfigurations[name]);
            return _log4NetConfiguration;
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            var hierarchy = repository as Hierarchy;
            if(hierarchy != null)
                _rootLoggerConfiguration.ApplyTo(hierarchy.Root);

            foreach(var pair in _childLoggerConfigurations)
            {
                var logger = repository.GetLogger(pair.Key) as Logger;
                if(logger != null)
                    pair.Value.ApplyTo(logger);
            }
        }
    }
}
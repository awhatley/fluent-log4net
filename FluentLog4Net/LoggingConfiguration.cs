using System;
using System.Collections.Generic;

using log4net.Appender;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace FluentLog4Net
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
        public Log4NetConfiguration Root(Action<LoggerConfiguration> log)
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

        /// <summary>
        /// Configures a logger instance.
        /// </summary>
        public class LoggerConfiguration
        {
            private static readonly IDictionary<Type, IAppender> AppenderReferences = new Dictionary<Type, IAppender>();
            private readonly IList<IAppender> _appenders = new List<IAppender>();
            private Level _level;

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
            /// <typeparam name="T">The appender definition type.</typeparam>
            /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
            public LoggerConfiguration To<T>()
                where T : AppenderDefinition, new()
            {
                return To(typeof(T));
            }

            /// <summary>
            /// Configures the logger to write to an appender definition.
            /// </summary>
            /// <param name="type">The appender definition type.</param>
            /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
            public LoggerConfiguration To(Type type)
            {
                if(!AppenderReferences.ContainsKey(type))
                {
                    var definition = Activator.CreateInstance(type) as AppenderDefinition;
                    if(definition == null)
                        throw new ArgumentException("Type " + type.FullName + " must derive from AppenderDefinition to be configured as an appender.");

                    AppenderReferences.Add(type, definition.Configure().BuildAppender());
                }
                
                _appenders.Add(AppenderReferences[type]);
                return this;
            }

            /// <summary>
            /// Configures the logger to write to an appender definition.
            /// </summary>
            /// <param name="appender">An <see cref="AppenderDefinition"/> instance.</param>
            /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
            public LoggerConfiguration To(AppenderDefinition appender)
            {
                _appenders.Add(appender.Configure().BuildAppender());
                return this;
            }

            internal void ApplyTo(Logger logger)
            {
                if(_level != null)
                    logger.Level = _level;

                foreach(var appender in _appenders)
                    logger.AddAppender(appender);
            }
        }
    }
}
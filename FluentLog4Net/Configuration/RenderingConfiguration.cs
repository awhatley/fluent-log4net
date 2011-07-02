using System;
using System.Collections.Generic;

using log4net.Repository;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the settings for the currently configured object renderers.
    /// </summary>
    public class RenderingConfiguration
    {
        private readonly Log4NetConfiguration _log4NetConfiguration;
        private readonly List<RendererConfiguration> _rendererConfigurations;

        internal RenderingConfiguration(Log4NetConfiguration log4NetConfiguration)
        {
            _log4NetConfiguration = log4NetConfiguration;
            _rendererConfigurations = new List<RendererConfiguration>();
        }

        /// <summary>
        /// Registers a custom renderer for the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of object that will be rendered.</typeparam>
        /// <returns>A <see cref="RendererConfiguration"/> instance.</returns>
        public RendererConfiguration Type<TObject>()
        {
            return Type(typeof(TObject));
        }

        /// <summary>
        /// Registers a custom renderer for the specified type.
        /// </summary>
        /// <param name="objectType">The type of object that will be rendered.</param>
        /// <returns>A <see cref="RendererConfiguration"/> instance.</returns>
        public RendererConfiguration Type(Type objectType)
        {
            var configuration = new RendererConfiguration(_log4NetConfiguration, objectType);
            _rendererConfigurations.Add(configuration);

            return configuration;
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            foreach(var configuration in _rendererConfigurations)
                configuration.ApplyTo(repository.RendererMap);
        }
    }
}
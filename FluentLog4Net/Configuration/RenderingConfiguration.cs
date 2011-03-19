using System;
using System.Collections.Generic;

using log4net.ObjectRenderer;
using log4net.Repository;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the settings for the currently configured object renderers.
    /// </summary>
    public class RenderingConfiguration
    {
        internal readonly Log4NetConfiguration Log4NetConfiguration;
        internal readonly IDictionary<Type, IObjectRenderer> Map = new Dictionary<Type, IObjectRenderer>();

        internal RenderingConfiguration(Log4NetConfiguration log4NetConfiguration)
        {
            Log4NetConfiguration = log4NetConfiguration;
        }

        /// <summary>
        /// Registers a custom renderer for the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of object that will be rendered.</typeparam>
        /// <returns>A <see cref="RendererConfiguration"/> instance.</returns>
        public RendererConfiguration Type<TObject>()
        {
            return new RendererConfiguration(this, typeof(TObject));
        }

        /// <summary>
        /// Registers a custom renderer for the specified type.
        /// </summary>
        /// <param name="objectType">The type of object that will be rendered.</param>
        /// <returns>A <see cref="RendererConfiguration"/> instance.</returns>
        public RendererConfiguration Type(Type objectType)
        {
            return new RendererConfiguration(this, objectType);
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            foreach(var pair in Map)
                repository.RendererMap.Put(pair.Key, pair.Value);
        }
    }
}
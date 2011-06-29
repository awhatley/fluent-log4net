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
        private readonly Func<Action<ILoggerRepository>, Log4NetConfiguration> _configure;
        private readonly Dictionary<Type, IObjectRenderer> _cachedRenderers;

        internal RenderingConfiguration(Func<Action<ILoggerRepository>, Log4NetConfiguration> configure)
        {
            _configure = configure;
            _cachedRenderers = new Dictionary<Type, IObjectRenderer>();
        }

        /// <summary>
        /// Registers a custom renderer for the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of object that will be rendered.</typeparam>
        /// <returns>A <see cref="RendererConfiguration"/> instance.</returns>
        public RendererConfiguration Type<TObject>()
        {
            return new RendererConfiguration(typeof(TObject), RegisterRenderer);
        }

        /// <summary>
        /// Registers a custom renderer for the specified type.
        /// </summary>
        /// <param name="objectType">The type of object that will be rendered.</param>
        /// <returns>A <see cref="RendererConfiguration"/> instance.</returns>
        public RendererConfiguration Type(Type objectType)
        {
            return new RendererConfiguration(objectType, RegisterRenderer);
        }

        private Log4NetConfiguration RegisterRenderer(Type targetType, Type rendererType, Func<IObjectRenderer> rendererFactory)
        {
            var renderer = _cachedRenderers.ContainsKey(rendererType)
                ? _cachedRenderers[rendererType] 
                : _cachedRenderers[rendererType] = rendererFactory();

            return _configure(repo => repo.RendererMap.Put(targetType, renderer));
        }
    }
}
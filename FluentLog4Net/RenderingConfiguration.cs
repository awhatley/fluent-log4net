using System;
using System.Collections.Generic;

using log4net.ObjectRenderer;
using log4net.Repository;

namespace FluentLog4Net
{
    /// <summary>
    /// Stores the settings for the currently configured object renderers.
    /// </summary>
    public class RenderingConfiguration
    {
        private readonly IDictionary<Type, IObjectRenderer> _map = new Dictionary<Type, IObjectRenderer>();

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

        /// <summary>
        /// Configures a custom object renderer for a given type.
        /// </summary>
        public class RendererConfiguration
        {
            private readonly RenderingConfiguration _renderingConfiguration;
            private readonly Type _objectType;

            internal RendererConfiguration(RenderingConfiguration renderingConfiguration, Type objectType)
            {
                _renderingConfiguration = renderingConfiguration;
                _objectType = objectType;
            }

            /// <summary>
            /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
            /// </summary>
            /// <typeparam name="TRenderer">A type that implements <see cref="IObjectRenderer"/>.</typeparam>
            /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
            public RenderingConfiguration Using<TRenderer>() where TRenderer : IObjectRenderer, new()
            {
                _renderingConfiguration._map.Add(_objectType, new TRenderer());
                return _renderingConfiguration;
            }

            /// <summary>
            /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
            /// </summary>
            /// <param name="rendererType">A type that implements <see cref="IObjectRenderer"/>.</param>
            /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
            public RenderingConfiguration Using(Type rendererType)
            {
                var renderer = Activator.CreateInstance(rendererType) as IObjectRenderer;
                if(renderer == null)
                    throw new ArgumentException("Type " + rendererType.FullName + " must implement IObjectRenderer to be configured as a renderer.");

                _renderingConfiguration._map.Add(_objectType, renderer);
                return _renderingConfiguration;
            }
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            foreach(var pair in _map)
                repository.RendererMap.Put(pair.Key, pair.Value);
        }
    }
}
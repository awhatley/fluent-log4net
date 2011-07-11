using System;
using System.Collections.Generic;
using System.IO;

using FluentLog4Net.Helpers;

using log4net.ObjectRenderer;
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
            return _rendererConfigurations.AddItem(new RendererConfiguration(_log4NetConfiguration, objectType));
        }

        internal void ApplyConfigurationTo(ILoggerRepository repository)
        {
            foreach(var configuration in _rendererConfigurations)
                configuration.ApplyTo(repository.RendererMap);
        }

        /// <summary>
        /// Configures a custom object renderer for a given type.
        /// </summary>
        public class RendererConfiguration
        {
            private readonly Log4NetConfiguration _log4NetConfiguration;
            private readonly Type _targetType;
            private IObjectRenderer _renderer;

            internal RendererConfiguration(Log4NetConfiguration log4NetConfiguration, Type targetType)
            {
                _log4NetConfiguration = log4NetConfiguration;
                _targetType = targetType;
            }

            /// <summary>
            /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
            /// </summary>
            /// <typeparam name="TRenderer">A type that implements <see cref="IObjectRenderer"/>.</typeparam>
            /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
            public Log4NetConfiguration Using<TRenderer>() where TRenderer : class, IObjectRenderer, new()
            {
                _renderer = new TRenderer();
                return _log4NetConfiguration;
            }

            /// <summary>
            /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
            /// </summary>
            /// <param name="rendererType">A type that implements <see cref="IObjectRenderer"/>.</param>
            /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
            public Log4NetConfiguration Using(Type rendererType)
            {
                const string invalidType = "Type {0} must implement IObjectRenderer to be configured as a renderer.";

                if(!typeof(IObjectRenderer).IsAssignableFrom(rendererType))
                    throw new ArgumentException(String.Format(invalidType, rendererType.FullName));

                _renderer = (IObjectRenderer)Activator.CreateInstance(rendererType, true);
                return _log4NetConfiguration;
            }

            /// <summary>
            /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
            /// </summary>
            /// <param name="renderer">An <see cref="IObjectRenderer"/> instance.</param>
            /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
            public Log4NetConfiguration Using(IObjectRenderer renderer)
            {
                if(renderer == null)
                    throw new ArgumentNullException("renderer", "Renderer cannot be null.");

                _renderer = renderer;
                return _log4NetConfiguration;
            }

            /// <summary>
            /// Defines an <see cref="Action{RendererMap,Object,TextWriter}"/> responsible for 
            /// rendering the type.
            /// </summary>
            /// <param name="renderer">A lambda expression used to render the type.</param>
            /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
            public Log4NetConfiguration Using(Action<RendererMap, object, TextWriter> renderer)
            {
                _renderer = new ActionRenderer(renderer);
                return _log4NetConfiguration;
            }

            internal void ApplyTo(RendererMap map)
            {
                map.Put(_targetType, _renderer);
            }
        }

        private class ActionRenderer : IObjectRenderer
        {
            private readonly Action<RendererMap, object, TextWriter> _renderer;

            public ActionRenderer(Action<RendererMap, object, TextWriter> renderer)
            {
                _renderer = renderer;
            }

            public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
            {
                _renderer(rendererMap, obj, writer);
            }
        }
    }
}
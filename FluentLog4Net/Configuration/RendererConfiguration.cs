using System;
using System.IO;

using log4net.ObjectRenderer;

namespace FluentLog4Net.Configuration
{
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
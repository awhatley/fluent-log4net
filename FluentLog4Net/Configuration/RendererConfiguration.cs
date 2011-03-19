using System;

using log4net.ObjectRenderer;

namespace FluentLog4Net.Configuration
{
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
        public Log4NetConfiguration Using<TRenderer>() where TRenderer : IObjectRenderer, new()
        {
            _renderingConfiguration.Map.Add(_objectType, new TRenderer());
            return _renderingConfiguration.Log4NetConfiguration;
        }

        /// <summary>
        /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
        /// </summary>
        /// <param name="rendererType">A type that implements <see cref="IObjectRenderer"/>.</param>
        /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
        public Log4NetConfiguration Using(Type rendererType)
        {
            var renderer = Activator.CreateInstance(rendererType) as IObjectRenderer;
            if(renderer == null)
                throw new ArgumentException("Type " + rendererType.FullName + " must implement IObjectRenderer to be configured as a renderer.");

            _renderingConfiguration.Map.Add(_objectType, renderer);
            return _renderingConfiguration.Log4NetConfiguration;
        }
    }
}
using System;

using log4net.ObjectRenderer;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Configures a custom object renderer for a given type.
    /// </summary>
    public class RendererConfiguration
    {
        private readonly Type _targetType;
        private readonly Func<Type, Type, Func<IObjectRenderer>, Log4NetConfiguration> _registerRenderer;

        internal RendererConfiguration(Type targetType, Func<Type, Type, Func<IObjectRenderer>, Log4NetConfiguration> registerRenderer)
        {
            _targetType = targetType;
            _registerRenderer = registerRenderer;
        }

        /// <summary>
        /// Defines the <see cref="IObjectRenderer"/> responsible for rendering the type.
        /// </summary>
        /// <typeparam name="TRenderer">A type that implements <see cref="IObjectRenderer"/>.</typeparam>
        /// <returns>The current <see cref="RenderingConfiguration"/> instance.</returns>
        public Log4NetConfiguration Using<TRenderer>() where TRenderer : class, IObjectRenderer, new()
        {
            return _registerRenderer(_targetType, typeof(TRenderer), () => new TRenderer());
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

            return _registerRenderer(_targetType, rendererType, () => (IObjectRenderer)Activator.CreateInstance(rendererType, true));
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

            return _registerRenderer(_targetType, renderer.GetType(), () => renderer);
        }
    }
}
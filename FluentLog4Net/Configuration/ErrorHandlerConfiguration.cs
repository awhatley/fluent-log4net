using FluentLog4Net.ErrorHandlers;

using log4net.Appender;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the configured error handler for an appender.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ErrorHandlerConfiguration<T>
    {
        private readonly T _parent;
        private IErrorHandlerDefinition _handler;

        internal ErrorHandlerConfiguration(T parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Configures the appender to use the specified error handler definition.
        /// </summary>
        /// <param name="handler">An <see cref="IErrorHandlerDefinition"/> instance.</param>
        /// <returns>The parent <see cref="T"/> instance in the fluent API.</returns>
        public T With(IErrorHandlerDefinition handler)
        {
            _handler = handler;
            return _parent;
        }

        internal void ApplyTo(AppenderSkeleton appender)
        {
            if(_handler != null)
                appender.ErrorHandler = _handler.CreateErrorHandler();
        }
    }
}
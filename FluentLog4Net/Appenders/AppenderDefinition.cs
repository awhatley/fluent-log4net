using System.Collections.Generic;

using FluentLog4Net.Configuration;
using FluentLog4Net.Helpers;

using log4net.Appender;
using log4net.Core;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// The base class for all fluent appender definitions.
    /// </summary>
    public abstract class AppenderDefinition<T> : IAppenderDefinition where T : AppenderDefinition<T>
    {
        private Level _threshold;
        private readonly LayoutConfiguration<T>  _layout;
        private readonly List<FilterConfiguration<T>> _filters;
        private readonly ErrorHandlerConfiguration<T> _errorHandler;

        /// <summary>
        /// Initializes a new instance of the base <see cref="AppenderDefinition{T}"/> class.
        /// </summary>
        protected AppenderDefinition()
        {
            _layout = new LayoutConfiguration<T>((T)this);
            _filters = new List<FilterConfiguration<T>>();
            _errorHandler = new ErrorHandlerConfiguration<T>((T)this);
        }

        /// <summary>
        /// Specifies an appender-wide logging threshold. Messages below this threshold
        /// will be ignored by this appender.
        /// </summary>
        /// <param name="threshold">A <see cref="Level"/> at which to limit logged messages.</param>
        /// <returns>The current <typeparamref name="T"/> instance.</returns>
        public T At(Level threshold)
        {
            _threshold = threshold;
            return (T)this;
        }

        /// <summary>
        /// Configures the layout for this appender.
        /// </summary>
        public LayoutConfiguration<T> Format
        {
            get { return _layout; }
        }

        /// <summary>
        /// Configures message filters for this appender.
        /// </summary>
        public FilterConfiguration<T> Apply
        {
            get { return _filters.AddItem(new FilterConfiguration<T>((T)this)); }
        }

        /// <summary>
        /// Configures the error handler for this appender.
        /// </summary>
        public ErrorHandlerConfiguration<T> HandleErrors
        {
            get { return _errorHandler; }
        }

        /// <summary>
        /// Builds an appender instance configured per this definition.
        /// </summary>
        /// <returns>An <see cref="AppenderSkeleton"/> instance.</returns>
        protected abstract AppenderSkeleton CreateAppender();

        IAppender IAppenderDefinition.CreateAppender()
        {
            var appender = CreateAppender();
            appender.Threshold = _threshold;

            _layout.ApplyTo(appender);
            
            foreach(var filter in _filters)
                filter.ApplyTo(appender);

            _errorHandler.ApplyTo(appender);

            return appender;
        }
    }
}
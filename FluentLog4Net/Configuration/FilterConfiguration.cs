using FluentLog4Net.Filters;

using log4net.Appender;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores a configured filter definition for an appender.
    /// </summary>
    /// <typeparam name="T">The parent type being configured in the fluent API.</typeparam>
    public class FilterConfiguration<T>
    {
        private readonly T _parent;
        private IFilterDefinition _filter;

        internal FilterConfiguration(T parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Configures the appender to use the specified filter definition.
        /// </summary>
        /// <param name="filter">An <see cref="IFilterDefinition"/> instance.</param>
        /// <returns>The parent <typeparamref name="T"/> instance in the fluent API.</returns>
        public T Filter(IFilterDefinition filter)
        {
            _filter = filter;
            return _parent;
        }

        internal void ApplyTo(AppenderSkeleton appender)
        {
            if(_filter != null)
                appender.AddFilter(_filter.CreateFilter());
        }
    }
}
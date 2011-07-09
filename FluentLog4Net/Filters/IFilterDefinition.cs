using log4net.Filter;

namespace FluentLog4Net.Filters
{
    /// <summary>
    /// The base interface for all fluent filter definitions.
    /// </summary>
    public interface IFilterDefinition
    {
        /// <summary>
        /// Builds a filter instance configured per the definition.
        /// </summary>
        /// <returns>An <see cref="IFilter"/> instance.</returns>
        IFilter CreateFilter();
    }
}
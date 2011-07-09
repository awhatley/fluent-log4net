using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// The base interface for all fluent layout definitions.
    /// </summary>
    public interface ILayoutDefinition
    {
        /// <summary>
        /// Builds a layout instance configured per the definition.
        /// </summary>
        /// <returns>An <see cref="ILayout"/> instance.</returns>
        ILayout CreateLayout();
    }
}
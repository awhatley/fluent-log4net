using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Configures a <see cref="SimpleLayout"/> instance.
    /// </summary>
    public class SimpleLayoutDefinition : LayoutDefinition<SimpleLayoutDefinition>
    {
        /// <summary>
        /// Builds a <see cref="SimpleLayout"/> instance.
        /// </summary>
        /// <returns>a <see cref="SimpleLayout"/> instance.</returns>
        protected override LayoutSkeleton CreateLayout()
        {
            return new SimpleLayout();
        }
    }
}
using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Configures an <see cref="ExceptionLayout"/> instance.
    /// </summary>
    public class ExceptionLayoutDefinition : LayoutDefinition<ExceptionLayoutDefinition>
    {
        /// <summary>
        /// Builds an <see cref="ExceptionLayout"/> instance.
        /// </summary>
        /// <returns>An <see cref="ExceptionLayout"/> instance.</returns>
        protected override LayoutSkeleton CreateLayout()
        {
            return new ExceptionLayout();
        }
    }
}
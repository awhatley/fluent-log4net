using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// The base class for all fluent layout definitions.
    /// </summary>
    public abstract class LayoutDefinition<T> : ILayoutDefinition where T : LayoutDefinition<T>
    {
        private string _header;
        private string _footer;

        /// <summary>
        /// Uses the specified header text, which will be appended before 
        /// any logging events are formatted and appended.
        /// </summary>
        /// <param name="header">The header text.</param>
        /// <returns>The current <typeparamref name="T"/> instance.</returns>
        public T Header(string header)
        {
            _header = header;
            return (T)this;
        }

        /// <summary>
        /// Uses the specified footer text, which will be appended after 
        /// all logging events have been formatted and appended.
        /// </summary>
        /// <param name="footer">The footer text.</param>
        /// <returns>The current <typeparamref name="T"/> instance.</returns>
        public T Footer(string footer)
        {
            _footer = footer;
            return (T)this;
        }

        /// <summary>
        /// Builds a lyout instance configured per this definition.
        /// </summary>
        /// <returns>An <see cref="LayoutSkeleton"/> instance.</returns>
        protected abstract LayoutSkeleton CreateLayout();

        ILayout ILayoutDefinition.CreateLayout()
        {
            var layout = CreateLayout();
            layout.Header = _header;
            layout.Footer = _footer;

            return layout;
        }
    }
}
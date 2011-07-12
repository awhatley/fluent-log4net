using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Configures a <see cref="PatternLayout"/> instance.
    /// </summary>
    public class PatternLayoutDefinition : LayoutDefinition<PatternLayoutDefinition>
    {
        private readonly string _pattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternLayoutDefinition"/> class with
        /// the specified pattern.
        /// </summary>
        /// <param name="pattern">The log4net conversion pattern to use.</param>
        public PatternLayoutDefinition(string pattern)
        {
            _pattern = pattern;
        }

        /// <summary>
        /// Builds a <see cref="PatternLayout"/> with the configured pattern.
        /// </summary>
        /// <returns>A <see cref="PatternLayout"/> instance.</returns>
        protected override LayoutSkeleton CreateLayout()
        {
            return new PatternLayout(_pattern);
        }
    }
}
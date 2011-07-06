using System;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Helper class used to build and configure layout definitions.
    /// </summary>
    public class LayoutDefinitionBuilder
    {
        /// <summary>
        /// Builds a <see cref="FluentPatternLayoutDefinition"/> instance.
        /// </summary>
        /// <param name="pattern">A method to configure the pattern layout.</param>
        /// <returns>A configured <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Pattern(Action<FluentPatternLayoutDefinition> pattern)
        {
            return BuildAndConfigure(pattern);
        }

        /// <summary>
        /// Builds a <see cref="PatternLayoutDefinition"/> instance.
        /// </summary>
        /// <param name="pattern">The pattern string to use.</param>
        /// <returns>A configured <see cref="PatternLayoutDefinition"/> instance.</returns>
        public PatternLayoutDefinition Pattern(string pattern)
        {
            return new PatternLayoutDefinition(pattern);
        }

        private static T BuildAndConfigure<T>(Action<T> configure) where T : class, ILayoutDefinition, new()
        {
            var definition = new T();
            configure(definition);
            return definition;
        }
    }
}
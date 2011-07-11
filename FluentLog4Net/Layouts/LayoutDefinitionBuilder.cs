using System;

using FluentLog4Net.Helpers;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Helper class used to build and configure layout definitions.
    /// </summary>
    public class LayoutDefinitionBuilder
    {
        /// <summary>
        /// Uses a flexible layout configurable with a fluent API.
        /// </summary>
        /// <param name="pattern">A method to configure the pattern layout.</param>
        /// <returns>A configured <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Pattern(Action<FluentPatternLayoutDefinition> pattern)
        {
            return Build.AndConfigure(pattern);
        }

        /// <summary>
        /// Uses a flexible layout configurable with a pattern string.
        /// </summary>
        /// <param name="pattern">The pattern string to use.</param>
        /// <returns>A configured <see cref="PatternLayoutDefinition"/> instance.</returns>
        public PatternLayoutDefinition Pattern(string pattern)
        {
            return new PatternLayoutDefinition(pattern);
        }
    }
}
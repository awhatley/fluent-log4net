using System;

using FluentLog4Net.Configuration;
using FluentLog4Net.Layouts;

namespace FluentLog4Net
{
    /// <summary>
    /// Helper class for creating re-usable layout definition references.
    /// </summary>
    public static class Layout
    {
        /// <summary>
        /// Begins building a new layout definition.
        /// </summary>
        public static LayoutDefinitionBuilder Using
        {
            get { return new LayoutDefinitionBuilder(); }
        }

        /// <summary>
        /// Uses a flexible layout configurable with a fluent API.
        /// </summary>
        /// <typeparam name="T">The type of appender definition being configured.</typeparam>
        /// <param name="configuration">The current <see cref="LayoutConfiguration{T}"/> instance.</param>
        /// <param name="pattern">A method to fluently build a conversion pattern string.</param>
        /// <returns>The current <typeparamref name="T"/> being configured.</returns>
        public static T Pattern<T>(this LayoutConfiguration<T> configuration, Action<FluentPatternLayoutDefinition> pattern)
        {
            return configuration.Layout(Using.Pattern(pattern));
        }

        /// <summary>
        /// Uses a flexible layout configurable with a pattern string.
        /// </summary>
        /// <typeparam name="T">The type of appender definition being configured.</typeparam>
        /// <param name="configuration">The current <see cref="LayoutConfiguration{T}"/> instance.</param>
        /// <param name="pattern">A conversion pattern string.</param>
        /// <returns>The current <typeparamref name="T"/> being configured.</returns>
        public static T Pattern<T>(this LayoutConfiguration<T> configuration, string pattern)
        {
            return configuration.Layout(Using.Pattern(pattern));
        }
    }
}
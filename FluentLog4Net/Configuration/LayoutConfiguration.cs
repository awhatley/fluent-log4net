using System;

using FluentLog4Net.Layouts;

using log4net.Appender;

namespace FluentLog4Net.Configuration
{
    /// <summary>
    /// Stores the configured layout definition for an appender.
    /// </summary>
    /// <typeparam name="T">The parent type being configured in the fluent API.</typeparam>
    public class LayoutConfiguration<T>
    {
        private readonly T _parent;
        private ILayoutDefinition _layout;

        internal LayoutConfiguration(T parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Configures the appender to use the specified layout definition.
        /// </summary>
        /// <param name="layout">An <see cref="ILayoutDefinition"/> instance.</param>
        /// <returns>The parent <typeparamref name="T"/> instance in the fluent API.</returns>
        public T Layout(ILayoutDefinition layout)
        {
            _layout = layout;
            return _parent;
        }

        /// <summary>
        /// Uses a flexible layout configurable with a fluent API.
        /// </summary>
        /// <typeparam name="T">The type of appender definition being configured.</typeparam>
        /// <param name="pattern">A method to fluently build a conversion pattern string.</param>
        /// <returns>The current <typeparamref name="T"/> being configured.</returns>
        public T Pattern(Action<FluentPatternLayoutDefinition> pattern)
        {
            return Layout(FluentLog4Net.Layout.Using.Pattern(pattern));
        }

        /// <summary>
        /// Uses a flexible layout configurable with a pattern string.
        /// </summary>
        /// <typeparam name="T">The type of appender definition being configured.</typeparam>
        /// <param name="pattern">A conversion pattern string.</param>
        /// <returns>The current <typeparamref name="T"/> being configured.</returns>
        public T Pattern(string pattern)
        {
            return Layout(FluentLog4Net.Layout.Using.Pattern(pattern));
        }

        internal void ApplyTo(AppenderSkeleton appender)
        {
            if(_layout != null)
                appender.Layout = _layout.CreateLayout();
        }
    }
}
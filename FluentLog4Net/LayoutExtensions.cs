using System;

using FluentLog4Net.Configuration;
using FluentLog4Net.Layouts;

namespace FluentLog4Net
{
    public static class LayoutExtensions
    {
        public static T Pattern<T>(this LayoutConfiguration<T> configuration, Action<FluentPatternLayoutDefinition> pattern)
        {
            return configuration.Layout(Layout.Using.Pattern(pattern));
        }

        public static T Pattern<T>(this LayoutConfiguration<T> configuration, string pattern)
        {
            return configuration.Layout(Layout.Using.Pattern(pattern));
        }
    }
}
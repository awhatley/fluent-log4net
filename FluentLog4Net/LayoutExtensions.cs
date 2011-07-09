using System;

using FluentLog4Net.Configuration;
using FluentLog4Net.ErrorHandlers;
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

    public static class ErrorHandlerExtensions
    {
        public static T OnlyOnce<T>(this ErrorHandlerConfiguration<T> configuration, Action<OnlyOnceErrorHandlerDefinition> handler)
        {
            return configuration.With(Handle.Errors.OnlyOnce(handler));
        }
    }

    public static class Handle
    {
        public static ErrorHandlerDefinitionBuilder Errors
        {
            get { return new ErrorHandlerDefinitionBuilder(); }
        }
    }
}
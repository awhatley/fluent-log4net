using System;

using FluentLog4Net.Configuration;
using FluentLog4Net.ErrorHandlers;

namespace FluentLog4Net
{
    /// <summary>
    /// Helper class for creating re-usable error handler definition references.
    /// </summary>
    public static class Handle
    {
        /// <summary>
        /// Begins building a new error handler definition.
        /// </summary>
        public static ErrorHandlerDefinitionBuilder Errors
        {
            get { return new ErrorHandlerDefinitionBuilder(); }
        }

        /// <summary>
        /// Implements log4net's default error handling policy, which consists of emitting a message 
        /// for the first error in an appender and ignoring all subsequent errors.
        /// </summary>
        /// <typeparam name="T">The type of appender definition being configured.</typeparam>
        /// <param name="configuration">The current <see cref="ErrorHandlerConfiguration{T}"/> instance.</param>
        /// <param name="handler">A method to configure the error handler.</param>
        /// <returns>The current <typeparamref name="T"/> being configured.</returns>
        public static T OnlyOnce<T>(this ErrorHandlerConfiguration<T> configuration, Action<OnlyOnceErrorHandlerDefinition> handler)
        {
            return configuration.With(Errors.OnlyOnce(handler));
        }
    }
}
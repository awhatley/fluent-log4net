using System;

using FluentLog4Net.Helpers;

namespace FluentLog4Net.ErrorHandlers
{
    /// <summary>
    /// Helper class used to build and configure error handler definitions.
    /// </summary>
    public class ErrorHandlerDefinitionBuilder
    {
        /// <summary>
        /// Implements log4net's default error handling policy, which consists of emitting a message 
        /// for the first error in an appender and ignoring all subsequent errors.
        /// </summary>
        /// <returns>A configured <see cref="OnlyOnceErrorHandlerDefinition"/> instance.</returns>
        public OnlyOnceErrorHandlerDefinition OnlyOnce(Action<OnlyOnceErrorHandlerDefinition> handler)
        {
            return Build.AndConfigure(handler);
        }
    }
}
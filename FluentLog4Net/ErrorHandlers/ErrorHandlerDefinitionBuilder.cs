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
        /// Builds an <see cref="OnlyOnceErrorHandlerDefinition"/> instance.
        /// </summary>
        /// <returns>A configured <see cref="OnlyOnceErrorHandlerDefinition"/> instance.</returns>
        public OnlyOnceErrorHandlerDefinition OnlyOnce(Action<OnlyOnceErrorHandlerDefinition> handler)
        {
            return Build.AndConfigure(handler);
        }
    }
}
using log4net.Core;

namespace FluentLog4Net.ErrorHandlers
{
    /// <summary>
    /// The base interface for all fluent error handler definitions.
    /// </summary>
    public interface IErrorHandlerDefinition
    {
        /// <summary>
        /// Builds an error handler instance configured per the definition.
        /// </summary>
        /// <returns>An <see cref="IErrorHandler"/> instance.</returns>
        IErrorHandler CreateErrorHandler();
    }
}
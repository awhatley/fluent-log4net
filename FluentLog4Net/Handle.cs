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
    }
}
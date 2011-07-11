using log4net.Core;
using log4net.Util;

namespace FluentLog4Net.ErrorHandlers
{
    /// <summary>
    /// Configures an <see cref="OnlyOnceErrorHandler"/> instance.
    /// </summary>
    public class OnlyOnceErrorHandlerDefinition : IErrorHandlerDefinition
    {
        private string _prefix;

        /// <summary>
        /// Specifies a prefix to use for each message.
        /// </summary>
        /// <param name="prefix">The prefix to use for each message.</param>
        /// <returns>The current <see cref="OnlyOnceErrorHandlerDefinition"/> instance.</returns>
        public OnlyOnceErrorHandlerDefinition PrefixedBy(string prefix)
        {
            _prefix = prefix;
            return this;
        }

        IErrorHandler IErrorHandlerDefinition.CreateErrorHandler()
        {
            return new OnlyOnceErrorHandler(_prefix);
        }
    }
}
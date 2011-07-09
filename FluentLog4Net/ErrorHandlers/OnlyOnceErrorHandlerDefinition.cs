using log4net.Core;
using log4net.Util;

namespace FluentLog4Net.ErrorHandlers
{
    public class OnlyOnceErrorHandlerDefinition : IErrorHandlerDefinition
    {
        private string _prefix;

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
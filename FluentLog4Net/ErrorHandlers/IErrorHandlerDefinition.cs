using log4net.Core;

namespace FluentLog4Net.ErrorHandlers
{
    public interface IErrorHandlerDefinition
    {
        IErrorHandler CreateErrorHandler();
    }
}
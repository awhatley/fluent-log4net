using log4net.Appender;

namespace FluentLog4Net
{
    public interface IAppenderConfiguration
    {
        IAppender BuildAppender();
    }
}
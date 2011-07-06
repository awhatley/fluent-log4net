using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// The base interface for all fluent appender definitions.
    /// </summary>
    public interface IAppenderDefinition
    {
        /// <summary>
        /// Builds an appender instance configured per the definition.
        /// </summary>
        IAppender CreateAppender();
    }
}
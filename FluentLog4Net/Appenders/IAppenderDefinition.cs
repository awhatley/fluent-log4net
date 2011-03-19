using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// The base interface for all fluent appender definitions.
    /// </summary>
    public interface IAppenderDefinition
    {
        /// <summary>
        /// Retrieves the appender instance configured by this definition.
        /// </summary>
        /// <returns>An <see cref="IAppender"/> instance. The instance must
        /// be the same on successive calls.</returns>
        IAppender GetAppender();
    }
}
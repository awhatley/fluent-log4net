using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Configures a <see cref="ConsoleAppender"/> instance.
    /// </summary>
    public class ConsoleAppenderDefinition : IAppenderDefinition
    {
        /// <summary>
        /// Retrieves the appender instance configured by this definition.
        /// </summary>
        /// <returns>An <see cref="IAppender"/> instance. The instance must
        /// be the same on successive calls.</returns>
        public IAppender GetAppender()
        {
            return new ConsoleAppender();
        }
    }
}
using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Configures a <see cref="ConsoleAppender"/> instance.
    /// </summary>
    public class ConsoleAppenderDefinition : IAppenderDefinition
    {
        private readonly ConsoleAppender _appender = new ConsoleAppender();

        /// <summary>
        /// Retrieves the appender instance being configured by this definition.
        /// </summary>
        /// <value>An <see cref="IAppender" /> instance. The instance must
        /// be the same on successive calls.</value>
        public IAppender Appender
        {
            get { return _appender; }
        }
    }
}
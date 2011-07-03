using System;

using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Configures a <see cref="ConsoleAppender"/> instance.
    /// </summary>
    public class ConsoleAppenderDefinition : IAppenderDefinition
    {
        private readonly ConsoleAppender _appender;
        private readonly ConsoleAppenderTarget _targeting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleAppenderDefinition"/> class.
        /// </summary>
        public ConsoleAppenderDefinition()
        {
            _appender = new ConsoleAppender();
            _targeting = new ConsoleAppenderTarget(this);
        }

        IAppender IAppenderDefinition.Appender
        {
            get { return _appender; }
        }

        /// <summary>
        /// Configures the output target of the <see cref="ConsoleAppender"/>.
        /// </summary>
        public ConsoleAppenderTarget Targeting
        {
            get { return _targeting; }
        }

        /// <summary>
        /// Configures the output target of a <see cref="ConsoleAppender"/>.
        /// </summary>
        public class ConsoleAppenderTarget
        {
            private readonly ConsoleAppenderDefinition _consoleAppenderDefinition;

            internal ConsoleAppenderTarget(ConsoleAppenderDefinition consoleAppenderDefinition)
            {
                _consoleAppenderDefinition = consoleAppenderDefinition;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Out"/>.
            /// </summary>
            /// <returns>The current <see cref="ConsoleAppenderDefinition"/> instance.</returns>
            public ConsoleAppenderDefinition ConsoleOut()
            {
                _consoleAppenderDefinition._appender.Target = ConsoleAppender.ConsoleOut;
                return _consoleAppenderDefinition;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Error"/>.
            /// </summary>
            /// <returns>The current <see cref="ConsoleAppenderDefinition"/> instance.</returns>
            public ConsoleAppenderDefinition ConsoleError()
            {
                _consoleAppenderDefinition._appender.Target = ConsoleAppender.ConsoleError;
                return _consoleAppenderDefinition;
            }
        }
    }
}
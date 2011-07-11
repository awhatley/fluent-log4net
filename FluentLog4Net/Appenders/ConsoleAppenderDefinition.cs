using System;

using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Configures a <see cref="ConsoleAppender"/> instance.
    /// </summary>
    public class ConsoleAppenderDefinition : AppenderDefinition<ConsoleAppenderDefinition>
    {
        private readonly Target _targeting;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleAppenderDefinition"/> class.
        /// </summary>
        public ConsoleAppenderDefinition()
        {
            _targeting = new Target(this);
        }

        /// <summary>
        /// Configures the output target of the <see cref="ConsoleAppender"/>.
        /// </summary>
        public Target Targeting
        {
            get { return _targeting; }
        }

        /// <summary>
        /// Builds a <see cref="ConsoleAppender"/> with the current configuration.
        /// </summary>
        /// <returns>A <see cref="ConsoleAppender"/> instance.</returns>
        protected override AppenderSkeleton CreateAppender()
        {
            var appender = new ConsoleAppender();
            _targeting.ApplyTo(appender);

            return appender;
        }

        /// <summary>
        /// Configures the output target of a <see cref="ConsoleAppender"/>.
        /// </summary>
        public class Target
        {
            private readonly ConsoleAppenderDefinition _consoleAppenderDefinition;
            private string _target;

            internal Target(ConsoleAppenderDefinition consoleAppenderDefinition)
            {
                _consoleAppenderDefinition = consoleAppenderDefinition;
                _target = ConsoleAppender.ConsoleOut;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Out"/>.
            /// </summary>
            /// <returns>The current <see cref="ConsoleAppenderDefinition"/> instance.</returns>
            public ConsoleAppenderDefinition ConsoleOut()
            {
                _target = ConsoleAppender.ConsoleOut;
                return _consoleAppenderDefinition;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Error"/>.
            /// </summary>
            /// <returns>The current <see cref="ConsoleAppenderDefinition"/> instance.</returns>
            public ConsoleAppenderDefinition ConsoleError()
            {
                _target = ConsoleAppender.ConsoleError;
                return _consoleAppenderDefinition;
            }

            internal void ApplyTo(ConsoleAppender appender)
            {
                appender.Target = _target;
            }
        }
    }
}
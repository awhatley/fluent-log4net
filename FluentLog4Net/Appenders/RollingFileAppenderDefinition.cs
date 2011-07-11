using System;

using FluentLog4Net.Helpers;

using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Stores the fluent configuration settings for a <see cref="RollingFileAppender"/>.
    /// </summary>
    public class RollingFileAppenderDefinition : AppenderDefinition<RollingFileAppenderDefinition>
    {
        /// <summary>
        /// Indicates that log files should be rolled on a daily basis.
        /// </summary>
        /// <returns></returns>
        public RollingFileAppenderDefinition Daily()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates that logging should always write current messages to the configured 
        /// file name, rather than to the file name based on the current date or size rollover.
        /// </summary>
        /// <returns>The current <see cref="RollingFileAppenderDefinition"/> instance.</returns>
        public RollingFileAppenderDefinition Static()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates that logging should write current messages to the file name determined 
        /// by the current date or size rollover settings, improving performance slightly.
        /// </summary>
        /// <returns>The current <see cref="RollingFileAppenderDefinition"/> instance.</returns>
        public RollingFileAppenderDefinition NonStatic()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates the maximum size the log file is allowed to reach before rolling over.
        /// </summary>
        /// <param name="units">The base unit for the maximum file size.</param>
        /// <returns>A <see cref="ByteMagnitudeSpecifier{T}"/> instance allowing the file size
        /// magnitude to be specified.</returns>
        public ByteMagnitudeSpecifier<RollingFileAppenderDefinition> MaximumSize(int units)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds a <see cref="RollingFileAppender"/> with the current configuration.
        /// </summary>
        /// <returns>A <see cref="RollingFileAppender"/> instance.</returns>
        protected override AppenderSkeleton CreateAppender()
        {
            throw new NotImplementedException();
        }
    }
}
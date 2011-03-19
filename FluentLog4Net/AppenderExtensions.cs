using System;

using FluentLog4Net.Appenders;
using FluentLog4Net.Configuration;

namespace FluentLog4Net
{
    /// <summary>
    /// Helper class for creating re-usable appender definition references.
    /// </summary>
    public static class Append
    {
        /// <summary>
        /// Begins building a new appender definition.
        /// </summary>
        public static AppenderDefinitionBuilder To
        {
            get { return new AppenderDefinitionBuilder(); }
        }

        /// <summary>
        /// Configures logging to the console.
        /// </summary>
        /// <param name="configure">The <see cref="AppenderConfiguration"/> instance.</param>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
        public static LoggerConfiguration Console(this AppenderConfiguration configure, Action<ConsoleAppenderDefinition> console)
        {
            return configure.Appender(To.Console(console));
        }

        /// <summary>
        /// Builds a <see cref="ConsoleAppenderDefinition"/> instance.
        /// </summary>
        /// <param name="build">The <see cref="AppenderDefinitionBuilder"/> instance.</param>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>A configured <see cref="ConsoleAppenderDefinition"/> instance.</returns>
        public static ConsoleAppenderDefinition Console(this AppenderDefinitionBuilder build, Action<ConsoleAppenderDefinition> console)
        {
            return build.Definition(console);
        }
    }
}
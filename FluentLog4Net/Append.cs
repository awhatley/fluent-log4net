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
        /// Configures logging to the console in color.
        /// </summary>
        /// <param name="configure">The <see cref="AppenderConfiguration"/> instance.</param>
        /// <param name="console">A method to configure the colored console logging.</param>
        /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
        public static LoggerConfiguration ColoredConsole(this AppenderConfiguration configure, Action<ColoredConsoleAppenderDefinition> console)
        {
            return configure.Appender(To.ColoredConsole(console));
        }

        /// <summary>
        /// Configures logging to a file.
        /// </summary>
        /// <param name="configure">The <see cref="AppenderConfiguration"/> instance.</param>
        /// <param name="file">A method to configure the file logging.</param>
        /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
        public static LoggerConfiguration File(this AppenderConfiguration configure, Action<FileAppenderDefinition> file)
        {
            return configure.Appender(To.File(file));
        }
    }
}
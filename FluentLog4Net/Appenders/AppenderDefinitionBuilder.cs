using System;

using FluentLog4Net.Helpers;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Helper class used to build and configure appender definitions.
    /// </summary>
    public class AppenderDefinitionBuilder
    {
        /// <summary>
        /// Builds a <see cref="ConsoleAppenderDefinition"/> instance.
        /// </summary>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>A configured <see cref="ConsoleAppenderDefinition"/> instance.</returns>
        public ConsoleAppenderDefinition Console(Action<ConsoleAppenderDefinition> console)
        {
            return Build.AndConfigure(console);
        }

        /// <summary>
        /// Builds a <see cref="ColoredConsoleAppenderDefinition"/> instance.
        /// </summary>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>A configured <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
        public ColoredConsoleAppenderDefinition ColoredConsole(Action<ColoredConsoleAppenderDefinition> console)
        {
            return Build.AndConfigure(console);
        }

        /// <summary>
        /// Builds a <see cref="FileAppenderDefinition"/> instance.
        /// </summary>
        /// <param name="file">A method to configure the file logging.</param>
        /// <returns>A configured <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition File(Action<FileAppenderDefinition> file)
        {
            return Build.AndConfigure(file);
        }
    }
}
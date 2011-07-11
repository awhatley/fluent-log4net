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
        /// Configures logging to the console.
        /// </summary>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>A configured <see cref="ConsoleAppenderDefinition"/> instance.</returns>
        public ConsoleAppenderDefinition Console(Action<ConsoleAppenderDefinition> console)
        {
            return Build.AndConfigure(console);
        }

        /// <summary>
        /// Configures logging to the console in color.
        /// </summary>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>A configured <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
        public ColoredConsoleAppenderDefinition ColoredConsole(Action<ColoredConsoleAppenderDefinition> console)
        {
            return Build.AndConfigure(console);
        }

        /// <summary>
        /// Configures logging to a file.
        /// </summary>
        /// <param name="file">A method to configure the file logging.</param>
        /// <returns>A configured <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition File(Action<FileAppenderDefinition> file)
        {
            return Build.AndConfigure(file);
        }
    }
}
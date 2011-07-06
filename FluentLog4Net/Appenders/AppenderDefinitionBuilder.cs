using System;

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
            return BuildAndConfigure(console);
        }

        /// <summary>
        /// Builds a <see cref="ColoredConsoleAppenderDefinition"/> instance.
        /// </summary>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>A configured <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
        public ColoredConsoleAppenderDefinition ColoredConsole(Action<ColoredConsoleAppenderDefinition> console)
        {
            return BuildAndConfigure(console);
        }

        public FileAppenderDefinition File(Action<FileAppenderDefinition> file)
        {
            return BuildAndConfigure(file);
        }

        private static T BuildAndConfigure<T>(Action<T> configure) where T : AppenderDefinition<T>, new()
        {
            var definition = new T();
            configure(definition);
            return definition;
        }
    }
}
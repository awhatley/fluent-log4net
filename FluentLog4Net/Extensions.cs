﻿using System;

using FluentLog4Net.Appenders;
using FluentLog4Net.Configuration;

namespace FluentLog4Net
{
    /// <summary>
    /// Extension methods for configuring appender definitions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Configures logging to the console.
        /// </summary>
        /// <param name="configure">The <see cref="AppenderConfiguration"/> instance.</param>
        /// <param name="console">A method to configure the console logging.</param>
        /// <returns>The current <see cref="LoggerConfiguration"/> instance.</returns>
        public static LoggerConfiguration Console(this AppenderConfiguration configure, Action<ConsoleAppenderDefinition> console)
        {
            return configure.Appender(Append.To.Console(console));
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
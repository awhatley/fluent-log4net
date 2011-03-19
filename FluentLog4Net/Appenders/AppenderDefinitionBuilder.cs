using System;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Helper class used to build and configure appender definitions.
    /// </summary>
    public class AppenderDefinitionBuilder
    {
        /// <summary>
        /// Builds and configures an appender definition of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of appender definition.</typeparam>
        /// <param name="configure">A method to configure the instance.</param>
        /// <returns>A configured <see cref="IAppenderDefinition"/> instance.</returns>
        public T Definition<T>(Action<T> configure)
            where T : IAppenderDefinition, new()
        {
            return Definition(new T(), configure);
        }

        /// <summary>
        /// Builds and configures an appender definition of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of appender definition.</typeparam>
        /// <param name="definition">The instance to configure.</param>
        /// <param name="configure">A method to configure the instance.</param>
        /// <returns>A configured <see cref="IAppenderDefinition"/> instance.</returns>
        public T Definition<T>(T definition, Action<T> configure)
            where T : IAppenderDefinition
        {
            configure(definition);
            return definition;
        }
    }
}
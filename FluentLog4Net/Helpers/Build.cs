using System;

namespace FluentLog4Net.Helpers
{
    /// <summary>
    /// Helper class to build and configure object instances
    /// </summary>
    public static class Build
    {
        /// <summary>
        /// Creates a new object instance and invokes the specified action on it.
        /// </summary>
        /// <typeparam name="T">The type of object to configure.</typeparam>
        /// <param name="configure">A method to configure the object.</param>
        /// <returns>A new <typeparamref name="T"/> instance.</returns>
        public static T AndConfigure<T>(Action<T> configure) where T : class, new()
        {
            var definition = new T();
            configure(definition);
            return definition;
        }
    }
}
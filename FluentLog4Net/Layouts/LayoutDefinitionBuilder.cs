using System;

using FluentLog4Net.Helpers;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Helper class used to build and configure layout definitions.
    /// </summary>
    public class LayoutDefinitionBuilder
    {
        /// <summary>
        /// Uses a flexible layout configurable with a fluent API.
        /// </summary>
        /// <param name="pattern">A method to configure the pattern layout.</param>
        /// <returns>A configured <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Pattern(Action<FluentPatternLayoutDefinition> pattern)
        {
            return Build.AndConfigure(pattern);
        }

        /// <summary>
        /// Uses a flexible layout configurable with a pattern string.
        /// </summary>
        /// <param name="pattern">The pattern string to use.</param>
        /// <returns>A configured <see cref="PatternLayoutDefinition"/> instance.</returns>
        public PatternLayoutDefinition Pattern(string pattern)
        {
            return new PatternLayoutDefinition(pattern);
        }

        /// <summary>
        /// Renders only the exception message from the logging event.
        /// </summary>
        /// <returns>A configured <see cref="ExceptionLayoutDefinition"/> instance.</returns>
        public ExceptionLayoutDefinition ExceptionMessage()
        {
            return new ExceptionLayoutDefinition();
        }

        /// <summary>
        /// Renders the level of the log statement, followed by " - " and then the message itself.
        /// </summary>
        /// <returns>A configured <see cref="SimpleLayoutDefinition"/> instance.</returns>
        public SimpleLayoutDefinition SimpleMessage()
        {
            return new SimpleLayoutDefinition();
        }

        /// <summary>
        /// Formats the log events as XML elements.
        /// </summary>
        /// <param name="xml">A method to configure the xml layout.</param>
        /// <returns>A configured <see cref="XmlLayoutDefinition"/> instance.</returns>
        public XmlLayoutDefinition Xml(Action<XmlLayoutDefinition> xml)
        {
            return Build.AndConfigure(xml);
        }

        /// <summary>
        /// Formats the log events as XML elements compatible with the log4j schema.
        /// </summary>
        /// <param name="xml">A method to configure the xml layout.</param>
        /// <returns>A configured <see cref="XmlLayoutDefinition"/> instance.</returns>
        public XmlLog4jLayoutDefinition XmlInLog4JSchema(Action<XmlLog4jLayoutDefinition> xml)
        {
            return Build.AndConfigure(xml);
        }
    }
}
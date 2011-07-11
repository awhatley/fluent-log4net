using System;

using log4net.Layout;
using log4net.Util;

namespace FluentLog4Net.Layouts
{
    /// <summary>
    /// Configures a <see cref="PatternLayout"/> instance using a fluent API.
    /// </summary>
    public class FluentPatternLayoutDefinition : ILayoutDefinition
    {
        private string _header;
        private string _pattern = String.Empty;
        private string _footer;
        private PatternLayoutDefinitionModifier _modifier;
        private PatternLayout.ConverterInfo _customConverter;

        /// <summary>
        /// Uses the specified header text, which will be appended before 
        /// any logging events are formatted and appended.
        /// </summary>
        /// <param name="header">The header text.</param>
        /// <returns>The current <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Header(string header)
        {
            _header = header;
            return this;
        }

        /// <summary>
        /// Uses the specified footer text, which will be appended after 
        /// all logging events have been formatted and appended.
        /// </summary>
        /// <param name="footer">The footer text.</param>
        /// <returns>The current <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Footer(string footer)
        {
            _footer = footer;
            return this;
        }

        /// <summary>
        /// Used to output the friendly name of the AppDomain where the logging event was generated.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier AppDomain()
        {
            return Format("appdomain");
        }

        /// <summary>
        /// Used to output the logger of the logging event.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Logger()
        {
            return Format("logger");
        }

        /// <summary>
        /// Used to output the logger of the logging event. The logger's name is separated into components
        /// by the '.' character, and only the specified number of components will be printed, starting
        /// from the right.
        /// </summary>
        /// <param name="precision">The number of right most components of the logger name to print.</param>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Logger(int precision)
        {
            return Format("logger{" + precision + "}");
        }

        /// <summary>
        /// Used to output the fully qualified type name of the caller issuing the logging request. The 
        /// type's name is separated into components by the '.' character, and only the specified number of 
        /// components will be printed, starting from the right.
        /// </summary>
        /// <param name="precision">The number of right most components of the type name to print.</param>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Type(int precision)
        {
            return Format("type{" + precision + "}");
        }

        /// <summary>
        /// Used to output the date of the logging event in the local time zone.
        /// </summary>
        /// <param name="format">A date format string to use for formatting the date.</param>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Date(string format)
        {
            return Format("date{" + format + "}");
        }

        /// <summary>
        /// Used to output the exception passed in with the log message.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Exception()
        {
            return Format("exception");
        }

        /// <summary>
        /// Used to output the file name where the logging request was issued.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier File()
        {
            return Format("file");
        }

        /// <summary>
        /// Used to output the user name for the currently active user (Principal.Identity.Name).
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Identity()
        {
            return Format("identity");
        }

        /// <summary>
        /// Used to output location information of the caller which generated the logging event.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Location()
        {
            return Format("location");
        }

        /// <summary>
        /// Used to output the line number from where the logging request was issued.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier LineNumber()
        {
            return Format("line");
        }

        /// <summary>
        /// Used to output the level of the logging event.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Level()
        {
            return Format("level");
        }

        /// <summary>
        /// Used to output the application supplied message associated with the logging event.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Message()
        {
            return Format("message");
        }

        /// <summary>
        /// Used to output the method name where the logging request was issued.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Method()
        {
            return Format("method");
        }

        /// <summary>
        /// Outputs the platform dependent line separator character or characters.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier NewLine()
        {
            return Format("newline");
        }

        /// <summary>
        /// Used to output the nested diagnostic context associated with the thread that generated 
        /// the logging event.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier NestedDiagnosticContext()
        {
            return Format("ndc");
        }

        /// <summary>
        /// Used to output the an event specific property.
        /// </summary>
        /// <param name="propertyName">The name of the property to output.</param>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Property(string propertyName)
        {
            return Format("property{" + propertyName + "}");
        }

        /// <summary>
        /// Used to output the number of milliseconds elapsed since the start of the application 
        /// until the creation of the logging event.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Timestamp()
        {
            return Format("timestamp");
        }

        /// <summary>
        /// Used to output the name of the thread that generated the logging event. Uses the 
        /// thread number if no name is available.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Thread()
        {
            return Format("thread");
        }

        /// <summary>
        /// Used to output the WindowsIdentity for the currently active user.
        /// </summary>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Username()
        {
            return Format("username");
        }

        /// <summary>
        /// Used to output the date of the logging event in universal time.
        /// </summary>
        /// <param name="format">A date format string to use for formatting the date.</param>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier UtcDate(string format)
        {
            return Format("utcdate{" + format + "}");
        }

        /// <summary>
        /// Used to output a custom pattern string with options. The percent symbol and option braces
        /// should not be included in these strings.
        /// </summary>
        /// <typeparam name="T">A <see cref="PatternConverter"/> to register for this pattern.</typeparam>
        /// <param name="name">The name of the pattern.</param>
        /// <param name="options">Any options to supply for the pattern.</param>
        /// <returns>A <see cref="PatternLayoutDefinitionModifier"/> instance allowing formatting 
        /// to be configured for this token.</returns>
        public PatternLayoutDefinitionModifier Custom<T>(string name, string options) where T : PatternConverter
        {            
            _customConverter = new PatternLayout.ConverterInfo { Name = name, Type = typeof(T) };
            return Format(name + "{" + options + "}");
        }

        /// <summary>
        /// Used to append to the pattern directly. No escaping will be performed.
        /// </summary>
        /// <param name="pattern">The pattern string to append.</param>
        /// <returns>The current <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Pattern(string pattern)
        {
            _pattern += pattern;
            return this;
        }

        /// <summary>
        /// Used to append a space to the pattern.
        /// </summary>
        /// <returns>The current <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Space()
        {
            _pattern += " ";
            return this;
        }

        /// <summary>
        /// Used to append a literal string to the pattern. Percent symbols will be escaped.
        /// </summary>
        /// <param name="text">The literal text to append.</param>
        /// <returns>The current <see cref="FluentPatternLayoutDefinition"/> instance.</returns>
        public FluentPatternLayoutDefinition Literal(string text)
        {
            _pattern += text.Replace("%", "%%");
            return this;
        }

        ILayout ILayoutDefinition.CreateLayout()
        {
            var layout = new PatternLayout {
                Header = _header,
                Footer = _footer,
                ConversionPattern = BuildPattern(),
            };

            if(_customConverter != null)
                layout.AddConverter(_customConverter);

            return layout;
        }

        protected virtual string BuildPattern()
        {
            return _pattern + (_modifier != null ? _modifier.BuildPattern() : String.Empty);
        }

        private PatternLayoutDefinitionModifier Format(string pattern)
        {
            return _modifier = new PatternLayoutDefinitionModifier(pattern);
        }

        /// <summary>
        /// Modifies a pattern by applying justification and width restrictions.
        /// </summary>
        public class PatternLayoutDefinitionModifier : FluentPatternLayoutDefinition
        {
            private readonly string _name;
            private bool _leftJustified;
            private int _minimumWidth;
            private int _maximumWidth;

            internal PatternLayoutDefinitionModifier(string name)
            {
                _name = name;
            }

            /// <summary>
            /// Right-justifies the output text.
            /// </summary>
            /// <returns>The current <see cref="FluentPatternLayoutDefinition.PatternLayoutDefinitionModifier"/> instance.</returns>
            public PatternLayoutDefinitionModifier RightJustified()
            {
                _leftJustified = false;
                return this;
            }

            /// <summary>
            /// Left-justifies the output text.
            /// </summary>
            /// <returns>The current <see cref="FluentPatternLayoutDefinition.PatternLayoutDefinitionModifier"/> instance.</returns>
            public PatternLayoutDefinitionModifier LeftJustified()
            {
                _leftJustified = true;
                return this;
            }

            /// <summary>
            /// Specifies a minimum width for the output text.
            /// Text smaller than this width will be space-padded.
            /// </summary>
            /// <param name="width">The minimum width for the output text.</param>
            /// <returns>The current <see cref="FluentPatternLayoutDefinition.PatternLayoutDefinitionModifier"/> instance.</returns>
            public PatternLayoutDefinitionModifier MinimumWidth(int width)
            {
                _minimumWidth = width;
                return this;
            }

            /// <summary>
            /// Specifies a maximum width for the output text.
            /// Text larger than this width will be truncated starting from the beginning of the string.
            /// </summary>
            /// <param name="width">The maximum width for the output text.</param>
            /// <returns>The current <see cref="FluentPatternLayoutDefinition.PatternLayoutDefinitionModifier"/> instance.</returns>
            public PatternLayoutDefinitionModifier MaximumWidth(int width)
            {
                _maximumWidth = width;
                return this;
            }

            protected override string BuildPattern()
            {
                var pattern = "%" + 
                    (_leftJustified ? "-" : String.Empty) +
                    (_minimumWidth > 0 ? _minimumWidth.ToString() : String.Empty) +
                    (_maximumWidth > 0 ? "." + _maximumWidth : String.Empty) +
                    (_name.TrimStart('%').Replace("%", "%%"));

                return pattern + base.BuildPattern();
            }
        }
    }
}
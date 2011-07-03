using System;
using System.Collections.Generic;

using FluentLog4Net.Helpers;

using log4net.Appender;
using log4net.Core;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Configures a <see cref="ColoredConsoleAppender"/> instance.
    /// </summary>
    public class ColoredConsoleAppenderDefinition : IAppenderDefinition
    {
        private readonly ColoredConsoleAppender _appender;
        private readonly ColoredConsoleAppenderTarget _targeting;
        private readonly Dictionary<int, ColoredConsoleAppenderColors> _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleAppenderDefinition"/> class.
        /// </summary>
        public ColoredConsoleAppenderDefinition()
        {
            _appender = new ColoredConsoleAppender();
            _targeting = new ColoredConsoleAppenderTarget(this);
            _colors = new Dictionary<int, ColoredConsoleAppenderColors>();
        }

        IAppender IAppenderDefinition.Appender
        {
            get { return _appender; }
        }

        /// <summary>
        /// Configures the output target of the <see cref="ColoredConsoleAppender"/>.
        /// </summary>
        public ColoredConsoleAppenderTarget Targeting
        {
            get { return _targeting; }
        }

        /// <summary>
        /// Configures colorization for the specified log <see cref="Level"/>.
        /// </summary>
        /// <param name="level">The <see cref="Level"/> for which to customize colors.</param>
        /// <returns>A <see cref="ColoredConsoleAppenderColors"/> instance.</returns>
        public ColoredConsoleAppenderColors Color(Level level)
        {
            if(level == null)
                level = Level.All;

            if(!_colors.ContainsKey(level.Value))
                _colors.Add(level.Value, new ColoredConsoleAppenderColors(this, level));

            return _colors[level.Value];
        }

        /// <summary>
        /// Configures the output target of a <see cref="ColoredConsoleAppender"/>.
        /// </summary>
        public class ColoredConsoleAppenderTarget
        {
            private readonly ColoredConsoleAppenderDefinition _consoleAppenderDefinition;

            internal ColoredConsoleAppenderTarget(ColoredConsoleAppenderDefinition consoleAppenderDefinition)
            {
                _consoleAppenderDefinition = consoleAppenderDefinition;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Out"/>.
            /// </summary>
            /// <returns>The current <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
            public ColoredConsoleAppenderDefinition ConsoleOut()
            {
                _consoleAppenderDefinition._appender.Target = ConsoleAppender.ConsoleOut;
                return _consoleAppenderDefinition;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Error"/>.
            /// </summary>
            /// <returns>The current <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
            public ColoredConsoleAppenderDefinition ConsoleError()
            {
                _consoleAppenderDefinition._appender.Target = ConsoleAppender.ConsoleError;
                return _consoleAppenderDefinition;
            }
        }

        /// <summary>
        /// Configures the color mappings for a <see cref="ColoredConsoleAppender"/>
        /// </summary>
        public class ColoredConsoleAppenderColors
        {
            private readonly ColoredConsoleAppenderDefinition _consoleAppenderDefinition;
            private readonly Level _level;

            internal ColoredConsoleAppenderColors(ColoredConsoleAppenderDefinition consoleAppenderDefinition, Level level)
            {
                _consoleAppenderDefinition = consoleAppenderDefinition;
                _level = level;
            }

            /// <summary>
            /// Specifies the particular colors to use for a <see cref="Level"/>.
            /// </summary>
            /// <param name="color">A method that builds the colors.</param>
            /// <returns>The current <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
            public ColoredConsoleAppenderDefinition Using(Func<ColorBuilder<ForegroundColor>, Color> color)
            {
                var combinedColor = color(new ColorBuilder<ForegroundColor>());

                _consoleAppenderDefinition._appender.AddMapping(new ColoredConsoleAppender.LevelColors {
                    Level = _level,
                    ForeColor = (ColoredConsoleAppender.Colors)combinedColor.Foreground,
                    BackColor = (ColoredConsoleAppender.Colors)combinedColor.Background,
                });

                return _consoleAppenderDefinition;
            }
        }
    }
}
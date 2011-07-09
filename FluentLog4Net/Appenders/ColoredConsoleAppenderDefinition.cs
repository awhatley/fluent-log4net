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
    public class ColoredConsoleAppenderDefinition : AppenderDefinition<ColoredConsoleAppenderDefinition>
    {
        private readonly Target _targeting;
        private readonly List<ColorMapping> _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleAppenderDefinition"/> class.
        /// </summary>
        public ColoredConsoleAppenderDefinition()
        {
            _targeting = new Target(this);
            _colors = new List<ColorMapping>();
        }

        /// <summary>
        /// Configures the output target of the <see cref="ColoredConsoleAppender"/>.
        /// </summary>
        public Target Targeting
        {
            get { return _targeting; }
        }

        /// <summary>
        /// Configures colorization for the specified log <see cref="Level"/>.
        /// </summary>
        /// <param name="level">The <see cref="Level"/> for which to customize colors.</param>
        /// <returns>A <see cref="ColorMapping"/> instance.</returns>
        public ColorMapping Color(Level level)
        {
            if(level == null)
                level = Level.All;

            return _colors.AddNew(new ColorMapping(this, level));
        }

        protected override AppenderSkeleton CreateAppender()
        {
            var appender = new ColoredConsoleAppender();
            _targeting.ApplyTo(appender);

            foreach(var color in _colors)
                color.ApplyTo(appender);

            return appender;
        }

        /// <summary>
        /// Configures the output target of a <see cref="ColoredConsoleAppender"/>.
        /// </summary>
        public class Target
        {
            private readonly ColoredConsoleAppenderDefinition _consoleAppenderDefinition;
            private string _target;

            internal Target(ColoredConsoleAppenderDefinition consoleAppenderDefinition)
            {
                _consoleAppenderDefinition = consoleAppenderDefinition;
                _target = ColoredConsoleAppender.ConsoleOut;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Out"/>.
            /// </summary>
            /// <returns>The current <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
            public ColoredConsoleAppenderDefinition ConsoleOut()
            {
                _target = ColoredConsoleAppender.ConsoleOut;
                return _consoleAppenderDefinition;
            }

            /// <summary>
            /// Sends messages to <see cref="Console.Error"/>.
            /// </summary>
            /// <returns>The current <see cref="ColoredConsoleAppenderDefinition"/> instance.</returns>
            public ColoredConsoleAppenderDefinition ConsoleError()
            {
                _target = ColoredConsoleAppender.ConsoleError;
                return _consoleAppenderDefinition;
            }

            internal void ApplyTo(ColoredConsoleAppender appender)
            {
                appender.Target = _target;
            }
        }

        /// <summary>
        /// Configures the color mappings for a <see cref="ColoredConsoleAppender"/>
        /// </summary>
        public class ColorMapping
        {
            private readonly ColoredConsoleAppenderDefinition _consoleAppenderDefinition;
            private readonly Level _level;
            private Color _combinedColor;

            internal ColorMapping(ColoredConsoleAppenderDefinition consoleAppenderDefinition, Level level)
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
                _combinedColor = color(new ColorBuilder<ForegroundColor>());
                return _consoleAppenderDefinition;
            }

            internal void ApplyTo(ColoredConsoleAppender appender)
            {
                appender.AddMapping(new ColoredConsoleAppender.LevelColors {
                    Level = _level,
                    ForeColor = (ColoredConsoleAppender.Colors)_combinedColor.Foreground,
                    BackColor = (ColoredConsoleAppender.Colors)_combinedColor.Background,
                });
            }
        }
    }
}
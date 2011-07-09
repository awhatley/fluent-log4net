using System.Reflection;

using log4net;
using log4net.Appender;

using NUnit.Framework;

using log4net.Core;
using log4net.Util;

namespace FluentLog4Net.Appenders
{
    [TestFixture]
    public class ColoredConsoleAppenderTests
    {
        [SetUp]
        public void Setup()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void ConsoleOut()
        {
            IAppenderDefinition console = Append.To.ColoredConsole(c => c
                .Targeting.ConsoleOut());

            var appender = (ColoredConsoleAppender)console.CreateAppender();
            Assert.That(appender.Target, Is.EqualTo(ColoredConsoleAppender.ConsoleOut));
        }

        [Test]
        public void ConsoleError()
        {
            IAppenderDefinition console = Append.To.ColoredConsole(c => c
                .Targeting.ConsoleError());

            var appender = (ColoredConsoleAppender)console.CreateAppender();
            Assert.That(appender.Target, Is.EqualTo(ColoredConsoleAppender.ConsoleError));
        }

        [Test]
        public void ColorMappings()
        {
            var mappingField = typeof(ColoredConsoleAppender)
                .GetField("m_levelMapping", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.NotNull(mappingField);

            IAppenderDefinition console = Append.To.ColoredConsole(c => c
                .Color(Level.Alert).Using(color => color.White.On.Bright.Red)
                .Color(Level.Critical).Using(color => color.Magenta)
                .Color(Level.Debug).Using(color => color.Black.On.Green));

            var appender = (ColoredConsoleAppender)console.CreateAppender();
            var mapping = (LevelMapping)mappingField.GetValue(appender);
            mapping.ActivateOptions();

            var alert = (ColoredConsoleAppender.LevelColors)mapping.Lookup(Level.Alert);
            var critical = (ColoredConsoleAppender.LevelColors)mapping.Lookup(Level.Critical);
            var debug = (ColoredConsoleAppender.LevelColors)mapping.Lookup(Level.Debug);
            
            Assert.That(alert.ForeColor, Is.EqualTo(ColoredConsoleAppender.Colors.White));
            Assert.That(alert.BackColor, Is.EqualTo(ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity));

            Assert.That(critical.ForeColor, Is.EqualTo(ColoredConsoleAppender.Colors.Purple));
            Assert.That(critical.BackColor, Is.EqualTo((ColoredConsoleAppender.Colors)0));

            Assert.That(debug.ForeColor, Is.EqualTo((ColoredConsoleAppender.Colors)0));
            Assert.That(debug.BackColor, Is.EqualTo(ColoredConsoleAppender.Colors.Green));
        }
    }
}
using System.Reflection;

using log4net;
using log4net.Appender;

using NUnit.Framework;

using log4net.Core;
using log4net.Repository.Hierarchy;
using log4net.Util;

using Colors = log4net.Appender.ColoredConsoleAppender.Colors;

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
        public void ConfigurationExtensionMethod()
        {
            IAppender appender = null;

            Log4Net.Configure()
                .Logging.Default(log => log.To.ColoredConsole(c => appender = ((IAppenderDefinition)c).Appender))
                .ApplyConfiguration();

            var repo = (Hierarchy)LogManager.GetRepository();
            Assert.That(repo.Root.Appenders, Has.Count.EqualTo(1));
            Assert.That(repo.Root.Appenders[0], Is.EqualTo(appender));
        }

        [Test]
        public void AppenderReferenceIsConstant()
        {
            IAppenderDefinition console = Append.To.ColoredConsole(c => { });
            Assert.That(console.Appender, Is.SameAs(console.Appender));
        }

        [Test]
        public void AppenderIsColoredConsoleAppender()
        {
            IAppenderDefinition console = Append.To.ColoredConsole(c => { });
            Assert.That(console.Appender, Is.TypeOf<ColoredConsoleAppender>());
        }

        [Test]
        public void ConsoleOut()
        {
            IAppenderDefinition console = Append.To.ColoredConsole(c => c
                .Targeting.ConsoleOut());

            var appender = (ColoredConsoleAppender)console.Appender;
            Assert.That(appender.Target, Is.EqualTo(ColoredConsoleAppender.ConsoleOut));
        }

        [Test]
        public void ConsoleError()
        {
            IAppenderDefinition console = Append.To.ColoredConsole(c => c
                .Targeting.ConsoleError());

            var appender = (ColoredConsoleAppender)console.Appender;
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

            var appender = (ColoredConsoleAppender)console.Appender;
            var mapping = (LevelMapping)mappingField.GetValue(appender);
            mapping.ActivateOptions();

            var alert = (ColoredConsoleAppender.LevelColors)mapping.Lookup(Level.Alert);
            var critical = (ColoredConsoleAppender.LevelColors)mapping.Lookup(Level.Critical);
            var debug = (ColoredConsoleAppender.LevelColors)mapping.Lookup(Level.Debug);
            
            Assert.That(alert.ForeColor, Is.EqualTo(Colors.White));
            Assert.That(alert.BackColor, Is.EqualTo(Colors.Red | Colors.HighIntensity));

            Assert.That(critical.ForeColor, Is.EqualTo(Colors.Purple));
            Assert.That(critical.BackColor, Is.EqualTo((Colors)0));

            Assert.That(debug.ForeColor, Is.EqualTo((Colors)0));
            Assert.That(debug.BackColor, Is.EqualTo(Colors.Green));
        }
    }
}
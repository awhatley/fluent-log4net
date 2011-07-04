using log4net;
using log4net.Appender;

using NUnit.Framework;

using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Appenders
{
    [TestFixture]
    public class ConsoleAppenderTests
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
                .Logging.Default(log => log.To.Console(c => appender = ((IAppenderDefinition)c).Appender))
                .ApplyConfiguration();

            var repo = (Hierarchy)LogManager.GetRepository();
            Assert.That(repo.Root.Appenders, Has.Count.EqualTo(1));
            Assert.That(repo.Root.Appenders[0], Is.EqualTo(appender));
        }

        [Test]
        public void AppenderReferenceIsConstant()
        {
            IAppenderDefinition console = Append.To.Console(c => { });
            Assert.That(console.Appender, Is.SameAs(console.Appender));
        }

        [Test]
        public void AppenderIsConsoleAppender()
        {
            IAppenderDefinition console = Append.To.Console(c => { });
            Assert.That(console.Appender, Is.TypeOf<ConsoleAppender>());
        }

        [Test]
        public void ConsoleOut()
        {
            IAppenderDefinition console = Append.To.Console(c => c
                .Targeting.ConsoleOut());

            var appender = (ConsoleAppender)console.Appender;
            Assert.That(appender.Target, Is.EqualTo(ConsoleAppender.ConsoleOut));
        }

        [Test]
        public void ConsoleError()
        {
            IAppenderDefinition console = Append.To.Console(c => c
                .Targeting.ConsoleError());

            var appender = (ConsoleAppender)console.Appender;
            Assert.That(appender.Target, Is.EqualTo(ConsoleAppender.ConsoleError));
        }
    }
}
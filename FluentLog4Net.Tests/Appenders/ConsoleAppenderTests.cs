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
        public void ConsoleOut()
        {
            IAppenderDefinition console = Append.To.Console(c => c
                .Targeting.ConsoleOut());

            var appender = (ConsoleAppender)console.CreateAppender();
            Assert.That(appender.Target, Is.EqualTo(ConsoleAppender.ConsoleOut));
        }

        [Test]
        public void ConsoleError()
        {
            IAppenderDefinition console = Append.To.Console(c => c
                .Targeting.ConsoleError());

            var appender = (ConsoleAppender)console.CreateAppender();
            Assert.That(appender.Target, Is.EqualTo(ConsoleAppender.ConsoleError));
        }
    }
}
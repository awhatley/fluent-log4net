using log4net.Appender;

using NUnit.Framework;

namespace FluentLog4Net.Appenders
{
    [TestFixture]
    public class ConsoleAppenderTests
    {
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
            Assert.That(appender.Target, Is.EqualTo("Console.Out"));
        }

        [Test]
        public void ConsoleError()
        {
            IAppenderDefinition console = Append.To.Console(c => c
                .Targeting.ConsoleError());

            var appender = (ConsoleAppender)console.Appender;
            Assert.That(appender.Target, Is.EqualTo("Console.Error"));
        }
    }
}
using NUnit.Framework;

namespace FluentLog4Net.Appenders
{
    [TestFixture]
    public class AppenderDefinitionBuilderTests
    {
        [Test]
        public void Console()
        {
            var called = false;
            var builder = new AppenderDefinitionBuilder();
            
            builder.Console(a => called = true);

            Assert.That(called, Is.True);
        }

        [Test]
        public void ColoredConsole()
        {
            var called = false;
            var builder = new AppenderDefinitionBuilder();
            
            builder.ColoredConsole(a => called = true);

            Assert.That(called, Is.True);
        }

        [Test]
        public void File()
        {
            var called = false;
            var builder = new AppenderDefinitionBuilder();
            
            builder.File(a => called = true);

            Assert.That(called, Is.True);
        }
    }
}
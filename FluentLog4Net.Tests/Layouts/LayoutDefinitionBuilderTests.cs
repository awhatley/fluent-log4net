using NUnit.Framework;

using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    [TestFixture]
    public class LayoutDefinitionBuilderTests
    {
        [Test]
        public void BuildFluentPatternDefinitionWithAction()
        {
            var builder = new LayoutDefinitionBuilder();
            
            FluentPatternLayoutDefinition expected = null;
            var actual = builder.Pattern(x => expected = x);

            Assert.That(actual, Is.SameAs(expected));
        }

        [Test]
        public void BuildFluentPatternDefinitionWithString()
        {
            const string pattern = "abc123";
            var builder = new LayoutDefinitionBuilder();
            
            var definition = builder.Pattern(pattern);

            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();
            Assert.That(layout.ConversionPattern, Is.EqualTo(pattern));
        }
    }
}
using FluentLog4Net.Layouts;

using NUnit.Framework;

using Rhino.Mocks;

using log4net;
using log4net.Appender;
using log4net.Layout;

namespace FluentLog4Net.Configuration
{
    [TestFixture]
    public class LayoutConfigurationTests
    {
        [SetUp]
        public void Setup()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void LayoutReturnsParent()
        {
            var parent = new object();
            var configuration = new LayoutConfiguration<object>(parent);
            var layout = MockRepository.GenerateMock<ILayoutDefinition>();
            
            Assert.That(configuration.Layout(layout), Is.EqualTo(parent));
        }

        [Test]
        public void ApplyToRegistersLayout()
        {
            var configuration = new LayoutConfiguration<object>(new object());
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var layoutDefinition = MockRepository.GenerateMock<ILayoutDefinition>();
            var layout = MockRepository.GenerateMock<ILayout>();

            layoutDefinition.Stub(x => x.CreateLayout()).Return(layout);

            configuration.Layout(layoutDefinition);
            configuration.ApplyTo(appender);

            appender.AssertWasCalled(a => a.Layout = layout);
        }
    }
}
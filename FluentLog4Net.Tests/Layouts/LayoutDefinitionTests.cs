using NUnit.Framework;

using Rhino.Mocks;

using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    [TestFixture]
    public class LayoutDefinitionTests
    {
        [Test]
        public void CreateLayoutUsesDerivedLayout()
        {
            var layout = MockRepository.GenerateMock<LayoutSkeleton>();
            var definition = new TestDefinition(layout);

            var actual = ((ILayoutDefinition)definition).CreateLayout();

            Assert.That(actual, Is.SameAs(layout));
        }

        [Test]
        public void CreateLayoutAppliesHeader()
        {
            var layout = MockRepository.GenerateMock<LayoutSkeleton>();
            var definition = new TestDefinition(layout);
            const string header = "HEADER";

            definition = definition.Header(header);
            ((ILayoutDefinition)definition).CreateLayout();

            layout.AssertWasCalled(l => l.Header = header);
        }

        [Test]
        public void CreateLayoutAppliesFooter()
        {
            var layout = MockRepository.GenerateMock<LayoutSkeleton>();
            var definition = new TestDefinition(layout);
            const string footer = "FOOTER";

            definition = definition.Footer(footer);
            ((ILayoutDefinition)definition).CreateLayout();

            layout.AssertWasCalled(l => l.Footer = footer);
        }
    
        private class TestDefinition : LayoutDefinition<TestDefinition>
        {
            private readonly LayoutSkeleton _layout;

            public TestDefinition(LayoutSkeleton layout)
            {
                _layout = layout;
            }

            protected override LayoutSkeleton CreateLayout()
            {
                return _layout;
            }
        }
    }
}
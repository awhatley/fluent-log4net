using FluentLog4Net.ErrorHandlers;
using FluentLog4Net.Filters;
using FluentLog4Net.Layouts;

using NUnit.Framework;

using Rhino.Mocks;

using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;

namespace FluentLog4Net.Appenders
{
    [TestFixture]
    public class AppenderDefinitionTests
    {
        [Test]
        public void CreateAppenderUsesDerivedAppender()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);

            var actual = ((IAppenderDefinition)definition).CreateAppender();

            Assert.That(actual, Is.SameAs(appender));
        }

        [Test]
        public void CreateAppenderAppliesThreshold()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var threshold = Level.Error;

            definition = definition.At(threshold);
            ((IAppenderDefinition)definition).CreateAppender();

            Assert.That(appender.Threshold, Is.EqualTo(threshold));
        }

        [Test]
        public void CreateAppenderAppliesNullThreshold()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);

            definition = definition.At(null);
            ((IAppenderDefinition)definition).CreateAppender();

            Assert.That(appender.Threshold, Is.Null);
        }

        [Test]
        public void CreateAppenderAppliesLastThresholdSet()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var threshold = Level.Warn;

            definition = definition
                .At(null)
                .At(Level.Error)
                .At(threshold);

            ((IAppenderDefinition)definition).CreateAppender();

            Assert.That(appender.Threshold, Is.EqualTo(threshold));
        }

        [Test]
        public void CreateAppenderAppliesLayout()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var layoutDefinition = MockRepository.GenerateMock<ILayoutDefinition>();
            var layout = MockRepository.GenerateMock<ILayout>();
            
            layoutDefinition.Stub(l => l.CreateLayout()).Return(layout);

            definition.Format.Layout(layoutDefinition);
            ((IAppenderDefinition)definition).CreateAppender();

            appender.AssertWasCalled(a => a.Layout = layout);
        }

        [Test]
        public void CreateAppenderAppliesLastLayoutSet()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var layoutDefinition = MockRepository.GenerateMock<ILayoutDefinition>();
            var layout = MockRepository.GenerateMock<ILayout>();
            
            layoutDefinition.Stub(l => l.CreateLayout()).Return(layout);

            definition.Format.Layout(null);
            definition.Format.Layout(MockRepository.GenerateMock<ILayoutDefinition>());
            definition.Format.Layout(layoutDefinition);
            ((IAppenderDefinition)definition).CreateAppender();

            appender.AssertWasCalled(a => a.Layout = layout);
        }

        [Test]
        public void CreateAppenderAppliesFilters()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var filterDefinition = MockRepository.GenerateMock<IFilterDefinition>();
            var filter = MockRepository.GenerateMock<IFilter>();
            
            filterDefinition.Stub(l => l.CreateFilter()).Return(filter);

            definition.Apply.Filter(filterDefinition);
            ((IAppenderDefinition)definition).CreateAppender();

            appender.AssertWasCalled(a => a.AddFilter(filter));
        }

        [Test]
        public void CreateAppenderAppliesMultipleFilters()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var filterDefinition1 = MockRepository.GenerateMock<IFilterDefinition>();
            var filter1 = MockRepository.GenerateMock<IFilter>();
            var filterDefinition2 = MockRepository.GenerateMock<IFilterDefinition>();
            var filter2 = MockRepository.GenerateMock<IFilter>();
            var filterDefinition3 = MockRepository.GenerateMock<IFilterDefinition>();
            var filter3 = MockRepository.GenerateMock<IFilter>();
            
            filterDefinition1.Stub(l => l.CreateFilter()).Return(filter1);
            filterDefinition2.Stub(l => l.CreateFilter()).Return(filter2);
            filterDefinition3.Stub(l => l.CreateFilter()).Return(filter3);

            definition.Apply.Filter(filterDefinition1);
            definition.Apply.Filter(filterDefinition2);
            definition.Apply.Filter(filterDefinition3);
            ((IAppenderDefinition)definition).CreateAppender();

            appender.AssertWasCalled(a => a.AddFilter(filter1));
            appender.AssertWasCalled(a => a.AddFilter(filter2));
            appender.AssertWasCalled(a => a.AddFilter(filter3));
        }

        [Test]
        public void CreateAppenderAppliesErrorHandler()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var errorHandlerDefinition = MockRepository.GenerateMock<IErrorHandlerDefinition>();
            var errorHandler = MockRepository.GenerateMock<IErrorHandler>();
            
            errorHandlerDefinition.Stub(l => l.CreateErrorHandler()).Return(errorHandler);

            definition.HandleErrors.With(errorHandlerDefinition);
            ((IAppenderDefinition)definition).CreateAppender();

            appender.AssertWasCalled(a => a.ErrorHandler = errorHandler);
        }

        [Test]
        public void CreateAppenderAppliesLastErrorHandlerSet()
        {
            var appender = MockRepository.GenerateMock<AppenderSkeleton>();
            var definition = new TestDefinition(appender);
            var errorHandlerDefinition = MockRepository.GenerateMock<IErrorHandlerDefinition>();
            var errorHandler = MockRepository.GenerateMock<IErrorHandler>();
            
            errorHandlerDefinition.Stub(l => l.CreateErrorHandler()).Return(errorHandler);

            definition.HandleErrors.With(null);
            definition.HandleErrors.With(MockRepository.GenerateMock<IErrorHandlerDefinition>());
            definition.HandleErrors.With(errorHandlerDefinition);
            ((IAppenderDefinition)definition).CreateAppender();

            appender.AssertWasCalled(a => a.ErrorHandler = errorHandler);
        }

        private class TestDefinition : AppenderDefinition<TestDefinition>
        {
            private readonly AppenderSkeleton _appender;

            public TestDefinition(AppenderSkeleton appender)
            {
                _appender = appender;
            }

            protected override AppenderSkeleton CreateAppender()
            {
                return _appender;
            }
        }
    }
}
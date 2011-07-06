using System;

using FluentLog4Net.Appenders;

using NUnit.Framework;

using Rhino.Mocks;

using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace FluentLog4Net.Configuration
{
    [TestFixture]
    public class AppenderConfigurationTests
    {
        [Test]
        public void ApplyToAddsConfiguredAppender()
        {
            var parent = MockRepository.GenerateMock<LoggerConfiguration>();
            var definition = MockRepository.GenerateMock<IAppenderDefinition>();
            var appender = MockRepository.GenerateMock<IAppender>();
            var logger = MockRepository.GenerateMock<Logger>(String.Empty);
            var configuration = new AppenderConfiguration(parent);

            definition.Stub(d => d.CreateAppender()).Return(appender);

            configuration.Appender(definition);
            configuration.ApplyTo(logger);

            logger.AssertWasCalled(l => l.AddAppender(appender));
        }

        [Test]
        public void ApplyToAddsConfiguredAppenderGeneric()
        {
            var parent = MockRepository.GenerateMock<LoggerConfiguration>();
            var appender = MockRepository.GenerateMock<IAppender>();
            var logger = MockRepository.GenerateMock<Logger>(String.Empty);
            var configuration = new AppenderConfiguration(parent);

            CustomAppenderDefinition.Appender = appender;

            configuration.Appender<CustomAppenderDefinition>();
            configuration.ApplyTo(logger);

            logger.AssertWasCalled(l => l.AddAppender(appender));
        }

        private class CustomAppenderDefinition : IAppenderDefinition
        {
            internal static IAppender Appender;

            public IAppender CreateAppender()
            {
                return Appender;
            }
        }
    }
}
using System;

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;

using NUnit.Framework;

namespace FluentLog4Net
{
    [TestFixture]
    public class LoggingConfigurationTests
    {
        [SetUp]
        public void ResetConfiguration()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void RootLoggerConfiguration()
        {
            Log4Net.Configure()
                .Logging(l => l
                    .Root(log => log
                        .At(Level.Severe)
                        .To<CustomAppender1>()
                        .To(typeof(CustomAppender2))
                        .To(new CustomAppender3())))
                .ApplyConfiguration();

            var repo = (Hierarchy)LogManager.GetRepository();
            Assert.That(repo.Root.Level, Is.EqualTo(Level.Severe));
            Assert.That(repo.Root.Appenders, Has.Count.EqualTo(3));
            Assert.That(repo.Root.Appenders[0], Is.EqualTo(CustomAppender1.Appender));
            Assert.That(repo.Root.Appenders[1], Is.EqualTo(CustomAppender2.Appender));
            Assert.That(repo.Root.Appenders[2], Is.EqualTo(CustomAppender3.Appender));
        }

        [Test]
        public void RootToThrowsExceptionOnWrongType()
        {
            const string error = "Type {0} must derive from AppenderDefinition to be configured as an appender.";

            Assert.That(() =>
                Log4Net.Configure()
                    .Logging(l => l.Root(log => log.To(typeof(Int32))))
                    .ApplyConfiguration(),
                Throws.ArgumentException.With.Message.EqualTo(String.Format(error, typeof(Int32).FullName)));
        }

        [Test]
        public void ChildToThrowsExceptionOnWrongType()
        {
            const string error = "Type {0} must derive from AppenderDefinition to be configured as an appender.";

            Assert.That(() =>
                Log4Net.Configure()
                    .Logging(l => l.For<Int32>(log => log.To(typeof(Int32))))
                    .ApplyConfiguration(),
                Throws.ArgumentException.With.Message.EqualTo(String.Format(error, typeof(Int32).FullName)));
        }

        [Test]
        public void ChildLoggerGenericConfiguration()
        {
            Log4Net.Configure()
                .Logging(l => l
                    .For<TimeZone>(log => log
                        .At(Level.Severe)
                        .To<CustomAppender1>()
                        .To(typeof(CustomAppender2))
                        .To(new CustomAppender3())))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var timeZoneLogger = (Logger)repo.GetLogger("System.TimeZone");

            Assert.That(timeZoneLogger.Level, Is.EqualTo(Level.Severe));
            Assert.That(timeZoneLogger.Appenders, Has.Count.EqualTo(3));
            Assert.That(timeZoneLogger.Appenders[0], Is.EqualTo(CustomAppender1.Appender));
            Assert.That(timeZoneLogger.Appenders[1], Is.EqualTo(CustomAppender2.Appender));
            Assert.That(timeZoneLogger.Appenders[2], Is.EqualTo(CustomAppender3.Appender));
        }

        [Test]
        public void ChildLoggerTypeConfiguration()
        {
            Log4Net.Configure()
                .Logging(l => l
                    .For(typeof(TimeZone), log => log
                        .At(Level.Severe)
                        .To<CustomAppender1>()
                        .To(typeof(CustomAppender2))
                        .To(new CustomAppender3())))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var timeZoneLogger = (Logger)repo.GetLogger("System.TimeZone");

            Assert.That(timeZoneLogger.Level, Is.EqualTo(Level.Severe));
            Assert.That(timeZoneLogger.Appenders, Has.Count.EqualTo(3));
            Assert.That(timeZoneLogger.Appenders[0], Is.EqualTo(CustomAppender1.Appender));
            Assert.That(timeZoneLogger.Appenders[1], Is.EqualTo(CustomAppender2.Appender));
            Assert.That(timeZoneLogger.Appenders[2], Is.EqualTo(CustomAppender3.Appender));
        }

        [Test]
        public void ChildLoggerNameConfiguration()
        {
            Log4Net.Configure()
                .Logging(l => l
                    .For("Foo", log => log
                        .At(Level.Severe)
                        .To<CustomAppender1>()
                        .To(typeof(CustomAppender2))
                        .To(new CustomAppender3())))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var fooLogger = (Logger)repo.GetLogger("Foo");

            Assert.That(fooLogger.Level, Is.EqualTo(Level.Severe));
            Assert.That(fooLogger.Appenders, Has.Count.EqualTo(3));
            Assert.That(fooLogger.Appenders[0], Is.EqualTo(CustomAppender1.Appender));
            Assert.That(fooLogger.Appenders[1], Is.EqualTo(CustomAppender2.Appender));
            Assert.That(fooLogger.Appenders[2], Is.EqualTo(CustomAppender3.Appender));
        }

        [Test]
        public void ChildLoggersShareAppenderInstances()
        {
            Log4Net.Configure()
                .Logging(l => l
                    .For("Foo", log => log
                        .To<CustomAppender1>()
                        .To(typeof(CustomAppender2))
                        .To(new CustomAppender3()))
                    .For("Bar", log => log
                        .To<CustomAppender1>()
                        .To(typeof(CustomAppender2))
                        .To(new CustomAppender3())))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var fooLogger = (Logger)repo.GetLogger("Foo");
            var barLogger = (Logger)repo.GetLogger("Bar");

            Assert.That(fooLogger.Appenders, Has.Count.EqualTo(3));
            Assert.That(barLogger.Appenders, Has.Count.EqualTo(3));
            Assert.That(fooLogger.Appenders[0], Is.EqualTo(barLogger.Appenders[0]));
            Assert.That(fooLogger.Appenders[1], Is.EqualTo(barLogger.Appenders[1]));
            Assert.That(fooLogger.Appenders[2], Is.EqualTo(barLogger.Appenders[2]));
        }

        private class CustomAppender : IAppender
        {
            public void Close()
            {
            }

            public void DoAppend(LoggingEvent loggingEvent)
            {
            }

            public string Name
            {
                get { return "CustomAppender"; }
                set { }
            }
        }

        private class CustomAppender1 : AppenderDefinition, IAppenderConfiguration
        {
            public static readonly IAppender Appender = new CustomAppender();

            public override IAppenderConfiguration Configure()
            {
                return this;
            }

            public IAppender BuildAppender()
            {
                return Appender;
            }
        }

        private class CustomAppender2 : AppenderDefinition, IAppenderConfiguration
        {
            public static readonly IAppender Appender = new CustomAppender();

            public override IAppenderConfiguration Configure()
            {
                return this;
            }

            public IAppender BuildAppender()
            {
                return Appender;
            }
        }

        private class CustomAppender3 : AppenderDefinition, IAppenderConfiguration
        {
            public static readonly IAppender Appender = new CustomAppender();

            public override IAppenderConfiguration Configure()
            {
                return this;
            }

            public IAppender BuildAppender()
            {
                return Appender;
            }
        }
    }
}
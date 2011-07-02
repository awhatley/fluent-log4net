using log4net;
using log4net.Core;
using log4net.Util;

using NUnit.Framework;

namespace FluentLog4Net
{
    [TestFixture]
    public class Log4NetConfigurationTests
    {
        [SetUp]
        public void Setup()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void DebugSetsInternalDebugging()
        {
            LogLog.InternalDebugging = false;

            Log4Net.Configure()
                .InternalDebugging(true)
                .ApplyConfiguration();

            Assert.That(LogLog.InternalDebugging, Is.True);
        }

        [Test]
        public void DebugFalseDisablesInternalDebugging()
        {
            LogLog.InternalDebugging = true;

            Log4Net.Configure()
                .InternalDebugging(false)
                .ApplyConfiguration();

            Assert.That(LogLog.InternalDebugging, Is.False);
        }

        [Test]
        public void OverwriteResetsConfiguration()
        {
            var reset = false;
            var repo = LogManager.GetRepository();
            repo.ConfigurationReset += (sender, args) => reset = true;

            Log4Net.Configure()
                .Overwrite(true)
                .ApplyConfiguration();

            Assert.That(reset, Is.True);
        }

        [Test]
        public void OverwriteFalseDoesNotResetConfiguration()
        {
            var reset = false;
            var repo = LogManager.GetRepository();
            repo.ConfigurationReset += (sender, args) => reset = true;

            Log4Net.Configure()
                .Overwrite(false)
                .ApplyConfiguration();

            Assert.That(reset, Is.False);
        }

        [Test]
        public void ThresholdAppliesRepositoryThreshold()
        {
            var repo = LogManager.GetRepository();

            Log4Net.Configure()
                .Threshold(Level.Notice)
                .ApplyConfiguration();

            Assert.That(repo.Threshold, Is.EqualTo(Level.Notice));
        }

        [Test]
        public void ApplyConfigurationRaisesConfigurationChanged()
        {
            var changed = false;
            var repo = LogManager.GetRepository();            
            repo.ConfigurationChanged += (sender, args) => changed = true;

            Log4Net.Configure().ApplyConfiguration();
            Assert.That(changed, Is.True);
        }

        [Test]
        public void ApplyConfigurationSetsConfigured()
        {
            var repo = LogManager.GetRepository();
            Assert.That(repo.Configured, Is.False);
            Log4Net.Configure().ApplyConfiguration();
            Assert.That(repo.Configured, Is.True);
        }
    }
}
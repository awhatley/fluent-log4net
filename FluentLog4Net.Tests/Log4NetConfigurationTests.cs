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
        public void ApplyConfigurationResetsConfiguration()
        {
            var reset = false;
            var repo = LogManager.GetRepository();
            repo.ConfigurationReset += (sender, args) => reset = true;

            Log4Net.Configure().ApplyConfiguration();
            Assert.That(reset, Is.True);
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
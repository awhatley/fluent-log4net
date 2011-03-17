using log4net;

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
            Log4Net.Configure().ApplyConfiguration();
            Assert.That(repo.Configured, Is.True);
        }
    }
}
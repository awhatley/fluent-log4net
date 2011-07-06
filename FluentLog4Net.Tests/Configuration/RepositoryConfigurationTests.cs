using NUnit.Framework;

using log4net;
using log4net.Core;

namespace FluentLog4Net.Configuration
{
    [TestFixture]
    public class RepositoryConfigurationTests
    {
        [SetUp]
        public void Setup()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void ThresholdAppliesRepositoryThreshold()
        {
            var repo = LogManager.GetRepository();

            Log4Net.Configure()
                .Global.Threshold(Level.Notice)
                .ApplyConfiguration();

            Assert.That(repo.Threshold, Is.EqualTo(Level.Notice));
        }
    }
}
using System.IO;
using System.Text;

using log4net;
using log4net.Appender;

using NUnit.Framework;

namespace FluentLog4Net.Appenders
{
    [TestFixture]
    public class FileAppenderTests
    {
        [SetUp]
        public void Setup()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void FileName()
        {
            Log4Net.Configure()
                .Logging.Default(l => l.To.File(f => f.Named(@"fileName")))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var appenders = repo.GetAppenders();
            var fileAppender = (FileAppender)appenders[0];
            
            Assert.That(fileAppender.File, Is.EqualTo(@"fileName"));
        }

        [Test]
        public void ExclusiveLock()
        {
            Log4Net.Configure()
                .Logging.Default(l => l.To.File(f => f.LockingExclusively()))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var appenders = repo.GetAppenders();
            var fileAppender = (FileAppender)appenders[0];
            
            Assert.That(fileAppender.LockingModel, Is.TypeOf<FileAppender.ExclusiveLock>());
        }

        [Test]
        public void MinimalLock()
        {
            Log4Net.Configure()
                .Logging.Default(l => l.To.File(f => f.LockingMinimally()))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var appenders = repo.GetAppenders();
            var fileAppender = (FileAppender)appenders[0];
            
            Assert.That(fileAppender.LockingModel, Is.TypeOf<FileAppender.MinimalLock>());
        }

        [Test]
        public void CustomLock()
        {
            Log4Net.Configure()
                .Logging.Default(l => l.To.File(f => f.LockingWith<MyLock>()))
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            var appenders = repo.GetAppenders();
            var fileAppender = (FileAppender)appenders[0];

            Assert.That(fileAppender.LockingModel, Is.TypeOf<MyLock>());
        }

        private class MyLock : FileAppender.LockingModelBase
        {
            public override void OpenFile(string filename, bool append, Encoding encoding)
            {
            }

            public override void CloseFile()
            {
            }

            public override Stream AcquireLock()
            {
                return null;
            }

            public override void ReleaseLock()
            {
            }
        }
    }
}
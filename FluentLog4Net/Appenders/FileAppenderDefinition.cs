using log4net.Appender;

namespace FluentLog4Net.Appenders
{
    /// <summary>
    /// Stores the fluent configuration settings for a <see cref="FileAppender"/>.
    /// </summary>
    public class FileAppenderDefinition : AppenderDefinition<FileAppenderDefinition>
    {
        private string _name;
        private FileAppender.LockingModelBase _lockingModel;
        private bool _appendToFile;

        /// <summary>
        /// Provides a name for the file to write log messages to.
        /// </summary>
        /// <param name="fileName">The full path of the file to write to.</param>
        /// <returns>The current <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition Named(string fileName)
        {
            _name = fileName;
            return this;
        }

        /// <summary>
        /// Indicates that locking should be done on a per-write basis. This is considerably
        /// slower than an exclusive lock, but allows other processes to move or delete the
        /// file during logging.
        /// </summary>
        /// <returns>The current <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition LockingMinimally()
        {
            _lockingModel = new FileAppender.MinimalLock();
            return this;
        }

        /// <summary>
        /// Indicates that a file lock should be held throughout the logging process, 
        /// maintaining an exclusive lock on the file.
        /// </summary>
        /// <returns>The current <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition LockingExclusively()
        {
            _lockingModel = new FileAppender.ExclusiveLock();
            return this;
        }

        /// <summary>
        /// Indicates that the provided custom locking model should be used.
        /// </summary>
        /// <typeparam name="TLockingModel">The custom locking model type.</typeparam>
        /// <returns>The current <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition LockingWith<TLockingModel>() where TLockingModel : FileAppender.LockingModelBase, new()
        {
            _lockingModel = new TLockingModel();
            return this;
        }

        /// <summary>
        /// Configures whether log messages should be appended to the file or that it
        /// should be overwritten.
        /// </summary>
        /// <param name="append">Whether to append to the file.</param>
        /// <returns>The current <see cref="FileAppenderDefinition"/> instance.</returns>
        public FileAppenderDefinition Append(bool append)
        {
            _appendToFile = append;
            return this;
        }

        protected override AppenderSkeleton CreateAppender()
        {
            return new FileAppender {
                File = _name, 
                LockingModel = _lockingModel, 
                AppendToFile = _appendToFile
            };
        }
    }
}
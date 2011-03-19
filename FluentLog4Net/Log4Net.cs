using FluentLog4Net.Configuration;

namespace FluentLog4Net
{
    /// <summary>
    /// The main API entry point for fluently configuring log4net.
    /// </summary>
    public static class Log4Net
    {
        /// <summary>
        /// Begins fluently configuring log4net.
        /// </summary>
        /// <returns>A <see cref="Log4NetConfiguration"/> instance.</returns>
        public static Log4NetConfiguration Configure()
        {
            return new Log4NetConfiguration();
        }
    }
}
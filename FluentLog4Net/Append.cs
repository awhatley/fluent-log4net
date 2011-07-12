using FluentLog4Net.Appenders;

namespace FluentLog4Net
{
    /// <summary>
    /// Helper class for creating re-usable appender definition references.
    /// </summary>
    public static class Append
    {
        /// <summary>
        /// Begins building a new appender definition.
        /// </summary>
        public static AppenderDefinitionBuilder To
        {
            get { return new AppenderDefinitionBuilder(); }
        }
    }
}
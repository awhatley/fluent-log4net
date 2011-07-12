using FluentLog4Net.Layouts;

namespace FluentLog4Net
{
    /// <summary>
    /// Helper class for creating re-usable layout definition references.
    /// </summary>
    public static class Layout
    {
        /// <summary>
        /// Begins building a new layout definition.
        /// </summary>
        public static LayoutDefinitionBuilder Using
        {
            get { return new LayoutDefinitionBuilder(); }
        }
    }
}
using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    public class XmlLog4jLayoutDefinition : LayoutDefinition<XmlLog4jLayoutDefinition>
    {
        private bool _includeLocation;
        private string _replacementString;

        public XmlLog4jLayoutDefinition IncludingFileAndLineNumber()
        {
            _includeLocation = true;
            return this;
        }

        public XmlLog4jLayoutDefinition ReplaceInvalidCharactersWith(string replacement)
        {
            _replacementString = replacement;
            return this;
        }

        protected override LayoutSkeleton CreateLayout()
        {
            return new XmlLayoutSchemaLog4j {
                LocationInfo = _includeLocation,
                InvalidCharReplacement = _replacementString,
            };
        }
    }
}
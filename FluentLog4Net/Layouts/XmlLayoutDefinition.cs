using log4net.Layout;

namespace FluentLog4Net.Layouts
{
    public class XmlLayoutDefinition : LayoutDefinition<XmlLayoutDefinition>
    {
        private bool _includeLocation;
        private string _elementPrefix;
        private bool _encodeMessage;
        private bool _encodeProperties;
        private string _replacementString;

        public XmlLayoutDefinition IncludingFileAndLineNumber()
        {
            _includeLocation = true;
            return this;
        }

        public XmlLayoutDefinition ElementsPrefixedWith(string prefix)
        {
            _elementPrefix = prefix;
            return this;
        }

        public XmlLayoutDefinition Base64EncodeMessage()
        {
            _encodeMessage = true;
            return this;
        }

        public XmlLayoutDefinition Base64EncodeProperties()
        {
            _encodeProperties = true;
            return this;
        }

        public XmlLayoutDefinition ReplaceInvalidCharactersWith(string replacement)
        {
            _replacementString = replacement;
            return this;
        }

        protected override LayoutSkeleton CreateLayout()
        {
            return new XmlLayout {
                LocationInfo = _includeLocation,
                Prefix = _elementPrefix,
                Base64EncodeMessage = _encodeMessage,
                Base64EncodeProperties = _encodeProperties,
                InvalidCharReplacement = _replacementString,
            };
        }
    }
}
using System;

using log4net.Layout;
using log4net.Util;

namespace FluentLog4Net.Layouts
{
    public class FluentPatternLayoutDefinition : ILayoutDefinition
    {
        public FluentPatternLayoutDefinition Header(string header)
        {
            throw new NotImplementedException();
        }

        public FluentPatternLayoutDefinition Footer(string footer)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier AppDomain()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Logger(int precision)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Type(int precision)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Date(string format)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Exception()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier File()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Identity()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Location()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier LineNumber()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Level()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Message()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Method()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier NewLine()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier NestedDiagnosticContext()
        {
            throw new NotImplementedException();
        }

        public FluentPatternLayoutDefinition Pattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Property(string propertyName)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Timestamp()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Thread()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Username()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier UtcDate(string format)
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Custom<T>(string options) where T : PatternConverter
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Space()
        {
            throw new NotImplementedException();
        }

        public PatternLayoutDefinitionModifier Literal(string text)
        {
            throw new NotImplementedException();
        }

        public ILayout CreateLayout()
        {
            return new PatternLayout();
        }

        public class PatternLayoutDefinitionModifier : FluentPatternLayoutDefinition
        {
            public PatternLayoutDefinitionModifier RightJustified()
            {
                throw new NotImplementedException();
            }

            public PatternLayoutDefinitionModifier LeftJustified()
            {
                throw new NotImplementedException();
            }

            public PatternLayoutDefinitionModifier MinimumWidth(int width)
            {
                throw new NotImplementedException();
            }

            public PatternLayoutDefinitionModifier MaximumWidth(int width)
            {
                throw new NotImplementedException();
            }
        }
    }
}
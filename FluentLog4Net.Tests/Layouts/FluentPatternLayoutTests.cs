using NUnit.Framework;

using log4net.Layout;
using log4net.Util;

namespace FluentLog4Net.Layouts
{
    [TestFixture]
    public class FluentPatternLayoutTests
    {
        [Test]
        public void Header()
        {
            const string header = "HEADER";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Header(header);
            var layout = ((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.Header, Is.EqualTo(header));
        }

        [Test]
        public void Footer()
        {
            const string footer = "FOOTER";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Footer(footer);
            var layout = ((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.Footer, Is.EqualTo(footer));
        }

        [Test]
        public void AppDomain()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.AppDomain();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%appdomain"));
        }

        [Test]
        public void Logger()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Logger();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%logger"));
        }

        [Test]
        public void LoggerWithPrecision()
        {
            const int precision = 5;
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Logger(precision);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%logger{" + precision + "}"));
        }

        [Test]
        public void Type()
        {
            const int precision = 5;
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Type(precision);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%type{" + precision + "}"));
        }

        [Test]
        public void Date()
        {
            const string format = "dd MMM yyyy HH:mm:ss,fff";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Date(format);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%date{" + format + "}"));
        }

        [Test]
        public void Exception()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Exception();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%exception"));
        }

        [Test]
        public void File()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.File();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%file"));
        }

        [Test]
        public void Identity()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Identity();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%identity"));
        }

        [Test]
        public void Location()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Location();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%location"));
        }

        [Test]
        public void LineNumber()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.LineNumber();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%line"));
        }

        [Test]
        public void Level()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Level();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%level"));
        }

        [Test]
        public void Message()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Message();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%message"));
        }

        [Test]
        public void Method()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Method();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%method"));
        }

        [Test]
        public void NewLine()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.NewLine();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%newline"));
        }

        [Test]
        public void NestedDiagnosticContext()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.NestedDiagnosticContext();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%ndc"));
        }

        [Test]
        public void Property()
        {
            const string propertyName = "foobar";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Property(propertyName);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%property{" + propertyName + "}"));
        }

        [Test]
        public void Timestamp()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Timestamp();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%timestamp"));
        }

        [Test]
        public void Thread()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Thread();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%thread"));
        }

        [Test]
        public void Username()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Username();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%username"));
        }

        [Test]
        public void UtcDate()
        {
            const string format = "dd MMM yyyy HH:mm:ss,fff";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.UtcDate(format);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%utcdate{" + format + "}"));
        }

        [Test]
        public void Custom()
        {
            const string name = "foobar";
            const string options = "blah, foo";

            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Custom<PatternConverter>(name, options);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%foobar{blah, foo}"));
        }

        [Test]
        public void Pattern()
        {
            const string pattern = "%foobarbaz{blahblah}%message%newline";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Pattern(pattern);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo(pattern));
        }

        [Test]
        public void Space()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Space();
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo(" "));
        }

        [Test]
        public void Literal()
        {
            const string literal = "foo bar 50% baz";
            var definition = new FluentPatternLayoutDefinition();
            
            var child = definition.Literal(literal);
            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo(literal.Replace("%", "%%")));
        }

        [Test]
        public void ComplexPattern()
        {
            var definition = new FluentPatternLayoutDefinition();
            
            definition
                .Timestamp().RightJustified().MaximumWidth(10)
                .Space()
                .Level().LeftJustified().MinimumWidth(5)
                .Space()
                .Literal("[").Thread().Literal("]:")
                .Space()
                .Message().NewLine();

            var layout = (PatternLayout)((ILayoutDefinition)definition).CreateLayout();

            Assert.That(layout.ConversionPattern, Is.EqualTo("%.10timestamp %-5level [%thread]: %message%newline"));
        }
    }
}
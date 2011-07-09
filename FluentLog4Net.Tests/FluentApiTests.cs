using System;
using System.IO;

using FluentLog4Net.Appenders;
using FluentLog4Net.ErrorHandlers;
using FluentLog4Net.Filters;
using FluentLog4Net.Layouts;

using NUnit.Framework;

using log4net.Appender;
using log4net.Core;
using log4net.ObjectRenderer;

namespace FluentLog4Net
{
    // These are not actually tests, just (overly verbose) demonstrations of the full API

    [TestFixture]
    [Ignore]
    public class FluentApiTests
    {
        private IFilterDefinition _myFilter;
        private IAppenderDefinition _myAppender;
        private ILayoutDefinition _myLayout;
        private IErrorHandlerDefinition _myErrorHandler;
        private IObjectRenderer _myRenderer;

        [Test]
        public void FluentBuilderExamples()
        {
            _myFilter = null;
            _myAppender = Append.To.Console(c => c.Targeting.ConsoleOut());
            _myLayout = Layout.Using.Pattern("%message%newline");
            _myErrorHandler = Handle.Errors.OnlyOnce(h => h.PrefixedBy("ERROR"));
            _myRenderer = null;
        }

        [Test]
        public void ConfigurationApiExample()
        {
            Log4Net.Configure()
                .Global.Threshold(Level.Debug)

                .Logging.Default(log => log
                    .At(Level.Error)
                    .To.Console(c => c
                        .Targeting.ConsoleOut()
                        .Format.Pattern("%-5level [%thread]: %message%newline")
                        .Apply.Filter(_myFilter)
                        .HandleErrors.OnlyOnce(h => h.PrefixedBy("ERROR")))

                    .To.ColoredConsole(c => c
                        .Targeting.ConsoleError()
                        .Format.Pattern(p => p
                            .Level().LeftJustified().MinimumWidth(5)
                            .Space()
                            .Literal("[").Thread().Literal("]:")
                            .Space()
                            .Message().NewLine())                        
                        .Apply.Filter(_myFilter)
                        .HandleErrors.OnlyOnce(h => h.PrefixedBy("ERROR")))

                    .To.File(f => f
                        .Append().LockingMinimally()
                        .Format.Layout(_myLayout)
                        .Apply.Filter(_myFilter)
                        .HandleErrors.With(_myErrorHandler))

                    .To.Appender(_myAppender)
                    .To.Appender<MyAppenderDefinition>())

                .Logging.For<DateTime>(log => log
                    .At(Level.Error)
                    .InheritAppenders(false)
                    .To.Appender(_myAppender))

                .Logging.For(typeof(TimeSpan), log => log
                    .At(Level.Error)
                    .InheritAppenders(false)
                    .To.Appender(_myAppender))

                .Logging.For("MyLogger", log => log
                    .At(Level.Error)
                    .InheritAppenders(false)
                    .To.Appender(_myAppender))

                .Render.Type<Int16>().Using(_myRenderer)
                .Render.Type<Int32>().Using<MyObjectRenderer>()
                .Render.Type<Int64>().Using(typeof(MyObjectRenderer))
                .Render.Type<IntPtr>().Using((map, obj, writer) => writer.Write(obj))

                .Render.Type(typeof(Decimal)).Using(new MyObjectRenderer())
                .Render.Type(typeof(Single)).Using(typeof(MyObjectRenderer))
                .Render.Type(typeof(Double)).Using<MyObjectRenderer>()
                .Render.Type(typeof(Array)).Using((map, obj, writer) => writer.Write(obj))

                .ApplyConfiguration();
        }

        private class MyAppenderDefinition : IAppenderDefinition
        {
            public IAppender CreateAppender()
            {
                throw new NotImplementedException();
            }
        }

        private class MyObjectRenderer : IObjectRenderer
        {
            public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
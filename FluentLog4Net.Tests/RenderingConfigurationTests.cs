using System;
using System.IO;

using log4net;
using log4net.ObjectRenderer;

using NUnit.Framework;

namespace FluentLog4Net
{
    [TestFixture]
    public class RenderingConfigurationTests
    {
        [SetUp]
        public void Setup()
        {
            LogManager.GetRepository().ResetConfiguration();
        }

        [Test]
        public void RenderRegistersRenderer()
        {
            Log4Net.Configure()                
                .Render.Type<Int32>().Using<Int32Renderer>()
                .Render.Type<Int64>().Using(typeof(Int64Renderer))
                .Render.Type(typeof(Single)).Using(typeof(SingleRenderer))
                .Render.Type(typeof(Double)).Using<DoubleRenderer>()
                .ApplyConfiguration();

            var repo = LogManager.GetRepository();
            Assert.That(repo.RendererMap.Get(typeof(Int32)), Is.TypeOf<Int32Renderer>());
            Assert.That(repo.RendererMap.Get(typeof(Int64)), Is.TypeOf<Int64Renderer>());
            Assert.That(repo.RendererMap.Get(typeof(Single)), Is.TypeOf<SingleRenderer>());
            Assert.That(repo.RendererMap.Get(typeof(Double)), Is.TypeOf<DoubleRenderer>());
        }

        [Test]
        public void RenderThrowsArgumentExceptionOnWrongType()
        {
            const string error = "Type {0} must implement IObjectRenderer to be configured as a renderer.";

            Assert.That(() =>
                Log4Net.Configure()
                    .Render.Type<Int32>().Using(typeof(Int32))
                    .ApplyConfiguration(),
                Throws.ArgumentException.With.Message.EqualTo(String.Format(error, typeof(Int32).FullName)));

            Assert.That(() =>
                Log4Net.Configure()
                    .Render.Type(typeof(Int64)).Using(typeof(Int64))
                    .ApplyConfiguration(),
                Throws.ArgumentException.With.Message.EqualTo(String.Format(error, typeof(Int64).FullName)));
        }

        private class Int32Renderer : IObjectRenderer
        {
            public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
            {
            }
        }

        private class Int64Renderer : IObjectRenderer
        {
            public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
            {
            }
        }

        private class SingleRenderer : IObjectRenderer
        {
            public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
            {
            }
        }

        private class DoubleRenderer : IObjectRenderer
        {
            public void RenderObject(RendererMap rendererMap, object obj, TextWriter writer)
            {
            }
        }
    }
}
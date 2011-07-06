using System;

using log4net.Appender;

namespace FluentLog4Net.Configuration
{
    public class FilterConfiguration<T>
    {
        public FilterConfiguration(T instance)
        {
        }

        public T Filter(IFilterDefinition filter)
        {
            throw new NotImplementedException();
        }

        public void ApplyTo(AppenderSkeleton appender)
        {
        }
    }

    public interface IFilterDefinition
    {
    }
}
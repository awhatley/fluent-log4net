using System.Collections.Generic;

namespace FluentLog4Net.Helpers
{
    public static class Extensions
    {
        public static T AddNew<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return item;
        }
    }
}
using System.Collections.Generic;

namespace FluentLog4Net.Helpers
{
    /// <summary>
    /// Contains miscellaneous extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the specified item to the list, returning the item added.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="list">An <see cref="IList{T}"/> instance.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>The item added.</returns>
        public static T AddItem<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return item;
        }
    }
}
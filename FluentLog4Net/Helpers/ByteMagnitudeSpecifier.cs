using System;

namespace FluentLog4Net.Helpers
{
    /// <summary>
    /// Used to transform a unit value into a specific order of magnitude.
    /// </summary>
    /// <typeparam name="T">The type currently being configured.</typeparam>
    public class ByteMagnitudeSpecifier<T>
    {
        private readonly T _parent;
        private readonly int _units;
        private readonly Action<long> _callback;

        internal ByteMagnitudeSpecifier(T parent, int units, Action<long> callback)
        {
            _parent = parent;
            _units = units;
            _callback = callback;
        }

        /// <summary>
        /// Transforms the unit value into megabytes.
        /// </summary>
        /// <returns>The current fluent configuration settings.</returns>
        public T Megabytes()
        {
            _callback(_units * 1024 * 1024);
            return _parent;
        }
    }
}
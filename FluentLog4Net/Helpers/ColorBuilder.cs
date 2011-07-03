namespace FluentLog4Net.Helpers
{
    /// <summary>
    /// Helper class to build color combination references for various appenders.
    /// </summary>
    public class ColorBuilder<T> where T : Color, new()
    {
        private readonly int _addedValue;
        private readonly int _foregroundValue;

        /// <summary>
        /// Represents the color black.
        /// </summary>
        public T Black { get { return new T { Foreground = _foregroundValue, Value = 0 + _addedValue }; } }

        /// <summary>
        /// Represents the color white.
        /// </summary>
        public T White { get { return new T { Foreground = _foregroundValue, Value = 7 + _addedValue }; } }

        /// <summary>
        /// Represents the color red.
        /// </summary>
        public T Red { get { return new T { Foreground = _foregroundValue, Value = 4 + _addedValue }; } }

        /// <summary>
        /// Represents the color green.
        /// </summary>
        public T Green { get { return new T { Foreground = _foregroundValue, Value = 2 + _addedValue }; } }

        /// <summary>
        /// Represents the color blue.
        /// </summary>
        public T Blue { get { return new T { Foreground = _foregroundValue, Value = 1 + _addedValue }; } }

        /// <summary>
        /// Represents the color cyan.
        /// </summary>
        public T Cyan { get { return new T { Foreground = _foregroundValue, Value = 3 + _addedValue }; } }

        /// <summary>
        /// Represents the color magenta.
        /// </summary>
        public T Magenta { get { return new T { Foreground = _foregroundValue, Value = 5 + _addedValue }; } }

        /// <summary>
        /// Represents the color yellow.
        /// </summary>
        public T Yellow { get { return new T { Foreground = _foregroundValue, Value = 6 + _addedValue }; } }

        /// <summary>
        /// Brightens the next color choice by marking it as high-intensity.
        /// </summary>
        public ColorBuilder<T> Bright { get { return new ColorBuilder<T>(8, _foregroundValue); } }

        internal ColorBuilder() : this(0, 0)
        {
        }

        internal ColorBuilder(int addedValue, int foregroundValue)
        {
            _addedValue = addedValue;
            _foregroundValue = foregroundValue;
        }
    }

    /// <summary>
    /// Represents a foreground color choice.
    /// </summary>
    public class ForegroundColor : Color
    {
        /// <summary>
        /// Combines the current foreground color with a background color.
        /// </summary>
        public ColorBuilder<Color> On
        {
            get { return new ColorBuilder<Color>(0, Foreground); }
        }

        internal override int Value
        {
            set { Foreground = value; }
        }
    }

    /// <summary>
    /// Represents a color choice.
    /// </summary>
    public class Color
    {
        internal int Foreground { get; set; }
        internal int Background { get; set; }

        internal virtual int Value
        {
            set { Background = value; }
        }
    }
}
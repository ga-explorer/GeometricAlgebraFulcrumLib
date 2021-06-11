namespace CodeComposerLib.HTMLold.Values
{
    /// <summary>
    /// https://www.w3.org/TR/css-values-3/#angles
    /// </summary>
    public sealed class HtmlValueAngleUnit : HtmlStoredValue
    {
        private static int _idCounter;


        /// <summary>
        /// No Unit is used
        /// </summary>
        public static HtmlValueAngleUnit None { get; }
            = new HtmlValueAngleUnit("");

        /// <summary>
        /// Degrees. There are 360 degrees in a full circle. 
        /// </summary>
        public static HtmlValueAngleUnit Degrees { get; }
            = new HtmlValueAngleUnit("deg");

        /// <summary>
        /// Gradians, also known as "gons" or "grades". There are 400 gradians in a full circle. 
        /// </summary>
        public static HtmlValueAngleUnit Gradians { get; }
            = new HtmlValueAngleUnit("grad");

        /// <summary>
        /// Radians. There are 2π radians in a full circle. 
        /// </summary>
        public static HtmlValueAngleUnit Radians { get; }
            = new HtmlValueAngleUnit("rad");

        /// <summary>
        /// Turns. There is 1 turn in a full circle. 
        /// </summary>
        public static HtmlValueAngleUnit Turns { get; }
            = new HtmlValueAngleUnit("turn");

        public int UnitId { get; }


        private HtmlValueAngleUnit(string value) : base(value)
        {
            UnitId = _idCounter++;
        }
    }
}
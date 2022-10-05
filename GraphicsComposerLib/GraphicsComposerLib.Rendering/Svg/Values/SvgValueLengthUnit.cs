namespace GraphicsComposerLib.Rendering.Svg.Values
{
    /// <summary>
    /// Units of length in SVG attributes and styles. See these pages for details:
    /// https://www.w3.org/TR/SVG11/types.html#DataTypeLength
    /// https://www.w3.org/TR/2008/REC-CSS2-20080411/syndata.html#length-units
    /// https://www.w3.org/TR/SVG/coords.html#Units
    /// </summary>
    public sealed class SvgValueLengthUnit : SvgStoredValue
    {
        private static int _idCounter;


        /// <summary>
        /// No Unit is used
        /// </summary>
        public static SvgValueLengthUnit None { get; } 
            = new SvgValueLengthUnit("");

        /// <summary>
        /// Percentage
        /// </summary>
        public static SvgValueLengthUnit Percent { get; } 
            = new SvgValueLengthUnit("%");

        /// <summary>
        /// The 'font-size' of the relevant font 
        /// </summary>
        public static SvgValueLengthUnit FontSize { get; } 
            = new SvgValueLengthUnit("em");

        /// <summary>
        /// The 'x-height' of the relevant font 
        /// </summary>
        public static SvgValueLengthUnit XHeight { get; } 
            = new SvgValueLengthUnit("ex");

        /// <summary>
        /// Pixels, relative to the viewing device
        /// </summary>
        public static SvgValueLengthUnit Pixels { get; } 
            = new SvgValueLengthUnit("px");

        /// <summary>
        /// Inches -- 1 inch is equal to 2.54 centimeters
        /// 1 Inch equals 90 Pixels
        /// </summary>
        public static SvgValueLengthUnit Inches { get; } 
            = new SvgValueLengthUnit("in");

        /// <summary>
        /// Centimeters
        /// 1 Centimeter would be 35.43307 Pixels
        /// </summary>
        public static SvgValueLengthUnit Centimeters { get; } 
            = new SvgValueLengthUnit("cm");

        /// <summary>
        /// Millimeters
        /// 1 Millimeter would be 3.543307 Pixels
        /// </summary>
        public static SvgValueLengthUnit Millimeters { get; } 
            = new SvgValueLengthUnit("mm");

        /// <summary>
        /// Points -- the points used by CSS2 are equal to 1/72th of an inch
        /// 1 Point equals 1.25 Pixels
        /// </summary>
        public static SvgValueLengthUnit Points { get; } 
            = new SvgValueLengthUnit("pt");

        /// <summary>
        /// Picas -- 1 pica is equal to 12 points
        /// 1 Pica equals 15 Pixels
        /// </summary>
        public static SvgValueLengthUnit Picas { get; } 
            = new SvgValueLengthUnit("pc");


        public int UnitId { get; }

        private SvgValueLengthUnit(string unitSymbol) : base(unitSymbol)
        {
            UnitId = _idCounter++;
        }
    }
}

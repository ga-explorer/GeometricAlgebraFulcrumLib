namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

/// <summary>
/// Units of length in SVG attributes and styles. See these pages for details:
/// https://www.w3.org/TR/SVG11/types.html#DataTypeLength
/// https://www.w3.org/TR/2008/REC-CSS2-20080411/syndata.html#length-units
/// https://www.w3.org/TR/SVG/coords.html#Units
/// </summary>
public sealed class HtmlValueLengthUnit : HtmlStoredValue
{
    private static int _idCounter;


    /// <summary>
    /// No Unit is used
    /// </summary>
    public static HtmlValueLengthUnit None { get; } 
        = new HtmlValueLengthUnit("");

    /// <summary>
    /// Percentage
    /// </summary>
    public static HtmlValueLengthUnit Percent { get; } 
        = new HtmlValueLengthUnit("%");

    /// <summary>
    /// The 'font-size' of the relevant font 
    /// </summary>
    public static HtmlValueLengthUnit FontSize { get; } 
        = new HtmlValueLengthUnit("em");

    /// <summary>
    /// The 'x-height' of the relevant font 
    /// </summary>
    public static HtmlValueLengthUnit XHeight { get; } 
        = new HtmlValueLengthUnit("ex");

    /// <summary>
    /// Pixels, relative to the viewing device
    /// </summary>
    public static HtmlValueLengthUnit Pixels { get; } 
        = new HtmlValueLengthUnit("px");

    /// <summary>
    /// Inches -- 1 inch is equal to 2.54 centimeters
    /// 1 Inch equals 90 Pixels
    /// </summary>
    public static HtmlValueLengthUnit Inches { get; } 
        = new HtmlValueLengthUnit("in");

    /// <summary>
    /// Centimeters
    /// 1 Centimeter would be 35.43307 Pixels
    /// </summary>
    public static HtmlValueLengthUnit Centimeters { get; } 
        = new HtmlValueLengthUnit("cm");

    /// <summary>
    /// Millimeters
    /// 1 Millimeter would be 3.543307 Pixels
    /// </summary>
    public static HtmlValueLengthUnit Millimeters { get; } 
        = new HtmlValueLengthUnit("mm");

    /// <summary>
    /// Points -- the points used by CSS2 are equal to 1/72th of an inch
    /// 1 Point equals 1.25 Pixels
    /// </summary>
    public static HtmlValueLengthUnit Points { get; } 
        = new HtmlValueLengthUnit("pt");

    /// <summary>
    /// Picas -- 1 pica is equal to 12 points
    /// 1 Pica equals 15 Pixels
    /// </summary>
    public static HtmlValueLengthUnit Picas { get; } 
        = new HtmlValueLengthUnit("pc");


    public int UnitId { get; }

    private HtmlValueLengthUnit(string unitSymbol) : base(unitSymbol)
    {
        UnitId = _idCounter++;
    }
}
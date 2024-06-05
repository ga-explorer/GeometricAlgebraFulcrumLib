using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Values;

/// <summary>
/// Units of length in SVG attributes and styles. See these pages for details:
/// https://www.w3.org/TR/SVG11/types.html#DataTypeLength
/// https://www.w3.org/TR/2008/REC-CSS2-20080411/syndata.html#length-units
/// https://www.w3.org/TR/SVG/coords.html#Units
/// </summary>
public sealed class SvgLengthUnit : 
    SvgStoredValue
{
    private static int _idCounter;


    /// <summary>
    /// No Unit is used
    /// </summary>
    public static SvgLengthUnit None { get; } 
        = new SvgLengthUnit("");

    /// <summary>
    /// Percentage
    /// </summary>
    public static SvgLengthUnit Percent { get; } 
        = new SvgLengthUnit("%");

    /// <summary>
    /// The 'font-size' of the relevant font 
    /// </summary>
    public static SvgLengthUnit FontSize { get; } 
        = new SvgLengthUnit("em");

    /// <summary>
    /// The 'x-height' of the relevant font 
    /// </summary>
    public static SvgLengthUnit XHeight { get; } 
        = new SvgLengthUnit("ex");

    /// <summary>
    /// Pixels, relative to the viewing device
    /// </summary>
    public static SvgLengthUnit Pixels { get; } 
        = new SvgLengthUnit("px");

    /// <summary>
    /// Inches -- 1 inch is equal to 2.54 centimeters
    /// 1 Inch equals 90 Pixels
    /// </summary>
    public static SvgLengthUnit Inches { get; } 
        = new SvgLengthUnit("in");

    /// <summary>
    /// Centimeters
    /// 1 Centimeter would be 35.43307 Pixels
    /// </summary>
    public static SvgLengthUnit Centimeters { get; } 
        = new SvgLengthUnit("cm");

    /// <summary>
    /// Millimeters
    /// 1 Millimeter would be 3.543307 Pixels
    /// </summary>
    public static SvgLengthUnit Millimeters { get; } 
        = new SvgLengthUnit("mm");

    /// <summary>
    /// Points -- the points used by CSS2 are equal to 1/72th of an inch
    /// 1 Point equals 1.25 Pixels
    /// </summary>
    public static SvgLengthUnit Points { get; } 
        = new SvgLengthUnit("pt");

    /// <summary>
    /// Picas -- 1 pica is equal to 12 points
    /// 1 Pica equals 15 Pixels
    /// </summary>
    public static SvgLengthUnit Picas { get; } 
        = new SvgLengthUnit("pc");


    public int UnitId { get; }

    private SvgLengthUnit(string unitSymbol) 
        : base(unitSymbol)
    {
        UnitId = _idCounter++;
    }

        
    public SvgLength CreateLength(double length)
    {
        return SvgLength.Create(this, length);
    }

    public SvgPoint CreatePoint(double x, double y)
    {
        return SvgPoint.Create(this, x, y);
    }
        
    public SvgPoint CreatePoint(IPair<double> point)
    {
        return SvgPoint.Create(this, point);
    }
}
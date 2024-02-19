namespace WebComposerLib.Svg.Values;

/// <summary>
/// https://www.w3.org/TR/css-values-3/#angles
/// </summary>
public sealed class SvgValueAngleUnit : SvgStoredValue
{
    private static int _idCounter;


    /// <summary>
    /// No Unit is used
    /// </summary>
    public static SvgValueAngleUnit None { get; }
        = new SvgValueAngleUnit("");

    /// <summary>
    /// Degrees. There are 360 degrees in a full circle. 
    /// </summary>
    public static SvgValueAngleUnit Degrees { get; }
        = new SvgValueAngleUnit("deg");

    /// <summary>
    /// Gradians, also known as "gons" or "grades". There are 400 gradians in a full circle. 
    /// </summary>
    public static SvgValueAngleUnit Gradians { get; }
        = new SvgValueAngleUnit("grad");

    /// <summary>
    /// Radians. There are 2π radians in a full circle. 
    /// </summary>
    public static SvgValueAngleUnit Radians { get; }
        = new SvgValueAngleUnit("rad");

    /// <summary>
    /// Turns. There is 1 turn in a full circle. 
    /// </summary>
    public static SvgValueAngleUnit Turns { get; }
        = new SvgValueAngleUnit("turn");

    public int UnitId { get; }


    private SvgValueAngleUnit(string value) : base(value)
    {
        UnitId = _idCounter++;
    }
}
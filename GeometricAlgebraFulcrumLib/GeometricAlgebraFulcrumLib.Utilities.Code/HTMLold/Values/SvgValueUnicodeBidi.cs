namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Values;

public sealed class HtmlValueUnicodeBidi : HtmlStoredValue
{
    public static HtmlValueUnicodeBidi Normal { get; }
        = new HtmlValueUnicodeBidi("normal");

    public static HtmlValueUnicodeBidi Embed { get; }
        = new HtmlValueUnicodeBidi("embed");

    public static HtmlValueUnicodeBidi BidiOverride { get; }
        = new HtmlValueUnicodeBidi("bidi-override");

    public static HtmlValueUnicodeBidi Inherit { get; }
        = new HtmlValueUnicodeBidi("inherit");


    private HtmlValueUnicodeBidi(string value) : base(value)
    {
    }
}
namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;

public abstract class MathMlColorValue : MathMlValue
{
    public static MathMlEmptyColorValue Empty { get; }
        = new MathMlEmptyColorValue();

    public static MathMlNamedColorValue Transparent { get; }
        = new MathMlNamedColorValue("transparent");

    public static MathMlRgbColorValue Rgb(System.Drawing.Color value)
    {
        return new MathMlRgbColorValue()
        {
            Value = value
        };
    }
}
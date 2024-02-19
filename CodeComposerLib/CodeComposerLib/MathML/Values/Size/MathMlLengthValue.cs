using CodeComposerLib.MathML.Constants;

namespace CodeComposerLib.MathML.Values.Size;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/CSS/length
/// </summary>
public abstract class MathMlLengthValue : MathMlValue
{
    public static MathMlEmptyLengthValue Empty { get; }
        = new MathMlEmptyLengthValue();

    public static MathMlNamedSizeValue SmallSize { get; }
        = new MathMlNamedSizeValue("small");

    public static MathMlNamedSizeValue NormalSize { get; }
        = new MathMlNamedSizeValue("normal");

    public static MathMlNamedSizeValue BigSize { get; }
        = new MathMlNamedSizeValue("big");

    public static MathMlNamedSizeValue Infinity { get; }
        = new MathMlNamedSizeValue("infinity");


    public static MathMlRelativeLengthValue Create(double value, MathMlRelativeLengthUnit unit)
    {
        return new MathMlRelativeLengthValue()
        {
            Value = value,
            Unit = unit
        };
    }

    public static MathMlPercentageLengthValue Create(double value, MathMlPercentageLengthUnit unit)
    {
        return new MathMlPercentageLengthValue()
        {
            Value = value,
            Unit = unit
        };
    }

    public static MathMlAbsoluteLengthValue Create(double value, MathMlAbsoluteLengthUnit unit)
    {
        return new MathMlAbsoluteLengthValue()
        {
            Value = value,
            Unit = unit
        };
    }

    public static MathMlUnitlessSizeValue Create(double value)
    {
        return new MathMlUnitlessSizeValue()
        {
            Value = value
        };
    }
}
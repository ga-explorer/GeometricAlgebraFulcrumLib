using System.Text;

namespace CodeComposerLib.HTMLold.Values;

public sealed class HtmlValueLength : HtmlComputedValue
{
    public static HtmlValueLength Inherit { get; } 
        = new HtmlValueLength();

    public static HtmlValueLength Create(double lengthValue)
    {
        return new HtmlValueLength(lengthValue);
    }

    public static HtmlValueLength Create(double lengthValue, HtmlValueLengthUnit unit)
    {
        return new HtmlValueLength(lengthValue, unit);
    }


    public bool IsInherit { get; }

    public double LengthValue { get; set; }


    private HtmlValueLengthUnit _unit = HtmlValueLengthUnit.None;
    public HtmlValueLengthUnit Unit
    {
        get { return _unit; }
        set { _unit = value ?? HtmlValueLengthUnit.None; }
    }

    public override string ValueText
        => IsInherit
            ? "inherit"
            : new StringBuilder(32)
                .Append(LengthValue.ToString("G"))
                .Append(_unit.ValueText)
                .ToString();


    private HtmlValueLength()
    {
        IsInherit = true;
    }

    private HtmlValueLength(double lengthValue)
    {
        IsInherit = false;
        LengthValue = lengthValue;
    }

    private HtmlValueLength(double lengthValue, HtmlValueLengthUnit unit)
    {
        IsInherit = false;
        LengthValue = lengthValue;
        _unit = unit ?? HtmlValueLengthUnit.None;
    }


    public bool IsEqualToZero()
    {
        return IsInherit == false &&
               LengthValue == 0 &&
               _unit.UnitId == HtmlValueLengthUnit.None.UnitId;
    }

    public bool IsEqualTo(double lengthValue)
    {
        return IsInherit == false &&
               LengthValue == lengthValue &&
               _unit.UnitId == HtmlValueLengthUnit.None.UnitId;
    }

    public bool IsEqualTo(double lengthValue, HtmlValueLengthUnit unit)
    {
        return IsInherit == false &&
               LengthValue == lengthValue &&
               _unit.UnitId == unit.UnitId;
    }

    public bool IsEqualToInherit()
    {
        return IsInherit;
    }
}
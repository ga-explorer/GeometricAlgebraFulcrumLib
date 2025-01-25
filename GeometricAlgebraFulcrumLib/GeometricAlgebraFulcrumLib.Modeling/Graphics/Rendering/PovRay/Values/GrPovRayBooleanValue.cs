namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayBooleanValue :
    GrPovRayValue<bool>
{
    public static GrPovRayBooleanValue On { get; } 
        = new GrPovRayBooleanValue("on");

    public static GrPovRayBooleanValue Off { get; } 
        = new GrPovRayBooleanValue("off");

    public static GrPovRayBooleanValue True { get; } 
        = new GrPovRayBooleanValue("true");

    public static GrPovRayBooleanValue False { get; } 
        = new GrPovRayBooleanValue("false");

    public static GrPovRayBooleanValue One { get; } 
        = new GrPovRayBooleanValue("1");

    public static GrPovRayBooleanValue Zero { get; } 
        = new GrPovRayBooleanValue("0");

    public static GrPovRayBooleanValue OnOff(bool value)
    {
        return value ? On : Off;
    }
    
    public static GrPovRayBooleanValue TrueFalse(bool value)
    {
        return value ? True : False;
    }
    
    public static GrPovRayBooleanValue OneZero(bool value)
    {
        return value ? One : Zero;
    }



    public static implicit operator GrPovRayBooleanValue(string valueText)
    {
        return new GrPovRayBooleanValue(valueText);
    }

    public static implicit operator GrPovRayBooleanValue(bool value)
    {
        return new GrPovRayBooleanValue(value);
    }


    private GrPovRayBooleanValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayBooleanValue(bool value)
        : base(value)
    {
    }


    public override string GetPovRayCode()
    {
        return string.IsNullOrEmpty(ValueText) 
            ? Value.GetPovRayCode() 
            : ValueText;
    }
}
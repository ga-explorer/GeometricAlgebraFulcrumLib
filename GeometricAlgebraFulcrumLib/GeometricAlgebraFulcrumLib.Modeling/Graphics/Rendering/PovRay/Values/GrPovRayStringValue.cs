using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayStringValue :
    GrPovRayValue<string>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayStringValue(string valueText)
    {
        return new GrPovRayStringValue(valueText);
    }

    public static GrPovRayStringValue CreateLiteralFromValue(string value)
    {
        return new GrPovRayStringValue(value.ValueToQuotedLiteral());
    }
    
    public static GrPovRayStringValue CreateLiteralFromLiteral(string value)
    {
        return new GrPovRayStringValue(value);
    }


    private GrPovRayStringValue(string valueText)
        : base(valueText)
    {
    }
    

    public override string GetPovRayCode()
    {
        return ValueText;
    }
}
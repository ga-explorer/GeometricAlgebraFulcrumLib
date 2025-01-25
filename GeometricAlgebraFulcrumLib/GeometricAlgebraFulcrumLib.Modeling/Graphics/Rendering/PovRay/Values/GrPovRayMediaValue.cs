using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Media;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayMediaValue :
    GrPovRayValue<GrPovRayMedia>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayMediaValue(string valueText)
    {
        return new GrPovRayMediaValue(valueText);
    }

    public static implicit operator GrPovRayMediaValue(GrPovRayMedia value)
    {
        return new GrPovRayMediaValue(value);
    }
    

    private GrPovRayMediaValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayMediaValue(GrPovRayMedia value)
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
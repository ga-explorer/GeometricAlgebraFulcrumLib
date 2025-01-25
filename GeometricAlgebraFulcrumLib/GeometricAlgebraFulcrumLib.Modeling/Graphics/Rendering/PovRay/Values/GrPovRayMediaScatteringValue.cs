using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Media;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayMediaScatteringValue :
    GrPovRayValue<GrPovRayMediaScattering>
{
    public static implicit operator GrPovRayMediaScatteringValue(string valueText)
    {
        return new GrPovRayMediaScatteringValue(valueText);
    }

    public static implicit operator GrPovRayMediaScatteringValue(GrPovRayMediaScattering value)
    {
        return new GrPovRayMediaScatteringValue(value);
    }


    private GrPovRayMediaScatteringValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayMediaScatteringValue(GrPovRayMediaScattering value)
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
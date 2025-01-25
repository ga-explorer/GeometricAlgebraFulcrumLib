using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Finishes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayFinishValue :
    GrPovRayValue<GrPovRayFinish>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayFinishValue(string valueText)
    {
        return new GrPovRayFinishValue(valueText);
    }

    public static implicit operator GrPovRayFinishValue(GrPovRayFinish value)
    {
        return new GrPovRayFinishValue(value);
    }
    

    private GrPovRayFinishValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayFinishValue(GrPovRayFinish value)
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
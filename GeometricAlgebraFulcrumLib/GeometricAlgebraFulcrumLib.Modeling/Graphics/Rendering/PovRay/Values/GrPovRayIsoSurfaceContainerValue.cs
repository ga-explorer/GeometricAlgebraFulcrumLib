using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.FSP;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayIsoSurfaceContainerValue :
    GrPovRayValue<GrPovRayIsoSurfaceContainer>,
    IGrPovRayRValue
{
    public static implicit operator GrPovRayIsoSurfaceContainerValue(string valueText)
    {
        return new GrPovRayIsoSurfaceContainerValue(valueText);
    }

    public static implicit operator GrPovRayIsoSurfaceContainerValue(GrPovRayIsoSurfaceContainer value)
    {
        return new GrPovRayIsoSurfaceContainerValue(value);
    }
    

    private GrPovRayIsoSurfaceContainerValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayIsoSurfaceContainerValue(GrPovRayIsoSurfaceContainer value)
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
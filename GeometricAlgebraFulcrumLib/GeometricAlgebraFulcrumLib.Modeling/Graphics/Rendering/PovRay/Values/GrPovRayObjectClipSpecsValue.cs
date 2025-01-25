using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayObjectClipSpecsValue :
    GrPovRayValue<GrPovRayObjectClipSpecs>
{
    public static GrPovRayObjectClipSpecsValue UsingBoundingObject { get; }
        = new GrPovRayObjectClipSpecsValue(
            GrPovRayObjectClipSpecs.UsingBoundingObject
        );

    public static GrPovRayObjectClipSpecsValue UsingObject(IGrPovRayObject clippingObject)
    {
        return new GrPovRayObjectClipSpecsValue(
            GrPovRayObjectClipSpecs.UsingObject(clippingObject)
        );
    }


    public static implicit operator GrPovRayObjectClipSpecsValue(string valueText)
    {
        return new GrPovRayObjectClipSpecsValue(valueText);
    }

    public static implicit operator GrPovRayObjectClipSpecsValue(GrPovRayObjectClipSpecs value)
    {
        return new GrPovRayObjectClipSpecsValue(value);
    }
    

    private GrPovRayObjectClipSpecsValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayObjectClipSpecsValue(GrPovRayObjectClipSpecs value)
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
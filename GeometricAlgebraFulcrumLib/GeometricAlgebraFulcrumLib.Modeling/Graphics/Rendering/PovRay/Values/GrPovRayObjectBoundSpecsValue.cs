using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

public sealed class GrPovRayObjectBoundSpecsValue :
    GrPovRayValue<GrPovRayObjectBoundSpecs>
{
    public static GrPovRayObjectBoundSpecsValue UsingClippingObject { get; }
        = new GrPovRayObjectBoundSpecsValue(
            GrPovRayObjectBoundSpecs.UsingClippingObject
        );

    public static GrPovRayObjectBoundSpecsValue UsingObject(IGrPovRayObject clippingObject)
    {
        return new GrPovRayObjectBoundSpecsValue(
            GrPovRayObjectBoundSpecs.UsingObject(clippingObject)
        );
    }


    public static implicit operator GrPovRayObjectBoundSpecsValue(string valueText)
    {
        return new GrPovRayObjectBoundSpecsValue(valueText);
    }

    public static implicit operator GrPovRayObjectBoundSpecsValue(GrPovRayObjectBoundSpecs value)
    {
        return new GrPovRayObjectBoundSpecsValue(value);
    }
    

    private GrPovRayObjectBoundSpecsValue(string valueText)
        : base(valueText)
    {
    }

    private GrPovRayObjectBoundSpecsValue(GrPovRayObjectBoundSpecs value)
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
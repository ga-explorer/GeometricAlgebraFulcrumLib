using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsEllipseOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsVector2Value? Radius
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Radius");
        set => SetAttributeValue("Radius", value);
    }
        

    public GrKonvaJsEllipseOptions()
    {
    }

    public GrKonvaJsEllipseOptions(GrKonvaJsEllipseOptions options)
    {
        SetAttributeValues(options);
    }
}
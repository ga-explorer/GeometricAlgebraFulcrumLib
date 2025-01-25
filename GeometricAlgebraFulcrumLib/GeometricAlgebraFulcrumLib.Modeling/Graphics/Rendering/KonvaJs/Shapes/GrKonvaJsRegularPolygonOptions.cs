using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsRegularPolygonOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsInt32Value? Sides
    {
        get => GetAttributeValueOrNull<GrKonvaJsInt32Value>("Sides");
        set => SetAttributeValue("Sides", value);
    }

    public GrKonvaJsFloat32Value? Radius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
        set => SetAttributeValue("Radius", value);
    }
        

    public GrKonvaJsRegularPolygonOptions()
    {
    }

    public GrKonvaJsRegularPolygonOptions(GrKonvaJsRegularPolygonOptions options)
    {
        SetAttributeValues(options);
    }
}
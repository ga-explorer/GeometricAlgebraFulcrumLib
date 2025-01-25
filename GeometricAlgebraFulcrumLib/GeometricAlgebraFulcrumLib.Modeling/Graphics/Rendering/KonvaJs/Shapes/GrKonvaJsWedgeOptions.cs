using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsWedgeOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsFloat32Value? Radius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
        set => SetAttributeValue("Radius", value);
    }

    public GrKonvaJsBooleanValue? Clockwise
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("clockwise");
        set => SetAttributeValue("clockwise", value);
    }
        
    public GrKonvaJsFloat32Value? Angle
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Angle");
        set => SetAttributeValue("Angle", value);
    }


    public GrKonvaJsWedgeOptions()
    {
    }

    public GrKonvaJsWedgeOptions(GrKonvaJsWedgeOptions options)
    {
        SetAttributeValues(options);
    }
}
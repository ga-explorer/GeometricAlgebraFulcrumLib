using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsLineProperties :
    GrKonvaJsShapeBaseProperties
{
    public GrKonvaJsVector2ArrayValue? Points
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2ArrayValue>("Points");
        init => SetAttributeValue("Points", value);
    }
        
    public GrKonvaJsBooleanValue? Closed
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Closed");
        init => SetAttributeValue("Closed", value);
    }

    public GrKonvaJsBooleanValue? Bezier
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("Bezier");
        init => SetAttributeValue("Bezier", value);
    }
        
    public GrKonvaJsFloat32Value? Tension
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Tension");
        init => SetAttributeValue("Tension", value);
    }


        
}
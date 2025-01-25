using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsRegularPolygonProperties :
    GrKonvaJsShapeBaseProperties
{
    public GrKonvaJsFloat32Value? Radius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("Radius");
        set => SetAttributeValue("Radius", value);
    }

       
}
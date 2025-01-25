using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsEllipseProperties :
    GrKonvaJsShapeBaseProperties
{
    public GrKonvaJsVector2Value? Radius
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Radius");
        set => SetAttributeValue("Radius", value);
    }

    public GrKonvaJsFloat32Value? RadiusX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("RadiusX");
        set => SetAttributeValue("RadiusX", value);
    }
        
    public GrKonvaJsFloat32Value? RadiusY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("RadiusY");
        set => SetAttributeValue("RadiusY", value);
    }

        
}
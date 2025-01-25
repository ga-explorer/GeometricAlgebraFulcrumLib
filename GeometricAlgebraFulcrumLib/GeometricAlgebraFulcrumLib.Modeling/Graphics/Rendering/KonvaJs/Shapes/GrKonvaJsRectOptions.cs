using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsRectOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsFloat32Value? CornerRadius
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("cornerRadius");
        set => SetAttributeValue("cornerRadius", value);
    }
        

    public GrKonvaJsRectOptions()
    {
    }

    public GrKonvaJsRectOptions(GrKonvaJsRectOptions options)
    {
        SetAttributeValues(options);
    }
}
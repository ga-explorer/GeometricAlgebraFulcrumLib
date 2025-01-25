using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsShapeOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsCodeValue? SceneFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("SceneFunc");
        set => SetAttributeValue("SceneFunc", value);
    }
        

    public GrKonvaJsShapeOptions()
    {
    }

    public GrKonvaJsShapeOptions(GrKonvaJsShapeOptions options)
    {
        SetAttributeValues(options);
    }
}
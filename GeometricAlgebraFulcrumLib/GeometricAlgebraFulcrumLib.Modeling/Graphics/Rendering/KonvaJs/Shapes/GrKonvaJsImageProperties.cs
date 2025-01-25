using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsImageProperties :
    GrKonvaJsShapeBaseProperties
{
    public GrKonvaJsCodeValue? Image
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("Image");
        set => SetAttributeValue("Image", value);
    }


        
}
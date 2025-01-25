using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Shapes;

public class GrKonvaJsImageOptions :
    GrKonvaJsShapeBaseOptions
{
    public GrKonvaJsCodeValue? Image
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("Image");
        init => SetAttributeValue("Image", value);
    }
        
    public GrKonvaJsVector2Value? Clip
    {
        get => GetAttributeValueOrNull<GrKonvaJsVector2Value>("Clip");
        init => SetAttributeValue("Clip", value);
    }


    public GrKonvaJsImageOptions()
    {

    }
        
    public GrKonvaJsImageOptions(GrKonvaJsImageOptions options)
    {
        SetAttributeValues(options);
    }
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

public class GrKonvaJsLayerProperties :
    GrKonvaJsContainerProperties
{
    public GrKonvaJsBooleanValue? ClearBeforeDraw
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ClearBeforeDraw");
        init => SetAttributeValue("ClearBeforeDraw", value);
    }

    public GrKonvaJsBooleanValue? ImageSmoothingEnabled
    {
        get => GetAttributeValueOrNull<GrKonvaJsBooleanValue>("ImageSmoothingEnabled");
        init => SetAttributeValue("ImageSmoothingEnabled", value);
    }

        
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.KonvaJs.Containers;

public abstract class GrKonvaJsContainerProperties :
    GrKonvaJsNodeProperties
{
    public GrKonvaJsFloat32Value? ClipX
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipX");
        init => SetAttributeValue("ClipX", value);
    }

    public GrKonvaJsFloat32Value? ClipY
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipY");
        init => SetAttributeValue("ClipY", value);
    }

    public GrKonvaJsFloat32Value? ClipWidth
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipWidth");
        init => SetAttributeValue("ClipWidth", value);
    }

    public GrKonvaJsFloat32Value? ClipHeight
    {
        get => GetAttributeValueOrNull<GrKonvaJsFloat32Value>("ClipHeight");
        init => SetAttributeValue("ClipHeight", value);
    }

    public GrKonvaJsBoundingBoxValue? Clip
    {
        get => GetAttributeValueOrNull<GrKonvaJsBoundingBoxValue>("Clip");
        init => SetAttributeValue("Clip", value);
    }

    public GrKonvaJsCodeValue? ClipFunc
    {
        get => GetAttributeValueOrNull<GrKonvaJsCodeValue>("ClipFunc");
        init => SetAttributeValue("ClipFunc", value);
    }
}
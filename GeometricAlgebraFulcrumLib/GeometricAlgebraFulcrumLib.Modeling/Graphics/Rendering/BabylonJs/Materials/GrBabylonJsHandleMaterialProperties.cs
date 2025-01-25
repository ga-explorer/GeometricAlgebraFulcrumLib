using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Materials;

public sealed class GrBabylonJsHandleMaterialProperties :
    GrBabylonJsMaterialProperties
{
    public GrBabylonJsFloat32Value? AnimationLength
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("animationLength");
        set => SetAttributeValue("animationLength", value);
    }

    public GrBabylonJsColor3Value? BaseColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("baseColor");
        set => SetAttributeValue("baseColor", value);
    }

    public GrBabylonJsFloat32Value? BaseScale
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("baseScale");
        set => SetAttributeValue("baseScale", value);
    }

    public GrBabylonJsFloat32Value? DragScale
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("dragScale");
        set => SetAttributeValue("dragScale", value);
    }

    public GrBabylonJsColor3Value? HoverColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsColor3Value>("hoverColor");
        set => SetAttributeValue("hoverColor", value);
    }

    public GrBabylonJsFloat32Value? HoverScale
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("hoverScale");
        set => SetAttributeValue("hoverScale", value);
    }

    public GrBabylonJsFloat32Value? Hover
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("hover");
        set => SetAttributeValue("hover", value);
    }

    public GrBabylonJsFloat32Value? Drag
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("drag");
        set => SetAttributeValue("drag", value);
    }


    public GrBabylonJsHandleMaterialProperties()
    {

    }

    public GrBabylonJsHandleMaterialProperties(GrBabylonJsHandleMaterialProperties properties)
    {
        SetAttributeValues(properties);
    }
}
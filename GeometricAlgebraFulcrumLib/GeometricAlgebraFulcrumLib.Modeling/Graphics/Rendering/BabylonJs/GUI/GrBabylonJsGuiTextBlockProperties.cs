using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public sealed class GrBabylonJsGuiTextBlockProperties :
    GrBabylonJsGuiControlProperties
{
    public GrBabylonJsFloat32Value? LineSpacing
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("lineSpacing");
        set => SetAttributeValue("lineSpacing", value);
    }

    public GrBabylonJsStringValue? Text
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("text");
        set => SetAttributeValue("text", value);
    }

    public GrBabylonJsHorizontalAlignmentValue? TextHorizontalAlignment
    {
        get => GetAttributeValueOrNull<GrBabylonJsHorizontalAlignmentValue>("textHorizontalAlignment");
        set => SetAttributeValue("textHorizontalAlignment", value);
    }

    public GrBabylonJsVerticalAlignmentValue? TextVerticalAlignment
    {
        get => GetAttributeValueOrNull<GrBabylonJsVerticalAlignmentValue>("textVerticalAlignment");
        set => SetAttributeValue("textVerticalAlignment", value);
    }

    public GrBabylonJsBooleanValue? TextWrapping
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("textWrapping");
        set => SetAttributeValue("textWrapping", value);
    }

    public GrBabylonJsBooleanValue? LineThrough
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("lineThrough");
        set => SetAttributeValue("lineThrough", value);
    }

    public GrBabylonJsBooleanValue? ResizeToFit
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("resizeToFit");
        set => SetAttributeValue("resizeToFit", value);
    }

    public GrBabylonJsBooleanValue? Underline
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("underline");
        set => SetAttributeValue("underline", value);
    }

    public GrBabylonJsStringValue? WordDivider
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("wordDivider");
        set => SetAttributeValue("wordDivider", value);
    }

    public GrBabylonJsFloat32Value? OutlineWidth
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("outlineWidth");
        set => SetAttributeValue("outlineWidth", value);
    }

    public GrBabylonJsGuiColorValue? OutlineColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("outlineColor");
        set => SetAttributeValue("outlineColor", value);
    }


    public GrBabylonJsGuiTextBlockProperties()
    {
    }

    public GrBabylonJsGuiTextBlockProperties(GrBabylonJsGuiTextBlockProperties properties)
    {
        SetAttributeValues(properties);
    }
            
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public sealed class GrBabylonJsGuiStyleProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsStringValue? FontFamily
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontWeight");
        set => SetAttributeValue("fontWeight", value);
    }

    public GrBabylonJsFloat32Value? FontSize
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("fontSize");
        set => SetAttributeValue("fontSize", value);
    }

    public GrBabylonJsStringValue? FontStyle
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontStyle");
        set => SetAttributeValue("fontStyle", value);
    }

    public GrBabylonJsStringValue? FontWeight
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("fontWeight");
        set => SetAttributeValue("fontWeight", value);
    }


    public GrBabylonJsGuiStyleProperties()
    {
    }

    public GrBabylonJsGuiStyleProperties(GrBabylonJsGuiStyleProperties properties)
    {
        SetAttributeValues(properties);
    }
}
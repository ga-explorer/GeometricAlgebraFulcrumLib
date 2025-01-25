using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public class GrBabylonJsGuiStackPanelProperties :
    GrBabylonJsGuiContainerProperties
{
    public GrBabylonJsBooleanValue? IgnoreLayoutWarnings
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("ignoreLayoutWarnings");
        set => SetAttributeValue("ignoreLayoutWarnings", value);
    }

    public GrBabylonJsBooleanValue? IsVertical
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isVertical");
        set => SetAttributeValue("isVertical", value);
    }

    public GrBabylonJsFloat32Value? Spacing
    {
        get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("spacing");
        set => SetAttributeValue("spacing", value);
    }


    public GrBabylonJsGuiStackPanelProperties()
    {
    }

    public GrBabylonJsGuiStackPanelProperties(GrBabylonJsGuiStackPanelProperties properties)
    {
        SetAttributeValues(properties);
    }
}
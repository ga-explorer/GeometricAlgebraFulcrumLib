using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

public abstract class GrBabylonJsGuiContainerProperties :
    GrBabylonJsGuiControlProperties
{
    public GrBabylonJsBooleanValue? LogLayoutCycleErrors
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("logLayoutCycleErrors");
        set => SetAttributeValue("logLayoutCycleErrors", value);
    }

    public GrBabylonJsInt32Value? MaxLayoutCycle
    {
        get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("maxLayoutCycle");
        set => SetAttributeValue("maxLayoutCycle", value);
    }

    public GrBabylonJsBooleanValue? AdaptHeightToChildren
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("adaptHeightToChildren");
        set => SetAttributeValue("adaptHeightToChildren", value);
    }

    public GrBabylonJsBooleanValue? AdaptWidthToChildren
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("adaptWidthToChildren");
        set => SetAttributeValue("adaptWidthToChildren", value);
    }

    public GrBabylonJsBooleanValue? RenderToIntermediateTexture
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderToIntermediateTexture");
        set => SetAttributeValue("renderToIntermediateTexture", value);
    }

    public GrBabylonJsGuiColorValue? BackgroundColor
    {
        get => GetAttributeValueOrNull<GrBabylonJsGuiColorValue>("backgroundColor");
        set => SetAttributeValue("backgroundColor", value);
    }


    protected GrBabylonJsGuiContainerProperties()
    {

    }

    protected GrBabylonJsGuiContainerProperties(GrBabylonJsGuiContainerProperties properties)
    {
        SetAttributeValues(properties);
    }
}
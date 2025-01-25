using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsUtilityLayerRendererProperties :
    GrBabylonJsObjectProperties
{
    public GrBabylonJsBooleanValue? OnlyCheckPointerDownEvents
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("onlyCheckPointerDownEvents");
        set => SetAttributeValue("onlyCheckPointerDownEvents", value);
    }

    public GrBabylonJsSceneValue? OriginalScene
    {
        get => GetAttributeValueOrNull<GrBabylonJsSceneValue>("originalScene");
        set => SetAttributeValue("originalScene", value);
    }

    public GrBabylonJsSceneValue? UtilityLayerScene
    {
        get => GetAttributeValueOrNull<GrBabylonJsSceneValue>("utilityLayerScene");
        set => SetAttributeValue("utilityLayerScene", value);
    }

    public GrBabylonJsBooleanValue? PickUtilitySceneFirst
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("pickUtilitySceneFirst");
        set => SetAttributeValue("pickUtilitySceneFirst", value);
    }

    public GrBabylonJsBooleanValue? PickingEnabled
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("pickingEnabled");
        set => SetAttributeValue("pickingEnabled", value);
    }

    public GrBabylonJsBooleanValue? ProcessAllEvents
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("processAllEvents");
        set => SetAttributeValue("processAllEvents", value);
    }

    public GrBabylonJsBooleanValue? ShouldRender
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("shouldRender");
        set => SetAttributeValue("shouldRender", value);
    }


    public GrBabylonJsUtilityLayerRendererProperties()
    {
    }

    public GrBabylonJsUtilityLayerRendererProperties(GrBabylonJsUtilityLayerRendererProperties properties)
    {
        SetAttributeValues(properties);
    }
}
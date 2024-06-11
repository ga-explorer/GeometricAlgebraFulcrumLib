using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsUtilityLayerRenderer :
    GrBabylonJsObject
{
    public sealed class UtilityLayerRendererProperties :
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


        public UtilityLayerRendererProperties()
        {
        }

        public UtilityLayerRendererProperties(UtilityLayerRendererProperties properties)
        {
            SetAttributeValues(properties);
        }
    }

    protected override string ConstructorName
        => "new BABYLON.UtilityLayerRenderer";

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public GrBabylonJsBooleanValue HandleEvents { get; set; }
    
    public UtilityLayerRendererProperties Properties { get; private set; }
        = new UtilityLayerRendererProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    
    
    public GrBabylonJsUtilityLayerRenderer(string constName) 
        : base(constName)
    {
    }

    
    public GrBabylonJsUtilityLayerRenderer SetProperties(UtilityLayerRendererProperties properties)
    {
        Properties = new UtilityLayerRendererProperties(properties);

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ParentScene.GetCode();

        if (HandleEvents.IsNullOrEmpty()) yield break;
        yield return HandleEvents.GetCode();
    }
}
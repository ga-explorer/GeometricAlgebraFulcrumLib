using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsUtilityLayerRenderer :
    GrBabylonJsObject
{
    public sealed class UtilityLayerRendererProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsBooleanValue? OnlyCheckPointerDownEvents { get; set; }

        public GrBabylonJsSceneValue? OriginalScene { get; set; }

        public GrBabylonJsSceneValue? UtilityLayerScene { get; set; }

        public GrBabylonJsBooleanValue? PickUtilitySceneFirst { get; set; }

        public GrBabylonJsBooleanValue? PickingEnabled { get; set; }

        public GrBabylonJsBooleanValue? ProcessAllEvents { get; set; }

        public GrBabylonJsBooleanValue? ShouldRender { get; set; }
        

        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return OnlyCheckPointerDownEvents.GetNameValueCodePair("onlyCheckPointerDownEvents");
            yield return OriginalScene.GetNameValueCodePair("originalScene");
            yield return UtilityLayerScene.GetNameValueCodePair("utilityLayerScene");
            yield return PickUtilitySceneFirst.GetNameValueCodePair("pickUtilitySceneFirst");
            yield return PickingEnabled.GetNameValueCodePair("pickingEnabled");
            yield return ProcessAllEvents.GetNameValueCodePair("processAllEvents");
            yield return ShouldRender.GetNameValueCodePair("shouldRender");
        }
    }

    protected override string ConstructorName
        => "new BABYLON.UtilityLayerRenderer";

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public GrBabylonJsBooleanValue HandleEvents { get; set; }
    
    public UtilityLayerRendererProperties? Properties { get; private set; }
        = new UtilityLayerRendererProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;
    
    
    public GrBabylonJsUtilityLayerRenderer(string constName) 
        : base(constName)
    {
    }

    
    public GrBabylonJsUtilityLayerRenderer SetProperties(UtilityLayerRendererProperties properties)
    {
        Properties = properties;

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ParentScene.GetCode();

        if (HandleEvents.IsNullOrEmpty()) yield break;
        yield return HandleEvents.GetCode();
    }
}
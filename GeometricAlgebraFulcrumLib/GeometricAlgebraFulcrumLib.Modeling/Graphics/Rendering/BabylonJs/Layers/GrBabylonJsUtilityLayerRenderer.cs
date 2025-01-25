using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsUtilityLayerRenderer :
    GrBabylonJsObject
{
    protected override string ConstructorName
        => "new BABYLON.UtilityLayerRenderer";

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public GrBabylonJsBooleanValue HandleEvents { get; set; }
    
    public GrBabylonJsUtilityLayerRendererProperties Properties { get; private set; }
        = new GrBabylonJsUtilityLayerRendererProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    
    
    public GrBabylonJsUtilityLayerRenderer(string constName) 
        : base(constName)
    {
    }

    
    public GrBabylonJsUtilityLayerRenderer SetProperties(GrBabylonJsUtilityLayerRendererProperties properties)
    {
        Properties = new GrBabylonJsUtilityLayerRendererProperties(properties);

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ParentScene.GetAttributeValueCode();

        if (HandleEvents.IsNullOrEmpty()) yield break;
        yield return HandleEvents.GetAttributeValueCode();
    }
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Layers;

public sealed class GrBabylonJsLayer :
    GrBabylonJsObject
{
    protected override string ConstructorName
        => "new BABYLON.Layer";

    public GrBabylonJsSceneValue ParentScene { get; set; }

    public GrBabylonJsStringValue ImgUrl { get; set; }

    public GrBabylonJsBooleanValue IsBackground { get; set; }

    public GrBabylonJsColor4Value Color { get; set; }

    public GrBabylonJsLayerProperties Properties { get; private set; }
        = new GrBabylonJsLayerProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions
        => null;

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsLayer(string constName)
        : base(constName)
    {
    }


    public GrBabylonJsLayer SetProperties(GrBabylonJsLayerProperties properties)
    {
        Properties = new GrBabylonJsLayerProperties(properties);

        return this;
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName;
        yield return ImgUrl.GetAttributeValueCode();
        yield return ParentScene.GetAttributeValueCode();

        if (IsBackground.IsNullOrEmpty()) yield break;
        yield return IsBackground.GetAttributeValueCode();

        if (Color.IsNullOrEmpty()) yield break;
        yield return Color.GetAttributeValueCode();
    }
}
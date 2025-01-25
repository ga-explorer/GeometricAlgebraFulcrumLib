using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsTexture :
    GrBabylonJsBaseTexture
{
    protected override string ConstructorName
        => "new BABYLON.Texture";

    public GrBabylonJsStringValue Url { get; set; }
    
    public GrBabylonJsTextureOptions Options { get; private set; }
        = new GrBabylonJsTextureOptions();

    public GrBabylonJsTextureProperties Properties { get; private set; }
        = new GrBabylonJsTextureProperties();
    
    public override GrBabylonJsObjectOptions ObjectOptions
        => Options;

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsTexture SetOptions(GrBabylonJsTextureOptions options)
    {
        Options = new GrBabylonJsTextureOptions(options);

        return this;
    }

    public GrBabylonJsTexture SetProperties(GrBabylonJsTextureProperties properties)
    {
        Properties = new GrBabylonJsTextureProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Url.GetAttributeValueCode();
        
        if (ParentScene is null || ParentScene.IsEmpty) yield break;
        yield return ParentScene.Value.ConstName;

        if (Options.Count == 0) yield break;
        yield return Options.GetAttributeSetCode();
    }
}
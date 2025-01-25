using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsMarpleTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.MarpleProceduralTexture";
    
    public GrBabylonJsMarpleTextureProperties Properties { get; private set; }
        = new GrBabylonJsMarpleTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsMarpleTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsMarpleTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsMarpleTexture SetProperties(GrBabylonJsMarpleTextureProperties properties)
    {
        Properties = new GrBabylonJsMarpleTextureProperties(properties);

        return this;
    }
}
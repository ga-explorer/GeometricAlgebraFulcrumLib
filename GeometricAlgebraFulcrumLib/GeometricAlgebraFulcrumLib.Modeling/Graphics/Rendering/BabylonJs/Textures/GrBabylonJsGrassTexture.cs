using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsGrassTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.GrassProceduralTexture";
    
    public GrBabylonJsGrassTextureProperties Properties { get; private set; }
        = new GrBabylonJsGrassTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsGrassTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsGrassTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsGrassTexture SetProperties(GrBabylonJsGrassTextureProperties properties)
    {
        Properties = new GrBabylonJsGrassTextureProperties(properties);

        return this;
    }
}
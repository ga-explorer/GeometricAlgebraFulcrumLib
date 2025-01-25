using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsNoiseTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.NoiseProceduralTexture";
    
    public GrBabylonJsNoiseTextureProperties Properties { get; private set; }
        = new GrBabylonJsNoiseTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsNoiseTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsNoiseTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsNoiseTexture SetProperties(GrBabylonJsNoiseTextureProperties properties)
    {
        Properties = new GrBabylonJsNoiseTextureProperties(properties);

        return this;
    }
}
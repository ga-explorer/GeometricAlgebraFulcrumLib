using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsWoodTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.WoodProceduralTexture";
    
    public GrBabylonJsWoodTextureProperties? Properties { get; private set; }
        = new GrBabylonJsWoodTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsWoodTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsWoodTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsWoodTexture SetProperties(GrBabylonJsWoodTextureProperties properties)
    {
        Properties = new GrBabylonJsWoodTextureProperties(properties);

        return this;
    }
}
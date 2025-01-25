using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsBrickTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.BrickProceduralTexture";
    
    public GrBabylonJsBrickTextureProperties Properties { get; private set; }
        = new GrBabylonJsBrickTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsBrickTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsBrickTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsBrickTexture SetProperties(GrBabylonJsBrickTextureProperties properties)
    {
        Properties = new GrBabylonJsBrickTextureProperties(properties);

        return this;
    }
}
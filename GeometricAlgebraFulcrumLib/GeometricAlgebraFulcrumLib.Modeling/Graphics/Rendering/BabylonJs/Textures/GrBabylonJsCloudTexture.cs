using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsCloudTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.CloudProceduralTexture";
    
    public GrBabylonJsCloudTextureProperties Properties { get; private set; }
        = new GrBabylonJsCloudTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsCloudTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsCloudTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsCloudTexture SetProperties(GrBabylonJsCloudTextureProperties properties)
    {
        Properties = new GrBabylonJsCloudTextureProperties(properties);

        return this;
    }
}
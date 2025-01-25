using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsFireTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.FireProceduralTexture";
    
    public GrBabylonJsFireTextureProperties Properties { get; private set; }
        = new GrBabylonJsFireTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsFireTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsFireTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsFireTexture SetProperties(GrBabylonJsFireTextureProperties properties)
    {
        Properties = new GrBabylonJsFireTextureProperties(properties);

        return this;
    }
}
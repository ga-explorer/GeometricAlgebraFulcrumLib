using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsRoadTexture :
    GrBabylonJsProceduralTexture
{
    protected override string ConstructorName
        => "new BABYLON.RoadProceduralTexture";

    public GrBabylonJsRoadTextureProperties Properties { get; private set; }
        = new GrBabylonJsRoadTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsRoadTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsRoadTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsRoadTexture SetProperties(GrBabylonJsRoadTextureProperties properties)
    {
        Properties = new GrBabylonJsRoadTextureProperties(properties);

        return this;
    }
}
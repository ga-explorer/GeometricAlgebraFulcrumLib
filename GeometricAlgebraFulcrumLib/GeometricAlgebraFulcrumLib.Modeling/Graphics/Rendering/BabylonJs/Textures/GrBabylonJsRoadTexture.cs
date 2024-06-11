using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsRoadTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class RoadTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsColor4Value? RoadColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("roadColor");
            set => SetAttributeValue("isEnabled", value);
        }


        public RoadTextureProperties()
        {
        }

        public RoadTextureProperties(RoadTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.RoadProceduralTexture";

    public RoadTextureProperties Properties { get; private set; }
        = new RoadTextureProperties();

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

    
    public GrBabylonJsRoadTexture SetProperties(RoadTextureProperties properties)
    {
        Properties = new RoadTextureProperties(properties);

        return this;
    }
}
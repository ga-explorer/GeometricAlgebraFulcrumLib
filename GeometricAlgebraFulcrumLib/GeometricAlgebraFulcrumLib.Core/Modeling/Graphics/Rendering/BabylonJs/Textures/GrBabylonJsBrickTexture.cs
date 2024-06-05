using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsBrickTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class BrickTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsColor4Value? JointColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("jointColor");
            set => SetAttributeValue("jointColor", value);
        }

        public GrBabylonJsColor4Value? MarbleColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("marbleColor");
            set => SetAttributeValue("marbleColor", value);
        }

        public GrBabylonJsInt32Value? NumberOfTilesWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("numberOfTilesWidth");
            set => SetAttributeValue("numberOfTilesWidth", value);
        }

        public GrBabylonJsInt32Value? NumberOfTilesHeight
        {
            get => GetAttributeValueOrNull<GrBabylonJsInt32Value>("numberOfTilesHeight");
            set => SetAttributeValue("numberOfTilesHeight", value);
        }


        public BrickTextureProperties()
        {
        }

        public BrickTextureProperties(BrickTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.BrickProceduralTexture";
    
    public BrickTextureProperties Properties { get; private set; }
        = new BrickTextureProperties();

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

    
    public GrBabylonJsBrickTexture SetProperties(BrickTextureProperties properties)
    {
        Properties = new BrickTextureProperties(properties);

        return this;
    }
}
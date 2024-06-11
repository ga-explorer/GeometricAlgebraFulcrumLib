using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsWoodTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class WoodTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsColor4Value? WoodColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("woodColor");
            set => SetAttributeValue("woodColor", value);
        }

        public GrBabylonJsVector2Value? AmpScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("ampScale");
            set => SetAttributeValue("ampScale", value);
        }

            
        public WoodTextureProperties()
        {
        }
            
        public WoodTextureProperties(WoodTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.WoodProceduralTexture";
    
    public WoodTextureProperties? Properties { get; private set; }
        = new WoodTextureProperties();

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

    
    public GrBabylonJsWoodTexture SetProperties(WoodTextureProperties properties)
    {
        Properties = new WoodTextureProperties(properties);

        return this;
    }
}
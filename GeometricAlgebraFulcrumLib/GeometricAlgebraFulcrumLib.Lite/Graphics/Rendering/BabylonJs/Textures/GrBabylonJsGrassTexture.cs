using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsGrassTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class GrassTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsColor4Value? GroundColor
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("groundColor");
            set => SetAttributeValue("groundColor", value);
        }

        public GrBabylonJsColor4ArrayValue? GrassColors
        {
            get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("grassColors");
            set => SetAttributeValue("grassColors", value);
        }


        public GrassTextureProperties()
        {
        }

        public GrassTextureProperties(GrassTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GrassProceduralTexture";
    
    public GrassTextureProperties Properties { get; private set; }
        = new GrassTextureProperties();

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

    
    public GrBabylonJsGrassTexture SetProperties(GrassTextureProperties properties)
    {
        Properties = new GrassTextureProperties(properties);

        return this;
    }
}
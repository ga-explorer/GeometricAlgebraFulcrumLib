using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsCloudTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class CloudTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsColor4Value? SkyColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("skyColor");
                set => SetAttributeValue("skyColor", value);
            }

            public GrBabylonJsColor4Value? CloudColor
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor4Value>("cloudColor");
                set => SetAttributeValue("cloudColor", value);
            }


            public CloudTextureProperties()
            {
            }

            public CloudTextureProperties(CloudTextureProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName
            => "new BABYLON.CloudProceduralTexture";
    
        public CloudTextureProperties Properties { get; private set; }
            = new CloudTextureProperties();

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

    
        public GrBabylonJsCloudTexture SetProperties(CloudTextureProperties properties)
        {
            Properties = new CloudTextureProperties(properties);

            return this;
        }
    }
}
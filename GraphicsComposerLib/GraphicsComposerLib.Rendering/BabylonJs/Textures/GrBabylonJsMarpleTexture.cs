using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsMarpleTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class MarpleTextureProperties :
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


            public MarpleTextureProperties()
            {
            }

            public MarpleTextureProperties(MarpleTextureProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName
            => "new BABYLON.MarpleProceduralTexture";
    
        public MarpleTextureProperties Properties { get; private set; }
            = new MarpleTextureProperties();

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;


        public GrBabylonJsMarpleTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsMarpleTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsMarpleTexture SetProperties(MarpleTextureProperties properties)
        {
            Properties = new MarpleTextureProperties(properties);

            return this;
        }
    }
}
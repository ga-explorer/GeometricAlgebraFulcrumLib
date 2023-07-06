using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsFireTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class FireTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsFloat32Value? Time
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("time");
                set => SetAttributeValue("time", value);
            }

            public GrBabylonJsVector2Value? Speed
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("speed");
                set => SetAttributeValue("speed", value);
            }

            public GrBabylonJsVector2Value? Shift
            {
                get => GetAttributeValueOrNull<GrBabylonJsVector2Value>("shift");
                set => SetAttributeValue("shift", value);
            }

            public GrBabylonJsColor4ArrayValue? FireColors
            {
                get => GetAttributeValueOrNull<GrBabylonJsColor4ArrayValue>("fireColors");
                set => SetAttributeValue("fireColors", value);
            }


            public FireTextureProperties()
            {
            }

            public FireTextureProperties(FireTextureProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName
            => "new BABYLON.FireProceduralTexture";
    
        public FireTextureProperties Properties { get; private set; }
            = new FireTextureProperties();

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

    
        public GrBabylonJsFireTexture SetProperties(FireTextureProperties properties)
        {
            Properties = new FireTextureProperties(properties);

            return this;
        }
    }
}
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsNoiseTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class NoiseTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsFloat32Value? AnimationSpeedFactor
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("animationSpeedFactor");
                set => SetAttributeValue("animationSpeedFactor", value);
            }

            public GrBabylonJsFloat32Value? Brightness
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("brightness");
                set => SetAttributeValue("brightness", value);
            }

            public GrBabylonJsFloat32Value? Octaves
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("octaves");
                set => SetAttributeValue("octaves", value);
            }

            public GrBabylonJsFloat32Value? Persistence
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("persistence");
                set => SetAttributeValue("persistence", value);
            }

            public GrBabylonJsFloat32Value? Time
            {
                get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("time");
                set => SetAttributeValue("time", value);
            }


            public NoiseTextureProperties()
            {
            }

            public NoiseTextureProperties(NoiseTextureProperties properties)
            {
                SetAttributeValues(properties);
            }
        }


        protected override string ConstructorName
            => "new BABYLON.NoiseProceduralTexture";
    
        public NoiseTextureProperties Properties { get; private set; }
            = new NoiseTextureProperties();

        public override GrBabylonJsObjectProperties ObjectProperties 
            => Properties;


        public GrBabylonJsNoiseTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsNoiseTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsNoiseTexture SetProperties(NoiseTextureProperties properties)
        {
            Properties = new NoiseTextureProperties(properties);

            return this;
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsNoiseTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class NoiseTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsFloat32Value? AnimationSpeedFactor { get; set; }

            public GrBabylonJsFloat32Value? Brightness { get; set; }

            public GrBabylonJsFloat32Value? Octaves { get; set; }

            public GrBabylonJsFloat32Value? Persistence { get; set; }

            public GrBabylonJsFloat32Value? Time { get; set; }

        
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return AnimationSpeedFactor.GetNameValueCodePair("animationSpeedFactor");
                yield return Brightness.GetNameValueCodePair("brightness");
                yield return Octaves.GetNameValueCodePair("octaves");
                yield return Persistence.GetNameValueCodePair("persistence");
                yield return Time.GetNameValueCodePair("time");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.NoiseProceduralTexture";
    
        public NoiseTextureProperties? Properties { get; private set; }
            = new NoiseTextureProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsNoiseTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsNoiseTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsNoiseTexture SetProperties([NotNull] NoiseTextureProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public abstract class GrBabylonJsProceduralTexture : 
        GrBabylonJsBaseTexture
    {
        public abstract class ProceduralTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsBooleanValue? IsEnabled { get; set; }

            public GrBabylonJsBooleanValue? Animate { get; set; }

            public GrBabylonJsBooleanValue? AutoClear { get; set; }
        
            public GrBabylonJsFloat32Value? RefreshRate { get; set; }

        
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return IsEnabled.GetNameValueCodePair("isEnabled");
                yield return Animate.GetNameValueCodePair("animate");
                yield return AutoClear.GetNameValueCodePair("autoClear");
                yield return RefreshRate.GetNameValueCodePair("refreshRate");
            }
        }

    
        public GrBabylonJsSizeValue Size { get; set; }

        public GrBabylonJsTextureValue FallBackTexture { get; set; }

        public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }


        protected GrBabylonJsProceduralTexture(string constName) 
            : base(constName)
        {
        }

        protected GrBabylonJsProceduralTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }


        protected override IEnumerable<string> GetConstructorArguments()
        {
            yield return ConstName.DoubleQuote();
            yield return Size.GetCode();

            if (ParentScene.IsNullOrEmpty()) yield break;
            yield return ParentScene.Value.ConstName;

            if (FallBackTexture.IsNullOrEmpty()) yield break;
            yield return FallBackTexture.GetCode();

            if (GenerateMipMaps.IsNullOrEmpty()) yield break;
            yield return GenerateMipMaps.GetCode();
        }
    }
}
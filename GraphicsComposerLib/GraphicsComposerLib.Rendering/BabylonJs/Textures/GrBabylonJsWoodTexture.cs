using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsWoodTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class WoodTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsColor4Value? WoodColor { get; set; }

            public GrBabylonJsVector2Value? AmpScale { get; set; }

        
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return WoodColor.GetNameValueCodePair("woodColor");
                yield return AmpScale.GetNameValueCodePair("ampScale");
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

    
        public GrBabylonJsWoodTexture SetProperties([NotNull] WoodTextureProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}
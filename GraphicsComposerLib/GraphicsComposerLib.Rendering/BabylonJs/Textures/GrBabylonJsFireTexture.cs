using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsFireTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class FireTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsFloat32Value? Time { get; set; }

            public GrBabylonJsVector2Value? Speed { get; set; }

            public GrBabylonJsVector2Value? Shift { get; set; }

            public GrBabylonJsColor4ArrayValue? FireColors { get; set; }

        
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return Time.GetNameValueCodePair("time");
                yield return Speed.GetNameValueCodePair("speed");
                yield return Shift.GetNameValueCodePair("shift");
                yield return FireColors.GetNameValueCodePair("fireColors");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.FireProceduralTexture";
    
        public FireTextureProperties? Properties { get; private set; }
            = new FireTextureProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsFireTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsFireTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsFireTexture SetProperties([NotNull] FireTextureProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}
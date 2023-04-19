using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsRoadTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class RoadTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsColor4Value? RoadColor { get; set; }

        
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return RoadColor.GetNameValueCodePair("cloudColor");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.RoadProceduralTexture";

        public RoadTextureProperties? Properties { get; private set; }
            = new RoadTextureProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsRoadTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsRoadTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsRoadTexture SetProperties([NotNull] RoadTextureProperties? properties)
        {
            Properties = properties;

            return this;
        }
    }
}
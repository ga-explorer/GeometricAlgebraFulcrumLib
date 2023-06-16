using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures
{
    public sealed class GrBabylonJsBrickTexture :
        GrBabylonJsProceduralTexture
    {
        public sealed class BrickTextureProperties :
            BaseTextureProperties
        {
            public GrBabylonJsColor4Value? JointColor { get; set; }

            public GrBabylonJsColor4Value? MarbleColor { get; set; }

            public GrBabylonJsInt32Value? NumberOfTilesWidth { get; set; }

            public GrBabylonJsInt32Value? NumberOfTilesHeight { get; set; }

        
            protected override IEnumerable<Pair<string>?> GetNameValuePairs()
            {
                foreach (var pair in base.GetNameValuePairs())
                    yield return pair;

                yield return JointColor.GetNameValueCodePair("jointColor");
                yield return MarbleColor.GetNameValueCodePair("marbleColor");
                yield return NumberOfTilesWidth.GetNameValueCodePair("numberOfTilesWidth");
                yield return NumberOfTilesHeight.GetNameValueCodePair("numberOfTilesHeight");
            }
        }


        protected override string ConstructorName
            => "new BABYLON.BrickProceduralTexture";
    
        public BrickTextureProperties? Properties { get; private set; }
            = new BrickTextureProperties();

        public override GrBabylonJsObjectProperties? ObjectProperties 
            => Properties;


        public GrBabylonJsBrickTexture(string constName) 
            : base(constName)
        {
        }

        public GrBabylonJsBrickTexture(string constName, GrBabylonJsSceneValue scene) 
            : base(constName, scene)
        {
        }

    
        public GrBabylonJsBrickTexture SetProperties(BrickTextureProperties properties)
        {
            Properties = properties;

            return this;
        }
    }
}
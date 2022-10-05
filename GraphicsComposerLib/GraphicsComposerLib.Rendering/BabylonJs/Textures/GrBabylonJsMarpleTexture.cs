using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsMarpleTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class MarpleTextureProperties :
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
        => "new BABYLON.MarpleProceduralTexture";
    
    public MarpleTextureProperties? Properties { get; private set; }
        = new MarpleTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsMarpleTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsMarpleTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsMarpleTexture SetProperties([NotNull] MarpleTextureProperties? properties)
    {
        Properties = properties;

        return this;
    }
}
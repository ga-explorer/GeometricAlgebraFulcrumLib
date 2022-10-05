using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsCloudTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class CloudTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsColor4Value? SkyColor { get; set; }

        public GrBabylonJsColor4Value? CloudColor { get; set; }

        
        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return SkyColor.GetNameValueCodePair("skyColor");
            yield return CloudColor.GetNameValueCodePair("cloudColor");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.CloudProceduralTexture";
    
    public CloudTextureProperties? Properties { get; private set; }
        = new CloudTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsCloudTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsCloudTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsCloudTexture SetProperties([NotNull] CloudTextureProperties? properties)
    {
        Properties = properties;

        return this;
    }
}
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsGrassTexture :
    GrBabylonJsProceduralTexture
{
    public sealed class GrassTextureProperties :
        BaseTextureProperties
    {
        public GrBabylonJsColor4Value? GroundColor { get; set; }

        public GrBabylonJsColor4ArrayValue? GrassColors { get; set; }

        
        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return GroundColor.GetNameValueCodePair("groundColor");
            yield return GrassColors.GetNameValueCodePair("grassColors");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GrassProceduralTexture";
    
    public GrassTextureProperties? Properties { get; private set; }
        = new GrassTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsGrassTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsGrassTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsGrassTexture SetProperties([NotNull] GrassTextureProperties? properties)
    {
        Properties = properties;

        return this;
    }
}
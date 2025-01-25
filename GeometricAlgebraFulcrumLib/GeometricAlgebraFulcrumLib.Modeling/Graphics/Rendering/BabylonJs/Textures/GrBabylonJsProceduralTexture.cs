using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public abstract class GrBabylonJsProceduralTexture : 
    GrBabylonJsBaseTexture
{
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
        yield return Size.GetAttributeValueCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene!.Value.ConstName;

        if (FallBackTexture.IsNullOrEmpty()) yield break;
        yield return FallBackTexture.GetAttributeValueCode();

        if (GenerateMipMaps.IsNullOrEmpty()) yield break;
        yield return GenerateMipMaps.GetAttributeValueCode();
    }
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsDynamicTexture :
    GrBabylonJsBaseTexture
{
    protected override string ConstructorName
        => "new BABYLON.DynamicTexture";

    public GrBabylonJsSizeValue Size { get; set; }
    
    public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }

    public GrBabylonJsTextureSamplingModeValue SamplingMode { get; set; }

    public GrBabylonJsTextureFormatValue Format { get; set; }

    public GrBabylonJsBooleanValue InvertY { get; set; }

    public GrBabylonJsDynamicTextureProperties? Properties { get; private set; }
        = new GrBabylonJsDynamicTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsDynamicTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsDynamicTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsDynamicTexture SetProperties(GrBabylonJsDynamicTextureProperties properties)
    {
        Properties = properties;

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return Size.GetAttributeValueCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;

        if (GenerateMipMaps.IsNullOrEmpty()) yield break;
        yield return GenerateMipMaps.GetAttributeValueCode();
        
        if (SamplingMode.IsNullOrEmpty()) yield break;
        yield return SamplingMode.GetAttributeValueCode();
        
        if (Format.IsNullOrEmpty()) yield break;
        yield return Format.GetAttributeValueCode();

        if (InvertY.IsNullOrEmpty()) yield break;
        yield return InvertY.GetAttributeValueCode();
    }
}
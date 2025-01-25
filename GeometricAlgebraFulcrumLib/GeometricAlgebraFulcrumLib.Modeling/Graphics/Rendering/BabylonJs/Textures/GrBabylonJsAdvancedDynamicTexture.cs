using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public class GrBabylonJsAdvancedDynamicTexture :
    GrBabylonJsBaseTexture
{
    protected override string ConstructorName
        => "new BABYLON.GUI.AdvancedDynamicTexture";

    public GrBabylonJsInt32Value Width { get; set; }

    public GrBabylonJsInt32Value Height { get; set; }

    public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }

    public GrBabylonJsTextureSamplingModeValue SamplingMode { get; set; }

    public GrBabylonJsBooleanValue InvertY { get; set; }

    public GrBabylonJsAdvancedDynamicTextureProperties Properties { get; private set; }
        = new GrBabylonJsAdvancedDynamicTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;


    public GrBabylonJsAdvancedDynamicTexture(string constName)
        : base(constName)
    {
    }

    public GrBabylonJsAdvancedDynamicTexture(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }


    public GrBabylonJsAdvancedDynamicTexture SetProperties(GrBabylonJsAdvancedDynamicTextureProperties properties)
    {
        Properties = new GrBabylonJsAdvancedDynamicTextureProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (Width.IsNullOrEmpty()) yield break;
        yield return Width.GetAttributeValueCode();

        if (Height.IsNullOrEmpty()) yield break;
        yield return Height.GetAttributeValueCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;

        if (GenerateMipMaps.IsNullOrEmpty()) yield break;
        yield return GenerateMipMaps.GetAttributeValueCode();

        if (SamplingMode.IsNullOrEmpty()) yield break;
        yield return SamplingMode.GetAttributeValueCode();

        if (InvertY.IsNullOrEmpty()) yield break;
        yield return InvertY.GetAttributeValueCode();
    }
}
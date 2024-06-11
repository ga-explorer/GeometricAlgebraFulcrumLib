using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public class GrBabylonJsAdvancedDynamicTexture :
    GrBabylonJsBaseTexture
{
    public sealed class AdvancedDynamicTextureProperties :
        GrBabylonJsDynamicTexture.DynamicTextureProperties
    {
        public GrBabylonJsBooleanValue? ApplyYInversionOnUpdate
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("applyYInversionOnUpdate");
            set => SetAttributeValue("applyYInversionOnUpdate", value);
        }

        public GrBabylonJsBooleanValue? CheckPointerEveryFrame
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("checkPointerEveryFrame");
            set => SetAttributeValue("checkPointerEveryFrame", value);
        }

        public GrBabylonJsBooleanValue? PremulAlpha
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("premulAlpha");
            set => SetAttributeValue("premulAlpha", value);
        }

        public GrBabylonJsBooleanValue? AllowGpuOptimizations
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("allowGpuOptimizations");
            set => SetAttributeValue("allowGpuOptimizations", value);
        }

        public GrBabylonJsBooleanValue? IsForeground
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isForeground");
            set => SetAttributeValue("isForeground", value);
        }

        public GrBabylonJsBooleanValue? RenderAtIdealSize
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("renderAtIdealSize");
            set => SetAttributeValue("renderAtIdealSize", value);
        }

        public GrBabylonJsBooleanValue? UseSmallestIdeal
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useSmallestIdeal");
            set => SetAttributeValue("useSmallestIdeal", value);
        }

        public GrBabylonJsBooleanValue? UseInvalidateRectOptimization
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useInvalidateRectOptimization");
            set => SetAttributeValue("useInvalidateRectOptimization", value);
        }

        public GrBabylonJsStringValue? SnippetUrl
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("snippetUrl");
            set => SetAttributeValue("snippetUrl", value);
        }

        public GrBabylonJsStringValue? Background
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("background");
            set => SetAttributeValue("background", value);
        }

        public GrBabylonJsStringValue? ClipboardData
        {
            get => GetAttributeValueOrNull<GrBabylonJsStringValue>("clipboardData");
            set => SetAttributeValue("clipboardData", value);
        }

        public GrBabylonJsFloat32Value? IdealWidth
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("idealWidth");
            set => SetAttributeValue("idealWidth", value);
        }

        public GrBabylonJsFloat32Value? IdealHeight
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("idealHeight");
            set => SetAttributeValue("idealHeight", value);
        }

        public GrBabylonJsFloat32Value? IdealRatio
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("idealRatio");
            set => SetAttributeValue("idealRatio", value);
        }

        public GrBabylonJsFloat32Value? RenderScale
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("renderScale");
            set => SetAttributeValue("renderScale", value);
        }

        public AdvancedDynamicTextureProperties()
        {
        }

        public AdvancedDynamicTextureProperties(AdvancedDynamicTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GUI.AdvancedDynamicTexture";

    public GrBabylonJsInt32Value Width { get; set; }

    public GrBabylonJsInt32Value Height { get; set; }

    public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }

    public GrBabylonJsTextureSamplingModeValue SamplingMode { get; set; }

    public GrBabylonJsBooleanValue InvertY { get; set; }

    public AdvancedDynamicTextureProperties Properties { get; private set; }
        = new AdvancedDynamicTextureProperties();

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


    public GrBabylonJsAdvancedDynamicTexture SetProperties(AdvancedDynamicTextureProperties properties)
    {
        Properties = new AdvancedDynamicTextureProperties(properties);

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (Width.IsNullOrEmpty()) yield break;
        yield return Width.GetCode();

        if (Height.IsNullOrEmpty()) yield break;
        yield return Height.GetCode();

        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;

        if (GenerateMipMaps.IsNullOrEmpty()) yield break;
        yield return GenerateMipMaps.GetCode();

        if (SamplingMode.IsNullOrEmpty()) yield break;
        yield return SamplingMode.GetCode();

        if (InvertY.IsNullOrEmpty()) yield break;
        yield return InvertY.GetCode();
    }
}
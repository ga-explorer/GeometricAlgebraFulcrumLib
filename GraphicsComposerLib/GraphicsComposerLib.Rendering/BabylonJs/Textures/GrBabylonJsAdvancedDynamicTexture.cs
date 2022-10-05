using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures;

public class GrBabylonJsAdvancedDynamicTexture :
    GrBabylonJsBaseTexture
{
    public sealed class AdvancedDynamicTextureProperties :
        GrBabylonJsDynamicTexture.DynamicTextureProperties
    {
        public GrBabylonJsBooleanValue? ApplyYInversionOnUpdate { get; set; }

        public GrBabylonJsBooleanValue? CheckPointerEveryFrame { get; set; }

        public GrBabylonJsBooleanValue? PremulAlpha { get; set; }

        public GrBabylonJsBooleanValue? AllowGpuOptimizations { get; set; }
        
        public GrBabylonJsBooleanValue? IsForeground { get; set; }

        public GrBabylonJsBooleanValue? RenderAtIdealSize { get; set; }

        public GrBabylonJsBooleanValue? UseSmallestIdeal { get; set; }

        public GrBabylonJsBooleanValue? UseInvalidateRectOptimization { get; set; }

        public GrBabylonJsStringValue? SnippetUrl { get; set; }

        public GrBabylonJsStringValue? Background { get; set; }
        
        public GrBabylonJsStringValue? ClipboardData { get; set; }

        public GrBabylonJsFloat32Value? IdealWidth { get; set; }

        public GrBabylonJsFloat32Value? IdealHeight { get; set; }

        public GrBabylonJsFloat32Value? IdealRatio { get; set; }
        
        public GrBabylonJsFloat32Value? RenderScale { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            foreach (var pair in base.GetNameValuePairs())
                yield return pair;

            yield return ApplyYInversionOnUpdate.GetNameValueCodePair("applyYInversionOnUpdate");
            yield return CheckPointerEveryFrame.GetNameValueCodePair("checkPointerEveryFrame");
            yield return PremulAlpha.GetNameValueCodePair("premulAlpha");
            yield return AllowGpuOptimizations.GetNameValueCodePair("AllowGPUOptimizations");
            yield return IsForeground.GetNameValueCodePair("isForeground");
            yield return RenderAtIdealSize.GetNameValueCodePair("renderAtIdealSize");
            yield return UseSmallestIdeal.GetNameValueCodePair("useSmallestIdeal");
            yield return UseInvalidateRectOptimization.GetNameValueCodePair("useInvalidateRectOptimization");
            yield return SnippetUrl.GetNameValueCodePair("SnippetUrl");
            yield return Background.GetNameValueCodePair("background");
            yield return ClipboardData.GetNameValueCodePair("clipboardData");
            yield return IdealWidth.GetNameValueCodePair("idealWidth");
            yield return IdealHeight.GetNameValueCodePair("idealHeight");
            yield return IdealRatio.GetNameValueCodePair("idealRatio");
            yield return RenderScale.GetNameValueCodePair("renderScale");
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GUI.AdvancedDynamicTexture";

    public GrBabylonJsInt32Value Width { get; set; }

    public GrBabylonJsInt32Value Height { get; set; }
    
    public GrBabylonJsBooleanValue GenerateMipMaps { get; set; }

    public GrBabylonJsTextureSamplingModeValue SamplingMode { get; set; }
    
    public GrBabylonJsBooleanValue InvertY { get; set; }

    public AdvancedDynamicTextureProperties? Properties { get; private set; }
        = new AdvancedDynamicTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsAdvancedDynamicTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsAdvancedDynamicTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsAdvancedDynamicTexture SetProperties([NotNull] AdvancedDynamicTextureProperties? properties)
    {
        Properties = properties;

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
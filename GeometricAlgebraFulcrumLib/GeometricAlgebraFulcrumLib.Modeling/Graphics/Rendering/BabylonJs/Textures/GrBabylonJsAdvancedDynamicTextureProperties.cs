using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsAdvancedDynamicTextureProperties :
    GrBabylonJsDynamicTextureProperties
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

    public GrBabylonJsAdvancedDynamicTextureProperties()
    {
    }

    public GrBabylonJsAdvancedDynamicTextureProperties(GrBabylonJsAdvancedDynamicTextureProperties properties)
    {
        SetAttributeValues(properties);
    }
}
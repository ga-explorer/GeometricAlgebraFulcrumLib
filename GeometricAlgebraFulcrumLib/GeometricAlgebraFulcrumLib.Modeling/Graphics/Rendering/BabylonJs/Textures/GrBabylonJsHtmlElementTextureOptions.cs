using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsHtmlElementTextureOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsSceneValue? Scene
    {
        get => GetAttributeValueOrNull<GrBabylonJsSceneValue>("scene");
        set => SetAttributeValue("scene", value);
    }
        
    public GrBabylonJsBooleanValue? GenerateMipMaps
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("generateMipMaps");
        set => SetAttributeValue("generateMipMaps", value);
    }

    public GrBabylonJsTextureSamplingModeValue? SamplingMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureSamplingModeValue>("samplingMode");
        set => SetAttributeValue("samplingMode", value);
    }

    public GrBabylonJsTextureFormatValue? Format
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureFormatValue>("format");
        set => SetAttributeValue("format", value);
    }


    public GrBabylonJsHtmlElementTextureOptions()
    {
    }

    public GrBabylonJsHtmlElementTextureOptions(GrBabylonJsHtmlElementTextureOptions options)
    {
        SetAttributeValues(options);
    }
}
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsTextureOptions :
    GrBabylonJsObjectOptions
{
    public GrBabylonJsCodeValue? Buffer
    {
        get => GetAttributeValueOrNull<GrBabylonJsCodeValue>("buffer");
        set => SetAttributeValue("buffer", value);
    }

    public GrBabylonJsBooleanValue? DeleteBuffer
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("deleteBuffer");
        set => SetAttributeValue("deleteBuffer", value);
    }

    public GrBabylonJsBooleanValue? UseSrgbBuffer
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("useSrgbBuffer");
        set => SetAttributeValue("useSrgbBuffer", value);
    }

    public GrBabylonJsTextureFormatValue? Format
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureFormatValue>("format");
        set => SetAttributeValue("format", value);
    }

    public GrBabylonJsBooleanValue? InvertY
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("invertY");
        set => SetAttributeValue("invertY", value);
    }

    public GrBabylonJsStringValue? MimeType
    {
        get => GetAttributeValueOrNull<GrBabylonJsStringValue>("mimeType");
        set => SetAttributeValue("mimeType", value);
    }

    public GrBabylonJsBooleanValue? NoMipmap
    {
        get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("noMipmap");
        set => SetAttributeValue("noMipmap", value);
    }

    public GrBabylonJsTextureSamplingModeValue? SamplingMode
    {
        get => GetAttributeValueOrNull<GrBabylonJsTextureSamplingModeValue>("samplingMode");
        set => SetAttributeValue("samplingMode", value);
    }


    public GrBabylonJsTextureOptions()
    {
    }

    public GrBabylonJsTextureOptions(GrBabylonJsTextureOptions options)
    {
        SetAttributeValues(options);
    }
}
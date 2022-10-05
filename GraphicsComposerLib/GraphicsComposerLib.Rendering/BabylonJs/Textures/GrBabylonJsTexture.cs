using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsTexture :
    GrBabylonJsBaseTexture
{
    public sealed class TextureOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsCodeValue? Buffer { get; set; }

        public GrBabylonJsBooleanValue? DeleteBuffer { get; set; }

        public GrBabylonJsBooleanValue? UseSrgbBuffer { get; set; }

        public GrBabylonJsTextureFormatValue? Format { get; set; }

        public GrBabylonJsBooleanValue? InvertY { get; set; }

        public GrBabylonJsStringValue? MimeType { get; set; }

        public GrBabylonJsBooleanValue? NoMipmap { get; set; }

        public GrBabylonJsTextureSamplingModeValue? SamplingMode { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Buffer.GetNameValueCodePair("buffer");
            yield return DeleteBuffer.GetNameValueCodePair("deleteBuffer");
            yield return UseSrgbBuffer.GetNameValueCodePair("useSRGBBuffer");
            yield return Format.GetNameValueCodePair("format");
            yield return InvertY.GetNameValueCodePair("invertY");
            yield return MimeType.GetNameValueCodePair("mimeType");
            yield return NoMipmap.GetNameValueCodePair("noMipmap");
            yield return SamplingMode.GetNameValueCodePair("samplingMode");
        }
    }

    public sealed class TextureProperties :
        BaseTextureProperties
    {
        
    }


    protected override string ConstructorName
        => "new BABYLON.Texture";

    public GrBabylonJsStringValue Url { get; set; }
    
    public TextureOptions? Options { get; private set; }
        = new TextureOptions();

    public TextureProperties? Properties { get; private set; }
        = new TextureProperties();
    
    public override GrBabylonJsObjectOptions? ObjectOptions
        => Options;

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;


    public GrBabylonJsTexture(string constName) 
        : base(constName)
    {
    }

    public GrBabylonJsTexture(string constName, GrBabylonJsSceneValue scene) 
        : base(constName, scene)
    {
    }

    
    public GrBabylonJsTexture SetOptions([NotNull] TextureOptions? options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsTexture SetProperties([NotNull] TextureProperties? properties)
    {
        Properties = properties;

        return this;
    }

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Url.GetCode();
        
        if (ParentScene.IsNullOrEmpty()) yield break;
        yield return ParentScene.Value.ConstName;

        if (Options is null) yield break;
        yield return Options.GetCode();
    }
}
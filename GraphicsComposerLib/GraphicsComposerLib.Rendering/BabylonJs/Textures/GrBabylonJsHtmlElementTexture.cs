using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;
using TextComposerLib;

namespace GraphicsComposerLib.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsHtmlElementTexture :
    GrBabylonJsBaseTexture
{
    public sealed class HtmlElementTextureOptions :
        GrBabylonJsObjectOptions
    {
        public GrBabylonJsSceneValue? Scene { get; internal set; }
        
        public GrBabylonJsBooleanValue? GenerateMipMaps { get; set; }

        public GrBabylonJsTextureSamplingModeValue? SamplingMode { get; set; }

        public GrBabylonJsTextureFormatValue? Format { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return Scene.GetNameValueCodePair("scene");
            yield return GenerateMipMaps.GetNameValueCodePair("generateMipMaps");
            yield return SamplingMode.GetNameValueCodePair("samplingMode");
            yield return Format.GetNameValueCodePair("format");
        }
    }


    public class HtmlElementTextureProperties :
        BaseTextureProperties
    {
        
    }

    protected override string ConstructorName
        => "new BABYLON.HtmlElementTexture";

    public GrBabylonJsCodeValue Element { get; }
    
    public HtmlElementTextureOptions Options { get; private set; }
        = new HtmlElementTextureOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;

    public HtmlElementTextureProperties? Properties { get; private set; }
        = new HtmlElementTextureProperties();

    public override GrBabylonJsObjectProperties? ObjectProperties 
        => Properties;

    
    public GrBabylonJsHtmlElementTexture(string constName, GrBabylonJsSceneValue scene, GrBabylonJsCodeValue element) 
        : base(constName, scene)
    {
        Element = element;
    }

    
    public GrBabylonJsHtmlElementTexture SetOptions(HtmlElementTextureOptions options)
    {
        Options = options;

        return this;
    }

    public GrBabylonJsHtmlElementTexture SetProperties(HtmlElementTextureProperties properties)
    {
        Properties = properties;

        return this;
    }
    
    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return Element.GetCode();

        Options.Scene = ParentScene;

        var optionsCode = 
            ObjectOptions.GetCode();

        yield return optionsCode;
    }

}
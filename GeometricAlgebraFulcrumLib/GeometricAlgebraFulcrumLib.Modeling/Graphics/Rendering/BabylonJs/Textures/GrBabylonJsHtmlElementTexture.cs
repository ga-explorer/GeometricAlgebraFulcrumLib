using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsHtmlElementTexture :
    GrBabylonJsBaseTexture
{
    public sealed class HtmlElementTextureOptions :
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


        public HtmlElementTextureOptions()
        {
        }

        public HtmlElementTextureOptions(HtmlElementTextureOptions options)
        {
            SetAttributeValues(options);
        }
    }


    public class HtmlElementTextureProperties :
        BaseTextureProperties
    {
        public HtmlElementTextureProperties()
        {
        }

        public HtmlElementTextureProperties(HtmlElementTextureProperties properties)
        {
            SetAttributeValues(properties);
        }
    }

    protected override string ConstructorName
        => "new BABYLON.HtmlElementTexture";

    public GrBabylonJsCodeValue Element { get; }
    
    public HtmlElementTextureOptions Options { get; private set; }
        = new HtmlElementTextureOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;

    public HtmlElementTextureProperties Properties { get; private set; }
        = new HtmlElementTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;

    
    public GrBabylonJsHtmlElementTexture(string constName, GrBabylonJsSceneValue scene, GrBabylonJsCodeValue element) 
        : base(constName, scene)
    {
        Element = element;
    }

    
    public GrBabylonJsHtmlElementTexture SetOptions(HtmlElementTextureOptions options)
    {
        Options = new HtmlElementTextureOptions(options);

        return this;
    }

    public GrBabylonJsHtmlElementTexture SetProperties(HtmlElementTextureProperties properties)
    {
        Properties = new HtmlElementTextureProperties(properties);

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
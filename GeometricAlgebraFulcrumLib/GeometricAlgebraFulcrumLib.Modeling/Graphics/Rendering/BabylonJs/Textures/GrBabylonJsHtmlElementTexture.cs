using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Textures;

public sealed class GrBabylonJsHtmlElementTexture :
    GrBabylonJsBaseTexture
{
    protected override string ConstructorName
        => "new BABYLON.HtmlElementTexture";

    public GrBabylonJsCodeValue Element { get; }
    
    public GrBabylonJsHtmlElementTextureOptions Options { get; private set; }
        = new GrBabylonJsHtmlElementTextureOptions();

    public override GrBabylonJsObjectOptions ObjectOptions 
        => Options;

    public GrBabylonJsHtmlElementTextureProperties Properties { get; private set; }
        = new GrBabylonJsHtmlElementTextureProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;

    
    public GrBabylonJsHtmlElementTexture(string constName, GrBabylonJsSceneValue scene, GrBabylonJsCodeValue element) 
        : base(constName, scene)
    {
        Element = element;
    }

    
    public GrBabylonJsHtmlElementTexture SetOptions(GrBabylonJsHtmlElementTextureOptions options)
    {
        Options = new GrBabylonJsHtmlElementTextureOptions(options);

        return this;
    }

    public GrBabylonJsHtmlElementTexture SetProperties(GrBabylonJsHtmlElementTextureProperties properties)
    {
        Properties = new GrBabylonJsHtmlElementTextureProperties(properties);

        return this;
    }
    
    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
        yield return Element.GetAttributeValueCode();

        Options.Scene = ParentScene;

        var optionsCode = 
            ObjectOptions.GetAttributeSetCode();

        yield return optionsCode;
    }

}
using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Style
/// </summary>
public class GrBabylonJsGuiStyle :
    GrBabylonJsObject
{
    public sealed class GuiStyleProperties :
        GrBabylonJsObjectProperties
    {
        public GrBabylonJsStringValue? FontFamily { get; set; }

        public GrBabylonJsFloat32Value? FontSize { get; set; }

        public GrBabylonJsStringValue? FontStyle { get; set; }

        public GrBabylonJsStringValue? FontWeight { get; set; }


        protected override IEnumerable<Pair<string>?> GetNameValuePairs()
        {
            yield return FontFamily.GetNameValueCodePair("fontWeight");
            yield return FontSize.GetNameValueCodePair("fontSize");
            yield return FontStyle.GetNameValueCodePair("fontStyle");
            yield return FontWeight.GetNameValueCodePair("fontWeight");
        }
    }

    protected override string ConstructorName 
        => "new BABYLON.GUI.Style";

    public GrBabylonJsAdvancedDynamicTextureValue Host { get; set; }

    public GuiStyleProperties Properties { get; private set; }
        = new GuiStyleProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties? ObjectProperties { get; }


    public GrBabylonJsGuiStyle(string constName) 
        : base(constName)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Host.ToString();
    }

    public GrBabylonJsGuiStyle SetProperties(GuiStyleProperties properties)
    {
        Properties = properties;

        return this;
    }
}
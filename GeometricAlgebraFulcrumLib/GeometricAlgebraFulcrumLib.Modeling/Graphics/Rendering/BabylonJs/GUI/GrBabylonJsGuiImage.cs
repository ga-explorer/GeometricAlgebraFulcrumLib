using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Image
/// </summary>
public sealed class GrBabylonJsGuiImage :
    GrBabylonJsGuiControl
{
    protected override string ConstructorName
        => "new BABYLON.GUI.Image";

    public GrBabylonJsGuiImageProperties Properties { get; private set; }
        = new GrBabylonJsGuiImageProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsStringValue Url { get; set; }


    public GrBabylonJsGuiImage(string constName, IGrBabylonJsGuiControlContainer parentContainer)
        : base(constName, parentContainer)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (Url.IsNullOrEmpty()) yield break;
        yield return Url.GetAttributeValueCode();
    }

    public GrBabylonJsGuiImage SetProperties(GrBabylonJsGuiImageProperties properties)
    {
        Properties = new GrBabylonJsGuiImageProperties(properties);

        return this;
    }
}
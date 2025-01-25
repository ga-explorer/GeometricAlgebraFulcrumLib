using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.TextBlock
/// </summary>
public sealed class GrBabylonJsGuiTextBlock :
    GrBabylonJsGuiControl
{
    protected override string ConstructorName
        => "new BABYLON.GUI.TextBlock";

    public GrBabylonJsGuiTextBlockProperties Properties { get; private set; }
        = new GrBabylonJsGuiTextBlockProperties();

    public override GrBabylonJsObjectProperties ObjectProperties
        => Properties;

    public GrBabylonJsStringValue? Text { get; set; }


    public GrBabylonJsGuiTextBlock(string constName, IGrBabylonJsGuiControlContainer parentContainer)
        : base(constName, parentContainer)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();

        if (Text is null || Text.IsEmpty) yield break;
        yield return Text.GetAttributeValueCode();
    }

    public GrBabylonJsGuiTextBlock SetProperties(GrBabylonJsGuiTextBlockProperties properties)
    {
        Properties = new GrBabylonJsGuiTextBlockProperties(properties);

        return this;
    }
}
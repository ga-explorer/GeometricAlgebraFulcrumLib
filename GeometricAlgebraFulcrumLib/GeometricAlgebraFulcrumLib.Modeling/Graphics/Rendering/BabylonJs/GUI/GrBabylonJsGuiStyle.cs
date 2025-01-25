using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Style
/// </summary>
public class GrBabylonJsGuiStyle :
    GrBabylonJsObject
{
    protected override string ConstructorName 
        => "new BABYLON.GUI.Style";

    public GrBabylonJsAdvancedDynamicTextureValue Host { get; set; }

    public GrBabylonJsGuiStyleProperties Properties { get; private set; }
        = new GrBabylonJsGuiStyleProperties();

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsGuiStyle(string constName) 
        : base(constName)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return Host.ToString();
    }

    public GrBabylonJsGuiStyle SetProperties(GrBabylonJsGuiStyleProperties properties)
    {
        Properties = new GrBabylonJsGuiStyleProperties(properties);

        return this;
    }
}
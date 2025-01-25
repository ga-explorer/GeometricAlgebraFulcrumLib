using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Line
/// </summary>
public sealed class GrBabylonJsGuiLine :
    GrBabylonJsGuiControl
{
    protected override string ConstructorName 
        => "new BABYLON.GUI.Line";

    public GrBabylonJsGuiLineProperties Properties { get; private set; }
        = new GrBabylonJsGuiLineProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    

    public GrBabylonJsGuiLine(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
        : base(constName, parentContainer)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
    }
    
    public GrBabylonJsGuiLine SetProperties(GrBabylonJsGuiLineProperties properties)
    {
        Properties = new GrBabylonJsGuiLineProperties(properties);

        return this;
    }
}
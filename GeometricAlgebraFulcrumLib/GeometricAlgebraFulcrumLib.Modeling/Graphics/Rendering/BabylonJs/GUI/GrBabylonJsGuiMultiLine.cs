using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.MultiLine
/// </summary>
public sealed class GrBabylonJsGuiMultiLine :
    GrBabylonJsGuiControl
{
    protected override string ConstructorName 
        => "new BABYLON.GUI.MultiLine";

    public GrBabylonJsGuiMultiLineProperties Properties { get; private set; }
        = new GrBabylonJsGuiMultiLineProperties();

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;
    

    public GrBabylonJsGuiMultiLine(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
        : base(constName, parentContainer)
    {
    }


    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
    }
    
    public GrBabylonJsGuiMultiLine SetProperties(GrBabylonJsGuiMultiLineProperties properties)
    {
        Properties = new GrBabylonJsGuiMultiLineProperties(properties);

        return this;
    }
}
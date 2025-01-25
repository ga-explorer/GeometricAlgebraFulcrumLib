using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.Control
/// </summary>
public abstract class GrBabylonJsGuiControl :
    GrBabylonJsObject
{
    public IGrBabylonJsGuiControlContainer ParentContainer { get; }

    public GrBabylonJsGuiFullScreenUiValue ParentUi { get; }

    public GrBabylonJsSceneValue? ParentScene 
        => ParentUi.Value.ParentScene;

    public override GrBabylonJsObjectOptions? ObjectOptions 
        => null;

    
    protected GrBabylonJsGuiControl(string constName, IGrBabylonJsGuiControlContainer parentContainer)
        : base(constName)
    {
        ParentContainer = parentContainer;
        ParentUi = parentContainer.ParentUi;
    }
    

    protected override IEnumerable<string> GetConstructorArguments()
    {
        yield return ConstName.DoubleQuote();
    }
}
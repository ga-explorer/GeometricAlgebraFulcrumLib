namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.StackPanel
/// </summary>
public sealed class GrBabylonJsGuiStackPanel : 
    GrBabylonJsGuiContainer
{
    protected override string ConstructorName
        => "new BABYLON.GUI.StackPanel";

    public GrBabylonJsGuiStackPanelProperties Properties { get; private set; }

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsGuiStackPanel(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
        : base(constName, parentContainer)
    {
    }


    public GrBabylonJsGuiStackPanel SetProperties(GrBabylonJsGuiStackPanelProperties properties)
    {
        Properties = new GrBabylonJsGuiStackPanelProperties(properties);

        return this;
    }
}
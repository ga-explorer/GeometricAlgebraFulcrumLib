using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.GUI;

/// <summary>
/// https://doc.babylonjs.com/typedoc/classes/BABYLON.GUI.StackPanel
/// </summary>
public sealed class GrBabylonJsGuiStackPanel : 
    GrBabylonJsGuiContainer
{
    public class GuiStackPanelProperties :
        GuiContainerProperties
    {
        public GrBabylonJsBooleanValue? IgnoreLayoutWarnings
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("ignoreLayoutWarnings");
            set => SetAttributeValue("ignoreLayoutWarnings", value);
        }

        public GrBabylonJsBooleanValue? IsVertical
        {
            get => GetAttributeValueOrNull<GrBabylonJsBooleanValue>("isVertical");
            set => SetAttributeValue("isVertical", value);
        }

        public GrBabylonJsFloat32Value? Spacing
        {
            get => GetAttributeValueOrNull<GrBabylonJsFloat32Value>("spacing");
            set => SetAttributeValue("spacing", value);
        }


        public GuiStackPanelProperties()
        {
        }

        public GuiStackPanelProperties(GuiStackPanelProperties properties)
        {
            SetAttributeValues(properties);
        }
    }


    protected override string ConstructorName
        => "new BABYLON.GUI.StackPanel";

    public GuiStackPanelProperties Properties { get; private set; }

    public override GrBabylonJsObjectProperties ObjectProperties 
        => Properties;


    public GrBabylonJsGuiStackPanel(string constName, IGrBabylonJsGuiControlContainer parentContainer) 
        : base(constName, parentContainer)
    {
    }


    public GrBabylonJsGuiStackPanel SetProperties(GuiStackPanelProperties properties)
    {
        Properties = new GuiStackPanelProperties(properties);

        return this;
    }
}
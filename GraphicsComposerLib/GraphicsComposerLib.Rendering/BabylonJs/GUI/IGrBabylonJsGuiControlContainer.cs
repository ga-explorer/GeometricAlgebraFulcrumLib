using GraphicsComposerLib.Rendering.BabylonJs.Values;

namespace GraphicsComposerLib.Rendering.BabylonJs.GUI
{
    public interface IGrBabylonJsGuiControlContainer
    {
        GrBabylonJsGuiFullScreenUiValue ParentUi { get; }

        GrBabylonJsGuiControlList ControlList { get; }
    }
}
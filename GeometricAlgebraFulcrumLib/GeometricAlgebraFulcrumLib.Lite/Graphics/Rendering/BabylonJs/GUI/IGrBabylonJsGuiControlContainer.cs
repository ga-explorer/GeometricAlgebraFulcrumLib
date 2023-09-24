using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.GUI
{
    public interface IGrBabylonJsGuiControlContainer
    {
        GrBabylonJsGuiFullScreenUiValue ParentUi { get; }

        GrBabylonJsGuiControlList ControlList { get; }
    }
}
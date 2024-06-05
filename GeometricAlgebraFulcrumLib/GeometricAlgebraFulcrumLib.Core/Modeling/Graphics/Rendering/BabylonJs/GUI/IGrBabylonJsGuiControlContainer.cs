using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.BabylonJs.GUI;

public interface IGrBabylonJsGuiControlContainer
{
    GrBabylonJsGuiFullScreenUiValue ParentUi { get; }

    GrBabylonJsGuiControlList ControlList { get; }
}
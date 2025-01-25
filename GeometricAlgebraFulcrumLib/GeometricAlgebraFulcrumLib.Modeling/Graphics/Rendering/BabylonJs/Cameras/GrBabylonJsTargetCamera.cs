using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;

public abstract class GrBabylonJsTargetCamera :
    GrBabylonJsCamera
{
    protected GrBabylonJsTargetCamera(string constName)
        : base(constName)
    {
    }

    protected GrBabylonJsTargetCamera(string constName, GrBabylonJsSceneValue scene)
        : base(constName, scene)
    {
    }
}
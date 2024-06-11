using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Cameras;

public abstract class XeoglCameraProjection : XeoglComponent
{
    public abstract XeoglCameraProjectionType ProjectionType { get; }
}
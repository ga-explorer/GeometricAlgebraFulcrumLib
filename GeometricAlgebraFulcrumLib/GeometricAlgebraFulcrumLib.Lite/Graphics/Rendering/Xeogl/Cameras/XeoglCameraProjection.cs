using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Constants;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Cameras
{
    public abstract class XeoglCameraProjection : XeoglComponent
    {
        public abstract XeoglCameraProjectionType ProjectionType { get; }
    }
}
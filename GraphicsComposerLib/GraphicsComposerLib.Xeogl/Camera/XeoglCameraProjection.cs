using GraphicsComposerLib.Xeogl.Constants;

namespace GraphicsComposerLib.Xeogl.Camera
{
    public abstract class XeoglCameraProjection : XeoglComponent
    {
        public abstract XeoglCameraProjectionType ProjectionType { get; }
    }
}
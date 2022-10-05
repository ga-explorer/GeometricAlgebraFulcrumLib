using GraphicsComposerLib.Rendering.Xeogl.Constants;

namespace GraphicsComposerLib.Rendering.Xeogl.Cameras
{
    public abstract class XeoglCameraProjection : XeoglComponent
    {
        public abstract XeoglCameraProjectionType ProjectionType { get; }
    }
}
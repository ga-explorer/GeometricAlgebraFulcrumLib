using GraphicsComposerLib.WebGl.Xeogl.Constants;

namespace GraphicsComposerLib.WebGl.Xeogl.Cameras
{
    public abstract class XeoglCameraProjection : XeoglComponent
    {
        public abstract XeoglCameraProjectionType ProjectionType { get; }
    }
}
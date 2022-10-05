using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Cameras
{
    public abstract class SdlFullCamera : SdlCamera
    {
        public ISdlVectorValue Direction { get; set; }

        public ISdlVectorValue Location { get; set; }

        public ISdlVectorValue LookAt { get; set; }

        public ISdlVectorValue Sky { get; set; }

        public ISdlVectorValue Up { get; set; }

        public ISdlVectorValue Right { get; set; }

        public ISdlScalarValue Angle { get; set; }
    }
}

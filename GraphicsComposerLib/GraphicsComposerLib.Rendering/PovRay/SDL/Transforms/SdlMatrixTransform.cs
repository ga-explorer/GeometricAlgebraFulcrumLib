using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Transforms
{
    public sealed class SdlMatrixTransform : SdlTransform
    {
        public SdlMatrix4X3 Matrix { get; private set; }


        internal SdlMatrixTransform(SdlMatrix4X3 matrix)
        {
            Matrix = matrix;
        }
    }
}

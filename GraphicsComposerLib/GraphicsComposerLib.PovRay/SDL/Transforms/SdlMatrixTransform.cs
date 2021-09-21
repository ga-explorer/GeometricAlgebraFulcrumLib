using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Transforms
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

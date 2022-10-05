using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Transforms
{
    public sealed class SdlRotateTransform : SdlTransform
    {
        /// <summary>
        /// The Euler X->Y->Z Rotation angles in degrees
        /// </summary>
        public ISdlVectorValue EulerXyzAnglesVector { get; private set; }


        internal SdlRotateTransform(ISdlVectorValue anglesVector)
        {
            EulerXyzAnglesVector = anglesVector;
        }

    }
}

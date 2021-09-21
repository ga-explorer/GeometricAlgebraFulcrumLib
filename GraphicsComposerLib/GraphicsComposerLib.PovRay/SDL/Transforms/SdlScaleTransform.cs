using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Transforms
{
    public sealed class SdlScaleTransform : SdlTransform
    {
        public ISdlVectorValue FactorVector { get; private set; }


        internal SdlScaleTransform(ISdlVectorValue factorVector)
        {
            FactorVector = factorVector;
        }
    }
}

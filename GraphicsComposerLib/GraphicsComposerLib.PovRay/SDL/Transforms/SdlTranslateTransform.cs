using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Transforms
{
    public sealed class SdlTranslateTransform : SdlTransform
    {
        public ISdlVectorValue Direction { get; private set; }


        internal SdlTranslateTransform(ISdlVectorValue direction)
        {
            Direction = direction;
        }
    }
}

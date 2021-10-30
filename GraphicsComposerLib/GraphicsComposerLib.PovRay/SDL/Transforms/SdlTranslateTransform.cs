using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Transforms
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

using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlBox : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue Corner1 { get; set; }

        public ISdlVectorValue Corner2 { get; set; }
    }
}

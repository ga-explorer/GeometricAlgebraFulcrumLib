using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Objects.FSP
{
    public class SdlBox : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue Corner1 { get; set; }

        public ISdlVectorValue Corner2 { get; set; }
    }
}

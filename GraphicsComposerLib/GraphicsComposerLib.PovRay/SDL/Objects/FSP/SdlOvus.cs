using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlOvus : SdlObject, ISdlFspObject
    {
        public ISdlScalarValue BottomRadius { get; set; }

        public ISdlScalarValue TopRadius { get; set; }
    }
}

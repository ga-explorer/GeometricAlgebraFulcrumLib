using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlOvus : SdlObject, ISdlFspObject
    {
        public ISdlScalarValue BottomRadius { get; set; }

        public ISdlScalarValue TopRadius { get; set; }
    }
}

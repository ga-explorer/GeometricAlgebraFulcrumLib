using GraphicsComposerLib.POVRay.SDL.Values;

namespace GraphicsComposerLib.POVRay.SDL.Objects.FSP
{
    public class SdlSphere : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue Center { get; set; }

        public ISdlScalarValue Radius { get; set; }
    }
}

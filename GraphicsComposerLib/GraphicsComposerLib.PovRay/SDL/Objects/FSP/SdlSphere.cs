using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.FSP
{
    public class SdlSphere : SdlObject, ISdlFspObject
    {
        public ISdlVectorValue Center { get; set; }

        public ISdlScalarValue Radius { get; set; }
    }
}

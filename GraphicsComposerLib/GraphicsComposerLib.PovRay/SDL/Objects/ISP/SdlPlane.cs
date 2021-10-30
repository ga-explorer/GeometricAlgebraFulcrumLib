using GraphicsComposerLib.PovRay.SDL.Values;

namespace GraphicsComposerLib.PovRay.SDL.Objects.ISP
{
    public class SdlPlane : SdlObject, ISdlIspObject
    {
        public ISdlVectorValue Normal { get; set; }

        public ISdlScalarValue Distance { get; set; }
    }
}

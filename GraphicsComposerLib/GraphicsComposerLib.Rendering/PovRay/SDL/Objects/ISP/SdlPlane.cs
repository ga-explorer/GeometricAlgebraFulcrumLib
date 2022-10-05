using GraphicsComposerLib.Rendering.PovRay.SDL.Values;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Objects.ISP
{
    public class SdlPlane : SdlObject, ISdlIspObject
    {
        public ISdlVectorValue Normal { get; set; }

        public ISdlScalarValue Distance { get; set; }
    }
}

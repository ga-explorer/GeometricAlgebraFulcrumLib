using GraphicsComposerLib.PovRay.SDL.Pigments;

namespace GraphicsComposerLib.PovRay.SDL.Directives
{
    public sealed class SdlDefaultPigmentDirective : SdlDirective
    {
        public ISdlPigment Pigment { get; set; }
    }
}

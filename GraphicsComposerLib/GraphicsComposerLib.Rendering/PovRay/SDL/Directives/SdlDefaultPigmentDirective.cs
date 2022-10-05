using GraphicsComposerLib.Rendering.PovRay.SDL.Pigments;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Directives
{
    public sealed class SdlDefaultPigmentDirective : SdlDirective
    {
        public ISdlPigment Pigment { get; set; }
    }
}

using GraphicsComposerLib.POVRay.SDL.Pigments;

namespace GraphicsComposerLib.POVRay.SDL.Directives
{
    public sealed class SdlDefaultPigmentDirective : SdlDirective
    {
        public ISdlPigment Pigment { get; set; }
    }
}

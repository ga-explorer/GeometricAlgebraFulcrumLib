using GraphicsComposerLib.Rendering.PovRay.SDL.Finishes;

namespace GraphicsComposerLib.Rendering.PovRay.SDL.Directives
{
    public sealed class SdlDefaultFinishDirective : SdlDirective
    {
        public ISdlFinish Finish { get; set; }
    }
}

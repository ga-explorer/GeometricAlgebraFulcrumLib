using GraphicsComposerLib.PovRay.SDL.Finishes;

namespace GraphicsComposerLib.PovRay.SDL.Directives
{
    public sealed class SdlDefaultFinishDirective : SdlDirective
    {
        public ISdlFinish Finish { get; set; }
    }
}

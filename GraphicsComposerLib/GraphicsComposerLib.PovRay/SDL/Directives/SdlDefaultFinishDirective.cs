using GraphicsComposerLib.POVRay.SDL.Finishes;

namespace GraphicsComposerLib.POVRay.SDL.Directives
{
    public sealed class SdlDefaultFinishDirective : SdlDirective
    {
        public ISdlFinish Finish { get; set; }
    }
}

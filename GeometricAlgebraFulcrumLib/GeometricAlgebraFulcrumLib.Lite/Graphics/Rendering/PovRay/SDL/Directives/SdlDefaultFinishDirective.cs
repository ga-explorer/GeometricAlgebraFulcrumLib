using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Finishes;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlDefaultFinishDirective : SdlDirective
{
    public ISdlFinish Finish { get; set; }
}
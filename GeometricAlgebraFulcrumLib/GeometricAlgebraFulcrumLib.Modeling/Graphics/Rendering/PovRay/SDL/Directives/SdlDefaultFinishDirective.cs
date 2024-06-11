using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlDefaultFinishDirective : SdlDirective
{
    public ISdlFinish Finish { get; set; }
}
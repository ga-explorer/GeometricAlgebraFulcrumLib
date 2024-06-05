using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlDefaultFinishDirective : SdlDirective
{
    public ISdlFinish Finish { get; set; }
}
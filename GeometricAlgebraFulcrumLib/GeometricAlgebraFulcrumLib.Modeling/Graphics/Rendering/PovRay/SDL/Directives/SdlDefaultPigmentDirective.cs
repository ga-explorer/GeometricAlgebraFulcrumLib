using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Directives;

public sealed class SdlDefaultPigmentDirective : SdlDirective
{
    public ISdlPigment Pigment { get; set; }
}
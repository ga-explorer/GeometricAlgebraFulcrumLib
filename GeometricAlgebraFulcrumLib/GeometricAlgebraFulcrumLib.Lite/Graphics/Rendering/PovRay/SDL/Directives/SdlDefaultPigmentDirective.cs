using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Pigments;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Directives
{
    public sealed class SdlDefaultPigmentDirective : SdlDirective
    {
        public ISdlPigment Pigment { get; set; }
    }
}

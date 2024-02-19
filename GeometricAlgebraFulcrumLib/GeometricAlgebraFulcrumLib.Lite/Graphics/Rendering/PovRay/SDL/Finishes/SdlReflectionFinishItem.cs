using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlReflectionFinishItem : ISdlFinishItem
{
    public ISdlColorValue MinColor { get; set; }

    public ISdlColorValue MaxColor { get; set; }

    public bool Fresnel { get; set; }

    public ISdlScalarValue FallOffValue { get; set; }

    public ISdlScalarValue ExponentValue { get; set; }

    public ISdlScalarValue MetallicValue { get; set; }


}
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlBrillianceFinishItem : ISdlFinishItem
{
    public ISdlScalarValue Amount { get; set; }
}
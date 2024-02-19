using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlAmbientFinishItem : ISdlFinishItem
{
    public ISdlColorValue Color { get; set; }
}
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlAmbientFinishItem : ISdlFinishItem
{
    public ISdlColorValue Color { get; set; }
}
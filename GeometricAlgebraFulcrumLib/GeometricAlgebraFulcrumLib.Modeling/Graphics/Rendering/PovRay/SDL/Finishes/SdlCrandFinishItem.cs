using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlCrandFinishItem : ISdlFinishItem
{
    public ISdlScalarValue Amount { get; set; }
}
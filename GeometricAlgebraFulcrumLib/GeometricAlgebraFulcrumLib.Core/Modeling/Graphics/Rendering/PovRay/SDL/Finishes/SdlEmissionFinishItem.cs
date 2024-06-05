using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlEmissionFinishItem : ISdlFinishItem
{
    public ISdlColorValue Color { get; set; }
}
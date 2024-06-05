using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Finishes;

public sealed class SdlDiffuseFinishItem : ISdlFinishItem
{
    public bool Albedo { get; set; }

    public ISdlScalarValue Amount { get; set; }
}
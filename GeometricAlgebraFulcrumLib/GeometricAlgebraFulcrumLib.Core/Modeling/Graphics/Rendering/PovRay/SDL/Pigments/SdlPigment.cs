using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;

public abstract class SdlPigment : ISdlPigment
{
    public string PigmentIdentifier { get; set; }

    public ISdlColorValue QuickColor { get; set; }
}
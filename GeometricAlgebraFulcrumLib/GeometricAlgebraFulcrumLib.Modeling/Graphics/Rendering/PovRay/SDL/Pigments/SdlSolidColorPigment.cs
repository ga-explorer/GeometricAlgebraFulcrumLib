using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;

public sealed class SdlSolidColorPigment : SdlPigment
{
    public ISdlColorValue Color { get; set; }


    internal SdlSolidColorPigment(ISdlColorValue color)
    {
        Color = color;
    }
}
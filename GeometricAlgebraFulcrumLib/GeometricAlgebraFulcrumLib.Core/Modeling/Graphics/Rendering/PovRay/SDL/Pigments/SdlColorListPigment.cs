using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Values;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.PovRay.SDL.Pigments;

public enum SdlColorListPigmentPattern
{
    Brick = 0, Checker = 1, Hexagon = 2
}

public sealed class SdlColorListPigment : SdlPigment
{
    private static readonly string[] PatternNames = new[]
    {
        "brick", "checker", "hexagon"
    };


    public SdlColorListPigmentPattern Pattern { get; set; }

    public string PatternName => PatternNames[(int)Pattern];

    public ISdlColorValue Color1 { get; set; }

    public ISdlColorValue Color2 { get; set; }

    public ISdlColorValue Color3 { get; set; }
}
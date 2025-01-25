namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

public sealed class GrPovRayBuiltInPattern :
    GrPovRayPattern
{
    public string PatternName { get; }


    internal GrPovRayBuiltInPattern(string patternName)
    {
        PatternName = patternName;
    }


    public override string GetPovRayCode()
    {
        return PatternName;
    }
}
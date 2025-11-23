namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Volumes.Sampled;

public sealed class AdaptiveVolumeSamplingOptions
{
    public double MaxGradientAngleRadians { get; set; } = 0.17453292519943295; // 10°
    public double RelativeChordError { get; set; } = 1e-3;
    public int MinDepth { get; set; } = 0;
    public int MaxDepth { get; set; } = 8;
    public bool ParallelEvaluation { get; set; } = true;
}
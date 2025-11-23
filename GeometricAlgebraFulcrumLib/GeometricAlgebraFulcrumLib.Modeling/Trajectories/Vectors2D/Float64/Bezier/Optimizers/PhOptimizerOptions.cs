namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier.Optimizers;

public class PhOptimizerOptions
{
    public double KappaMax { get; set; } = 1.0;
    public int SamplesPerSegment { get; set; } = 64;
    public double CurvaturePenaltyWeight { get; set; } = 100.0;
    public double EnergyWeight { get; set; } = 0.1;
    public double WaypointWeight { get; set; } = 1.0;
    public int MaxG2ItersPerJoin { get; set; } = 6;
    public double G2Tolerance { get; set; } = 1e-8;
    public double GenomeEpsilon = 1e-12;
}
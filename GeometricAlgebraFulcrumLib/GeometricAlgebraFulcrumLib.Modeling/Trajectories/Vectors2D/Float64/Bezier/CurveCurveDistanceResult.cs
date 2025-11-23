namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

public struct CurveCurveDistanceResult
{
    public double Distance { get; }
    public double T { get; }
    public double S { get; }
    public double[] Point1 { get; }
    public double[] Point2 { get; }

    public CurveCurveDistanceResult(double distance, double t, double s, double[] p1, double[] p2)
    {
        Distance = distance;
        T = t;
        S = s;
        Point1 = p1;
        Point2 = p2;
    }
}
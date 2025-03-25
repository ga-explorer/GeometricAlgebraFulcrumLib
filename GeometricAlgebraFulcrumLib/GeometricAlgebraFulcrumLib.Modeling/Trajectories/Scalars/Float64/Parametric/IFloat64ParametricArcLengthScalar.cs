namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Parametric;

public interface IFloat64ParametricArcLengthScalar :
    IFloat64Trajectory<double>
{
    double GetLength();

    double TimeToLength(double parameterValue);

    double LengthToTime(double length);
}
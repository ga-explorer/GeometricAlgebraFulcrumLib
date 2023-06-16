namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves
{
    public interface IArcLengthCurve3D :
        IParametricCurve3D
    {
        double GetLength();

        double ParameterToLength(double parameterValue);

        double LengthToParameter(double length);
    }
}
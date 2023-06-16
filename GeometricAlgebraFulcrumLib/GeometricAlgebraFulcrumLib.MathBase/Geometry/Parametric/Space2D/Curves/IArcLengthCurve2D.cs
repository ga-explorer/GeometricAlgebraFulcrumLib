namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves
{
    public interface IArcLengthCurve2D :
        IParametricCurve2D
    {
        double GetLength();

        double ParameterToLength(double parameterValue);

        double LengthToParameter(double length);
    }
}
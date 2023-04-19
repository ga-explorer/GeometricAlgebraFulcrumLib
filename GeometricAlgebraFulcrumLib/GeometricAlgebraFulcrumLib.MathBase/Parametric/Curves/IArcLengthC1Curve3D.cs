namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    public interface IArcLengthC1Curve3D :
        IParametricFiniteC1Curve3D
    {
        double GetLength();

        double ParameterToLength(double parameterValue);

        double LengthToParameter(double length);
    }
}
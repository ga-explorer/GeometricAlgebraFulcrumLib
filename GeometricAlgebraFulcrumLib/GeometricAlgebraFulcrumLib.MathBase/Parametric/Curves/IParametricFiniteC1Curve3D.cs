namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves
{
    public interface IParametricFiniteC1Curve3D :
        IParametricCurve3D
    {
        double ParameterValueMin { get; }

        double ParameterValueMax { get; }
    }
}
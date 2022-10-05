namespace NumericalGeometryLib.BasicMath.Calculus;

public interface IScalarDnFunction :
    IScalarD3Function
{
    double GetDerivative(double t, int n);
}
namespace NumericalGeometryLib.BasicMath.Calculus;

public interface IScalarD3Function :
    IScalarD2Function
{
    double GetThirdDerivative(double t);
}
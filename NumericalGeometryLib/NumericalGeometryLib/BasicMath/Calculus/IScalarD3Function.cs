namespace NumericalGeometryLib.BasicMath.Calculus;

public interface IScalarD3Function :
    IScalarD2Function
{
    double GetThirdDerivativeValue(double t);
}
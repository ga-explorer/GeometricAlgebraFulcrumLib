namespace NumericalGeometryLib.BasicMath.Calculus;

public interface IScalarD2Function :
    IScalarD1Function
{
    double GetSecondDerivative(double t);
}
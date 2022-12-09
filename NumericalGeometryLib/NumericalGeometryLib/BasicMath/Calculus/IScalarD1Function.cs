namespace NumericalGeometryLib.BasicMath.Calculus;

public interface IScalarD1Function :
    IScalarD0Function
{
    double GetFirstDerivativeValue(double t);
}
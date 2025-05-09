using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;

public sealed class GbBernsteinBasisSet :
    GbBernsteinBasisSetBase
{
    public Func<double, double> EvaluateDegree20Func { get; }

    public Func<double, double> EvaluateDegree22Func { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GbBernsteinBasisSet(int degree, Func<double, double> evaluateDegree20Func, Func<double, double> evaluateDegree22Func)
        : base(degree)
    {
        EvaluateDegree20Func = evaluateDegree20Func;
        EvaluateDegree22Func = evaluateDegree22Func;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValueDegree20(double parameterValue)
    {
        return EvaluateDegree20Func(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValueDegree22(double parameterValue)
    {
        return EvaluateDegree22Func(parameterValue);
    }
}
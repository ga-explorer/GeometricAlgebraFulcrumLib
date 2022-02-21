using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.Polynomials.CurveBasis
{
    public sealed class GbBernsteinBasisSet :
        GbBernsteinBasisSetBase
    {
        public Func<double, double> EvaluateDegree20Func { get; }

        public Func<double, double> EvaluateDegree22Func { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GbBernsteinBasisSet(int degree, [NotNull] Func<double, double> evaluateDegree20Func, [NotNull] Func<double, double> evaluateDegree22Func) 
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
}
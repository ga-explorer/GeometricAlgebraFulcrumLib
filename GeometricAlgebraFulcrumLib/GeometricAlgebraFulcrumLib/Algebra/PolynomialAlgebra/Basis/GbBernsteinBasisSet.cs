using System;
using System.Runtime.CompilerServices;
using Antlr4.Runtime.Misc;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public sealed class GbBernsteinBasisSet<T> :
        GbBernsteinBasisSetBase<T>
    {
        public Func<T, T> EvaluateDegree20Func { get; }

        public Func<T, T> EvaluateDegree22Func { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GbBernsteinBasisSet(IScalarAlgebraProcessor<T> scalarProcessor, int degree, [NotNull] Func<T, T> evaluateDegree20Func, [NotNull] Func<T, T> evaluateDegree22Func) 
            : base(scalarProcessor, degree)
        {
            EvaluateDegree20Func = evaluateDegree20Func;
            EvaluateDegree22Func = evaluateDegree22Func;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValueDegree20(T parameterValue)
        {
            return EvaluateDegree20Func(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValueDegree22(T parameterValue)
        {
            return EvaluateDegree22Func(parameterValue);
        }
    }
}
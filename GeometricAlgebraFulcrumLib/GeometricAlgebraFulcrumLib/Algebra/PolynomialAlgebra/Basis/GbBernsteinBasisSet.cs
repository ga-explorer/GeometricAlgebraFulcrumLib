using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    /// <summary>
    /// Defines a set of Generalized Blended Bernstein Basis polynomials.
    /// See paper "Geometric modeling and applications of generalized
    /// blended trigonometric Bézier curves with shape parameters"
    /// https://link.springer.com/article/10.1186/s13662-020-03001-4
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GbBernsteinBasisSet<T> :
        GbBernsteinBasisSetBase<T>
    {
        /// <summary>
        /// Degree 2 basis polynomial 0
        /// </summary>
        public Func<T, T> EvaluateDegree20Func { get; }

        /// <summary>
        /// Degree 2 basis polynomial 2
        /// </summary>
        public Func<T, T> EvaluateDegree22Func { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GbBernsteinBasisSet(IScalarAlgebraProcessor<T> scalarProcessor, int degree, [NotNull] Func<T, T> evaluateDegree20Func, [NotNull] Func<T, T> evaluateDegree22Func) 
            : base(scalarProcessor, degree)
        {
            EvaluateDegree20Func = evaluateDegree20Func;
            EvaluateDegree22Func = evaluateDegree22Func;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetValueDegree20(T parameterValue)
        {
            return EvaluateDegree20Func(parameterValue).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetValueDegree22(T parameterValue)
        {
            return EvaluateDegree22Func(parameterValue).CreateScalar(ScalarProcessor);
        }
    }
}
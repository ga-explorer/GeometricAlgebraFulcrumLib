using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public sealed class BernsteinBasisSet<T> :
        IPolynomialBasisSet<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int Degree { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BernsteinBasisSet([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int degree)
        {
            if (degree is < 0 or > 64)
                throw new ArgumentOutOfRangeException(nameof(degree));

            Degree = degree;
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(int index, T parameterValue)
        {
            if (index < 0 || index > Degree)
                return ScalarProcessor.ScalarZero;

            var parameterValueMinusOne = 
                ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, parameterValue);

            return ScalarProcessor.Times(
                ScalarProcessor.BinomialCoefficient(Degree, index),
                ScalarProcessor.Power(parameterValue, index),
                ScalarProcessor.Power(parameterValueMinusOne, Degree - index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(int index, T parameterValue, T termScalar)
        {
            return ScalarProcessor.Times(
                termScalar,
                GetValue(index, parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(T parameterValue, params T[] termScalarsList)
        {
            return ScalarProcessor.Add(
                termScalarsList.Select(
                    (termScalar, index) => GetValue(index, parameterValue, termScalar)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetValues(T parameterValue)
        {
            return Enumerable.Range(0, Degree + 1).Select(
                index => GetValue(index, parameterValue)
            ).ToArray();
        }
    }
}
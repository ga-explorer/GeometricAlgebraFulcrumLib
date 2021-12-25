using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public sealed class MonomialBasisSet<T> :
        IPolynomialBasisSet<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int Degree { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MonomialBasisSet([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int degree)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            ScalarProcessor = scalarProcessor;
            Degree = degree;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(int index, T parameterValue)
        {
            if (index < 0 || index > Degree)
                throw new ArgumentOutOfRangeException(nameof(index));

            return ScalarProcessor.Power(parameterValue, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(int index, T parameterValue, T termScalar)
        {
            if (index < 0 || index > Degree)
                throw new ArgumentOutOfRangeException(nameof(index));

            return ScalarProcessor.Times(
                termScalar, 
                ScalarProcessor.Power(parameterValue, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(T parameterValue, params T[] termScalarsList)
        {
            return ScalarProcessor.Add(
                termScalarsList.Select(
                    (scalar, index) => GetValue(index, parameterValue, scalar)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetValues(T parameterValue)
        {
            return Enumerable
                .Range(0, Degree + 1)
                .Select(index => GetValue(index, parameterValue))
                .ToArray();
        }
    }
}
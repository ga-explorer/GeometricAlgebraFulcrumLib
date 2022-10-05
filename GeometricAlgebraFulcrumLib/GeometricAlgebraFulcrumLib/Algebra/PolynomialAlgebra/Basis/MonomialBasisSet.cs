using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    public sealed class MonomialBasisSet<T> :
        IPolynomialBasisSet<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonomialBasisSet<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, int degree)
        {
            return new MonomialBasisSet<T>(scalarProcessor, degree);
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int Degree { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private MonomialBasisSet([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int degree)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            ScalarProcessor = scalarProcessor;
            Degree = degree;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(int index, T parameterValue)
        {
            if (index < 0 || index > Degree)
                throw new ArgumentOutOfRangeException(nameof(index));

            return index == 0
                ? ScalarProcessor.CreateScalarOne()
                : ScalarProcessor.Power(parameterValue, index).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(int index, T parameterValue, T termScalar)
        {
            if (index < 0 || index > Degree)
                throw new ArgumentOutOfRangeException(nameof(index));

            return ScalarProcessor.Times(
                termScalar, 
                index == 0
                    ? ScalarProcessor.ScalarOne
                    : ScalarProcessor.Power(parameterValue, index)
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetValue(T parameterValue, params T[] termScalarsList)
        {
            return ScalarProcessor.Add(
                termScalarsList.Select(
                    (scalar, index) => GetValue(index, parameterValue, scalar).ScalarValue
                )
            ).CreateScalar(ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<T> GetValues(T parameterValue)
        {
            return Enumerable
                .Range(0, Degree + 1)
                .Select(index => GetValue(index, parameterValue).ScalarValue)
                .ToArray();
        }
    }
}
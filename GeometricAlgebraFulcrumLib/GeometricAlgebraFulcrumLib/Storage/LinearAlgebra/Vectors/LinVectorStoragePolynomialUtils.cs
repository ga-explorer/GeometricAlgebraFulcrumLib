using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public static class LinVectorStoragePolynomialUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params ILinVectorStorage<T>[] vectorsList)
        {
            return basisSet.ScalarProcessor.Add(
                vectorsList.Select((mv, index) =>
                    basisSet.ScalarProcessor.Times(
                        mv,
                        basisSet.GetValue(index, parameterValue).ScalarValue
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, IEnumerable<ILinVectorStorage<T>> vectorsList)
        {
            return basisSet.ScalarProcessor.Add(
                vectorsList.Select((mv, index) =>
                    basisSet.ScalarProcessor.Times(
                        mv,
                        basisSet.GetValue(index, parameterValue).ScalarValue
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params GaVector<T>[] vectorsList)
        {
            var processor = vectorsList[0].GeometricProcessor;

            return vectorsList.Select(
                (mv, index) => mv * basisSet.GetValue(index, parameterValue)
            ).Aggregate(
                processor.CreateVectorZero(),
                (a, b) => a + b
            );
        }
    }
}

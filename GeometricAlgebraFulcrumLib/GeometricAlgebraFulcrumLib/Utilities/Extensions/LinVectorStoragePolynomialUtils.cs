using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
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
                        basisSet.GetValue(index, parameterValue)
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
                        basisSet.GetValue(index, parameterValue)
                    )
                )
            );
        }
    }
}
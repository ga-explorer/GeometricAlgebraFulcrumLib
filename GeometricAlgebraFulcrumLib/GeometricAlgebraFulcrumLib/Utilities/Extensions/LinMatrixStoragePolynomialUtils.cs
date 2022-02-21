using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinMatrixStoragePolynomialUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params ILinMatrixStorage<T>[] vectorsList)
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
        public static ILinMatrixStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, IEnumerable<ILinMatrixStorage<T>> vectorsList)
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
    }
}
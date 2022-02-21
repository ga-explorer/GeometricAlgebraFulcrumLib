using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStoragePolynomialUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params VectorStorage<T>[] vectorsList)
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
        public static VectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, IEnumerable<VectorStorage<T>> vectorsList)
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
        public static BivectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params BivectorStorage<T>[] vectorsList)
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
        public static BivectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, IEnumerable<BivectorStorage<T>> vectorsList)
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
        public static IMultivectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params IMultivectorStorage<T>[] multivectorsList)
        {
            return basisSet.ScalarProcessor.Add(
                multivectorsList.Select((mv, index) => 
                    basisSet.ScalarProcessor.Times(
                        mv,
                        basisSet.GetValue(index, parameterValue).ScalarValue
                    )
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, IEnumerable<IMultivectorStorage<T>> multivectorsList)
        {
            return basisSet.ScalarProcessor.Add(
                multivectorsList.Select((mv, index) => 
                    basisSet.ScalarProcessor.Times(
                        mv,
                        basisSet.GetValue(index, parameterValue).ScalarValue
                    )
                )
            );
        }
    }
}
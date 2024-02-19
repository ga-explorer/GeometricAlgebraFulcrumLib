using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

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
    public static XGaVector<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params XGaVector<T>[] vectorsList)
    {
        var processor = vectorsList[0].Processor;

        return vectorsList.Select(
            (mv, index) => mv * basisSet.GetValue(index, parameterValue)
        ).Aggregate(
            processor.CreateZeroVector(),
            (a, b) => a + b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params RGaVector<T>[] vectorsList)
    {
        var processor = vectorsList[0].Processor;

        return vectorsList.Select(
            (mv, index) => mv * basisSet.GetValue(index, parameterValue)
        ).Aggregate(
            processor.CreateZeroVector(),
            (a, b) => a + b
        );
    }
}
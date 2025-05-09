using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic;

public static class PolynomialsUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> GetValue<T>(this IPolynomialBasisSet<T> basisSet, T parameterValue, params XGaVector<T>[] vectorsList)
    {
        var processor = vectorsList[0].Processor;

        return vectorsList.Select(
            (mv, index) => mv * basisSet.GetValue(index, parameterValue)
        ).Aggregate(
            processor.VectorZero,
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
            processor.VectorZero,
            (a, b) => a + b
        );
    }
}

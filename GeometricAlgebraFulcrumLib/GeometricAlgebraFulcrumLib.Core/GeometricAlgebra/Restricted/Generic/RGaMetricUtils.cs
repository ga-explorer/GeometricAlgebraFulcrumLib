using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic;

public static class RGaMetricUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidVectorDictionary<T>(this IReadOnlyDictionary<ulong, T> basisScalarDictionary, IScalarProcessor<T> scalarProcessor)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<ulong, T>,

            1 => basisScalarDictionary is SingleItemDictionary<ulong, T> dict &&
                 dict.Key.Grade() == 1 &&
                 scalarProcessor.IsValid(dict.Value) &&
                 !scalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                p.Key.Grade() == 1 &&
                scalarProcessor.IsValid(p.Value) &&
                !scalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBivectorDictionary<T>(this IReadOnlyDictionary<ulong, T> basisScalarDictionary, IScalarProcessor<T> scalarProcessor)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<ulong, T>,

            1 => basisScalarDictionary is SingleItemDictionary<ulong, T> dict &&
                 dict.Key.Grade() == 2 &&
                 scalarProcessor.IsValid(dict.Value) &&
                 !scalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                p.Key.Grade() == 2 &&
                scalarProcessor.IsValid(p.Value) &&
                !scalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidKVectorDictionary<T>(this IReadOnlyDictionary<ulong, T> basisScalarDictionary, IScalarProcessor<T> scalarProcessor, int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<ulong, T>,

            1 => basisScalarDictionary is SingleItemDictionary<ulong, T> dict &&
                 dict.Key.Grade() == grade &&
                 scalarProcessor.IsValid(dict.Value) &&
                 !scalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                p.Key.Grade() == grade &&
                scalarProcessor.IsValid(p.Value) &&
                !scalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidMultivectorDictionary<T>(this IReadOnlyDictionary<ulong, T> basisScalarDictionary, IScalarProcessor<T> scalarProcessor)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<ulong, T>,

            1 => basisScalarDictionary is SingleItemDictionary<ulong, T> dict &&
                 scalarProcessor.IsValid(dict.Value) &&
                 !scalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                scalarProcessor.IsValid(p.Value) &&
                !scalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidMultivectorDictionary<T>(this RGaMetric metric, IReadOnlyDictionary<int, RGaKVector<T>> gradeKVectorDictionary)
    {
        return gradeKVectorDictionary.Count switch
        {
            0 => gradeKVectorDictionary is EmptyDictionary<int, RGaKVector<T>>,

            1 => gradeKVectorDictionary is SingleItemDictionary<int, RGaKVector<T>> dict &&
                 dict.Key >= 0 &&
                 dict.Value.Metric.HasSameSignature(metric) &&
                 dict.Value.IsValid(),

            _ => gradeKVectorDictionary.All(p =>
                p.Key >= 0 &&
                p.Value.Metric.HasSameSignature(metric) &&
                p.Value.IsValid()
            )
        };
    }

}
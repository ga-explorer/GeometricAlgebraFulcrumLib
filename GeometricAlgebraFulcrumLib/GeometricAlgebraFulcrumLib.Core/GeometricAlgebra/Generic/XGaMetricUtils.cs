﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic;

public static class XGaMetricUtils
{
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidVectorDictionary<T>(this IReadOnlyDictionary<IndexSet, T> basisScalarDictionary, XGaProcessor<T> processor)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, T>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, T> dict &&
                 dict.Key.Count == 1 &&
                 processor.ScalarProcessor.IsValid(dict.Value) &&
                 !processor.ScalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == 1 &&
                processor.ScalarProcessor.IsValid(p.Value) &&
                !processor.ScalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBivectorDictionary<T>(this IReadOnlyDictionary<IndexSet, T> basisScalarDictionary, XGaProcessor<T> processor)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, T>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, T> dict &&
                 dict.Key.Count == 2 &&
                 processor.ScalarProcessor.IsValid(dict.Value) &&
                 !processor.ScalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == 2 &&
                processor.ScalarProcessor.IsValid(p.Value) &&
                !processor.ScalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidKVectorDictionary<T>(this IReadOnlyDictionary<IndexSet, T> basisScalarDictionary, XGaProcessor<T> processor, int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, T>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, T> dict &&
                 dict.Key.Count == grade &&
                 processor.ScalarProcessor.IsValid(dict.Value) &&
                 !processor.ScalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == grade &&
                processor.ScalarProcessor.IsValid(p.Value) &&
                !processor.ScalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidMultivectorDictionary<T>(this IReadOnlyDictionary<IndexSet, T> basisScalarDictionary, XGaProcessor<T> processor)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, T>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, T> dict &&
                 processor.ScalarProcessor.IsValid(dict.Value) &&
                 !processor.ScalarProcessor.IsZero(dict.Value),

            _ => basisScalarDictionary.All(p =>
                processor.ScalarProcessor.IsValid(p.Value) &&
                !processor.ScalarProcessor.IsZero(p.Value)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidMultivectorDictionary<T>(this XGaMetric metric, IReadOnlyDictionary<int, XGaKVector<T>> gradeKVectorDictionary)
    {
        return gradeKVectorDictionary.Count switch
        {
            0 => gradeKVectorDictionary is EmptyDictionary<int, XGaKVector<T>>,

            1 => gradeKVectorDictionary is SingleItemDictionary<int, XGaKVector<T>> dict &&
                 dict.Key >= 0 &&
                 dict.Value.Processor.HasSameSignature(metric) &&
                 dict.Value.IsValid(),

            _ => gradeKVectorDictionary.All(p =>
                p.Key >= 0 &&
                p.Value.Processor.HasSameSignature(metric) &&
                p.Value.IsValid()
            )
        };
    }
}
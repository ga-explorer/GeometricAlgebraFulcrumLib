using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended;

public static class XGaMetricUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidVectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Key.Count == 1 &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == 1 &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBivectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Key.Count == 2 &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == 2 &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidKVectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary, int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Key.Count == grade &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == grade &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidMultivectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidMultivectorDictionary(this XGaMetric metric, IReadOnlyDictionary<int, XGaFloat64KVector> gradeKVectorDictionary)
    {
        return gradeKVectorDictionary.Count switch
        {
            0 => gradeKVectorDictionary is EmptyDictionary<int, XGaFloat64KVector>,

            1 => gradeKVectorDictionary is SingleItemDictionary<int, XGaFloat64KVector> dict &&
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
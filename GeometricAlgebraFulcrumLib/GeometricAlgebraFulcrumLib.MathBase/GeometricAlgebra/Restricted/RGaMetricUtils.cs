using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted
{
    public static class RGaMetricUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEuclidean(this RGaMetric metric)
        {
            return metric is null || metric.IsEuclidean;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidVectorDictionary(this IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            return basisScalarDictionary.Count switch
            {
                0 => basisScalarDictionary is EmptyDictionary<ulong, double>,

                1 => basisScalarDictionary is SingleItemDictionary<ulong, double> dict &&
                     dict.Key.Grade() == 1 &&
                     dict.Value.IsValid() &&
                     !dict.Value.IsZero(),

                _ => basisScalarDictionary.All(p =>
                    p.Key.Grade() == 1 &&
                    p.Value.IsValid() &&
                    !p.Value.IsZero()
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBivectorDictionary(this IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            return basisScalarDictionary.Count switch
            {
                0 => basisScalarDictionary is EmptyDictionary<ulong, double>,

                1 => basisScalarDictionary is SingleItemDictionary<ulong, double> dict &&
                     dict.Key.Grade() == 2 &&
                     dict.Value.IsValid() &&
                     !dict.Value.IsZero(),

                _ => basisScalarDictionary.All(p =>
                    p.Key.Grade() == 2 &&
                    p.Value.IsValid() &&
                    !p.Value.IsZero()
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidKVectorDictionary(this IReadOnlyDictionary<ulong, double> basisScalarDictionary, int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            return basisScalarDictionary.Count switch
            {
                0 => basisScalarDictionary is EmptyDictionary<ulong, double>,

                1 => basisScalarDictionary is SingleItemDictionary<ulong, double> dict &&
                     dict.Key.Grade() == grade &&
                     dict.Value.IsValid() &&
                     !dict.Value.IsZero(),

                _ => basisScalarDictionary.All(p =>
                    p.Key.Grade() == grade &&
                    p.Value.IsValid() &&
                    !p.Value.IsZero()
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidMultivectorDictionary(this IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            return basisScalarDictionary.Count switch
            {
                0 => basisScalarDictionary is EmptyDictionary<ulong, double>,

                1 => basisScalarDictionary is SingleItemDictionary<ulong, double> dict &&
                     dict.Value.IsValid() &&
                     !dict.Value.IsZero(),

                _ => basisScalarDictionary.All(p =>
                    p.Value.IsValid() &&
                    !p.Value.IsZero()
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidMultivectorDictionary(this RGaMetric metric, IReadOnlyDictionary<int, RGaFloat64KVector> gradeKVectorDictionary)
        {
            return gradeKVectorDictionary.Count switch
            {
                0 => gradeKVectorDictionary is EmptyDictionary<int, RGaFloat64KVector>,

                1 => gradeKVectorDictionary is SingleItemDictionary<int, RGaFloat64KVector> dict &&
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

        
        public static IEnumerable<KeyValuePair<ulong, T>> OrderByGradeIndex<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            var termsArray = termsList.ToArray();

            if (termsArray.Length == 0)
                return termsArray;

            var bitsCount = termsArray
                .Max(t => t.Key)
                .LastOneBitPosition() + 1;

            if (bitsCount == 0)
                return termsArray;

            return termsArray
                .OrderBy(t => t.Key.Grade())
                .ThenByDescending(t => t.Key.ReverseBits(bitsCount));
        }

        public static IEnumerable<KeyValuePair<IIndexSet, T>> OrderByGradeIndex<T>(this IEnumerable<KeyValuePair<IIndexSet, T>> termsList)
        {
            var termsArray = 
                termsList.ToImmutableArray();

            if (termsArray.Length == 0)
                return termsArray;

            var maxIndex = 
                termsArray.Max(t => 
                    t.Key.VSpaceDimensions()
                ) - 1;

            if (maxIndex <= 0)
                return termsArray;

            return termsArray
                .OrderBy(t => t.Key.Grade())
                .ThenByDescending(t => t.Key.MapIndices(i => maxIndex - i));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<ulong, T>> OrderById<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
        {
            return termsList.OrderBy(
                t => t.Key
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<IIndexSet, T>> OrderById<T>(this IEnumerable<KeyValuePair<IIndexSet, T>> termsList)
        {
            return termsList.OrderBy(
                t => t.Key
            );
        }
    }
}

using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis
{
    public static class RGaBasisBladeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaSignedBasisBlade ToSignedBasisBlade(this RGaBasisBlade basisBlade, IntegerSign sign)
        {
            return basisBlade.CreateSignedBasisBlade(sign);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaTerm<T> ToTerm<T>(this EGaBasisBlade basisBlade, Scalar<T> scalar)
        //{
        //    return GaTermFactoryUtils.ToTerm(basisBlade, scalar);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaTerm<T> ToTerm<T>(this EGaBasisBlade basisBlade, IScalarProcessor<T> scalarProcessor, T scalarValue)
        //{
        //    return GaTermFactoryUtils.ToTerm(basisBlade, scalarProcessor, scalarValue);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, IScalarProcessor<T> scalarProcessor)
        //{
        //    var scalar = term.Sign switch
        //    {
        //        0 => scalarProcessor.CreateScalarZero(),
        //        > 0 => scalarProcessor.CreateScalarOne(),
        //        _ => scalarProcessor.CreateScalarMinusOne()
        //    };

        //    return GaTermFactoryUtils.ToTerm(term.BasisBlade, scalar);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, IScalarProcessor<T> scalarProcessor, T scalarValue)
        //{
        //    var scalar = term.Sign switch
        //    {
        //        0 => scalarProcessor.CreateScalarZero(),
        //        > 0 => scalarProcessor.CreateScalar(scalarValue),
        //        _ => scalarProcessor.CreateScalar(scalarProcessor.Negative(scalarValue))
        //    };

        //    return GaTermFactoryUtils.ToTerm(term.BasisBlade, scalar);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaTerm<T> ToTerm<T>(this EGaSignedBasisBlade term, Scalar<T> scalar)
        //{
        //    return GaTermFactoryUtils.ToTerm(term.BasisBlade, scalar * term.Sign);
        //}
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(int basisVectorIndex)
        {
            return basisVectorIndex >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBivectorIndices(int basisBivectorIndex1, int basisBivectorIndex2)
        {
            return basisBivectorIndex1 >= 0 && 
                   basisBivectorIndex1 < basisBivectorIndex2;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeIndexSet(ImmutableSortedSet<int> basisBladeIndexSet)
        {
            return basisBladeIndexSet.Count == 0 || 
                   basisBladeIndexSet.First() >= 0;
        }

        public static bool IsValidBasisBladeIndexSet(IEnumerable<int> basisBladeIndexSet)
        {
            if (basisBladeIndexSet is ImmutableSortedSet<int> set)
                return IsValidBasisBladeIndexSet(set);

            var i = -1;
            foreach (var index in basisBladeIndexSet)
            {
                if (i >= index)
                    return false;

                i = index;
            }

            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorVectorOp(int basisVector1Index, int basisVector2Index)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVector1Index) &&
                IsValidBasisVectorIndex(basisVector2Index)
            );

            return basisVector1Index == basisVector2Index;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorBivectorOp(int basisVectorIndex, int basisBivectorIndex1, int basisBivectorIndex2)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2)
            );

            return basisVectorIndex == basisBivectorIndex1 ||
                   basisVectorIndex == basisBivectorIndex2;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorBladeOp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Contains(basisVectorIndex);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBivectorVectorOp(int basisBivectorIndex1, int basisBivectorIndex2, int basisVectorIndex)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2)
            );

            return basisVectorIndex == basisBivectorIndex1 ||
                   basisVectorIndex == basisBivectorIndex2;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBivectorBivectorOp(int basisBivector1Index1, int basisBivector1Index2, int basisBivector2Index1, int basisBivector2Index2)
        {
            Debug.Assert(
                IsValidBasisBivectorIndices(basisBivector1Index1, basisBivector1Index2) &&
                IsValidBasisBivectorIndices(basisBivector2Index1, basisBivector2Index2)
            );

            return basisBivector1Index1 == basisBivector2Index1 ||
                   basisBivector1Index1 == basisBivector2Index2 ||
                   basisBivector1Index2 == basisBivector2Index1 ||
                   basisBivector1Index2 == basisBivector2Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBivectorBladeOp(int basisBivectorIndex1, int basisBivectorIndex2, IReadOnlySet<int> basisBladeIndexSet)
        {
            Debug.Assert(
                IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Contains(basisBivectorIndex1) ||
                   basisBladeIndexSet.Contains(basisBivectorIndex2);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeVectorOp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Contains(basisVectorIndex);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeBivectorOp(IReadOnlySet<int> basisBladeIndexSet, int basisBivectorIndex1, int basisBivectorIndex2)
        {
            Debug.Assert(
                IsValidBasisBivectorIndices(basisBivectorIndex1, basisBivectorIndex2) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Contains(basisBivectorIndex1) ||
                   basisBladeIndexSet.Contains(basisBivectorIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeBladeOp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        {
            Debug.Assert(
                IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
                IsValidBasisBladeIndexSet(basisBlade1IndexSet)
            );

            return basisBlade1IndexSet.Overlaps(basisBlade2IndexSet);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorVectorESp(int basisVector1Index, int basisVector2Index)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVector1Index) &&
                IsValidBasisVectorIndex(basisVector2Index)
            );

            return basisVector1Index != basisVector2Index;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorBladeESp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Count != 1 || 
                   basisBladeIndexSet.First() != basisVectorIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeVectorESp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Count != 1 || 
                   basisBladeIndexSet.First() != basisVectorIndex;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeBladeESp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        {
            Debug.Assert(
                IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
                IsValidBasisBladeIndexSet(basisBlade2IndexSet)
            );

            return !basisBlade1IndexSet.SetEquals(basisBlade2IndexSet);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorVectorELcp(int basisVector1Index, int basisVector2Index)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVector1Index) &&
                IsValidBasisVectorIndex(basisVector2Index)
            );

            return basisVector1Index != basisVector2Index;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorBladeELcp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return !basisBladeIndexSet.Contains(basisVectorIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeVectorELcp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Count switch
            {
                0 => false,
                1 => basisBladeIndexSet.First() != basisVectorIndex,
                _ => true
            };
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeBladeELcp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        {
            Debug.Assert(
                IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
                IsValidBasisBladeIndexSet(basisBlade2IndexSet)
            );

            return !basisBlade1IndexSet.IsSubsetOf(basisBlade2IndexSet);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorVectorERcp(int basisVector1Index, int basisVector2Index)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVector1Index) &&
                IsValidBasisVectorIndex(basisVector2Index)
            );

            return basisVector1Index != basisVector2Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroVectorBladeERcp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return basisBladeIndexSet.Count switch
            {
                0 => false,
                1 => basisBladeIndexSet.First() != basisVectorIndex,
                _ => true
            };
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeVectorERcp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            return !basisBladeIndexSet.Contains(basisVectorIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroBladeBladeERcp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        {
            Debug.Assert(
                IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
                IsValidBasisBladeIndexSet(basisBlade2IndexSet)
            );

            return !basisBlade1IndexSet.IsSubsetOf(basisBlade2IndexSet);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeVectorVectorEGp(int basisVector1Index, int basisVector2Index)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVector1Index) &&
                IsValidBasisVectorIndex(basisVector2Index)
            );

            return basisVector1Index > basisVector2Index;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeVectorBladeEGp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            if (basisBladeIndexSet.Count == 1)
                return basisVectorIndex > basisBladeIndexSet.First();

            return basisBladeIndexSet
                .TakeWhile(index2 => basisVectorIndex > index2)
                .Aggregate(false, (current, _) => !current);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeBladeVectorEGp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        {
            Debug.Assert(
                IsValidBasisVectorIndex(basisVectorIndex) &&
                IsValidBasisBladeIndexSet(basisBladeIndexSet)
            );

            if (basisBladeIndexSet.Count == 1)
                return basisBladeIndexSet.First() > basisVectorIndex;

            return basisBladeIndexSet
                .TakeWhile(index1 => index1 > basisVectorIndex)
                .Aggregate(false, (current, _) => !current);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeBladeBladeEGp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        {
            Debug.Assert(
                IsValidBasisBladeIndexSet(basisBlade1IndexSet) &&
                IsValidBasisBladeIndexSet(basisBlade2IndexSet)
            );

            if (basisBlade1IndexSet.Count == 0)
                return false;

            if (basisBlade2IndexSet.Count == 0)
                return false;

            if (basisBlade1IndexSet.Count == 1)
                return IsNegativeVectorBladeEGp(basisBlade1IndexSet.First(), basisBlade2IndexSet);

            if (basisBlade2IndexSet.Count == 1)
                return IsNegativeBladeVectorEGp(basisBlade1IndexSet, basisBlade2IndexSet.First());

            return basisBlade1IndexSet
                .Reverse()
                .Aggregate(
                    false, 
                    (isNegative, index1) => isNegative ^ IsNegativeVectorBladeEGp(index1, basisBlade2IndexSet)
                );
        }

    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorVectorEGp(int basisVector1Index, int basisVector2Index)
        //{
        //    Debug.Assert(
        //        IsValidBasisVectorIndex(basisVector1Index) &&
        //        IsValidBasisVectorIndex(basisVector2Index)
        //    );

        //    return basisVector1Index == basisVector2Index 
        //        ? EGaSignedBasisBlade.CreatePositiveScalar() 
        //        : EGaSignedBasisBlade.CreatePositiveBivector(basisVector1Index, basisVector2Index);
        //}
    
        //public static EGaSignedBasisBlade VectorBivectorEGp(int basisVectorIndex, int basisBivectorIndex1, int basisBivectorIndex2)
        //{
        //    Debug.Assert(
        //        IsValidBasisVectorIndex(basisVectorIndex) && 
        //        IsValidBasisVectorIndex(basisBivectorIndex1) &&
        //        IsValidBasisVectorIndex(basisBivectorIndex2)
        //    );

        //    if (basisBivectorIndex1 == basisBivectorIndex2)
        //        return VectorVectorEGp(basisVectorIndex, basisBivectorIndex1);

        //    if (basisBivectorIndex1 < basisBivectorIndex2)
        //    {
        //        if (basisVectorIndex == basisBivectorIndex1)
        //            return EGaSignedBasisBlade.CreatePositiveVector(basisBivectorIndex2);

        //        if (basisVectorIndex == basisBivectorIndex2)
        //            return EGaSignedBasisBlade.CreateNegativeVector(basisBivectorIndex1);

        //        if (basisVectorIndex < basisBivectorIndex1)
        //            return EGaSignedBasisBlade.CreatePositive(
        //                GaMetricUtils.CreateBasisTrivector(basisVectorIndex, basisBivectorIndex1, basisBivectorIndex2)
        //            );

        //        if (basisVectorIndex < basisBivectorIndex2)
        //            return EGaSignedBasisBlade.CreateNegative(
        //                GaMetricUtils.CreateBasisTrivector(basisBivectorIndex1, basisVectorIndex, basisBivectorIndex2)
        //            );

        //        return EGaSignedBasisBlade.CreatePositive(
        //            GaMetricUtils.CreateBasisTrivector(basisBivectorIndex1, basisBivectorIndex2, basisVectorIndex)
        //        );
        //    }
        
        //    if (basisVectorIndex == basisBivectorIndex1)
        //        return EGaSignedBasisBlade.CreatePositiveVector(basisBivectorIndex2);

        //    if (basisVectorIndex == basisBivectorIndex2)
        //        return EGaSignedBasisBlade.CreateNegativeVector(basisBivectorIndex1);

        //    if (basisVectorIndex < basisBivectorIndex2)
        //        return EGaSignedBasisBlade.CreateNegative(
        //            GaMetricUtils.CreateBasisTrivector(basisVectorIndex, basisBivectorIndex2, basisBivectorIndex1)
        //        );

        //    if (basisVectorIndex < basisBivectorIndex1)
        //        return EGaSignedBasisBlade.CreatePositive(
        //            GaMetricUtils.CreateBasisTrivector(basisBivectorIndex2, basisVectorIndex, basisBivectorIndex1)
        //        );

        //    return EGaSignedBasisBlade.CreateNegative(
        //        GaMetricUtils.CreateBasisTrivector(basisBivectorIndex2, basisBivectorIndex1, basisVectorIndex)
        //    );
        //}

        //public static EGaSignedBasisBlade VectorBladeEGp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        //{
        //    Debug.Assert(
        //        IsValidBasisVectorIndex(basisVectorIndex) &&
        //        IsValidBasisBladeIndexSet(basisBladeIndexSet)
        //    );

        //    if (basisBladeIndexSet.Count == 0)
        //        return EGaSignedBasisBlade.CreatePositiveVector(basisVectorIndex);

        //    if (basisBladeIndexSet.Count == 1)
        //        return VectorVectorEGp(basisVectorIndex, basisBladeIndexSet.First());

        //    var basisBladeIndexSet1 = basisBladeIndexSet.ToImmutableSortedSet();
        //    var scalar = 1;
        //    var indexList = new List<int>(basisBladeIndexSet.Count + 1);

        //    var i2 = basisBladeIndexSet1[0];
        //    for (var i = 0; i < basisBladeIndexSet1.Count; i++)
        //    {
        //        var index2 = basisBladeIndexSet1[i];

        //        if (i > 0 && index2 <= i2)
        //            throw new ArgumentException();

        //        i2 = index2;

        //        Debug.Assert(
        //            index2 >= 0
        //        );

        //        if (basisVectorIndex > index2)
        //        {
        //            indexList.Add(index2);
        //            scalar = -scalar;

        //            if (i == basisBladeIndexSet1.Count - 1)
        //            {
        //                indexList.Add(basisVectorIndex);
        //                break;
        //            }

        //            continue;
        //        }

        //        if (basisVectorIndex < index2)
        //        {
        //            indexList.Add(basisVectorIndex);
        //            indexList.Add(index2);
        //        }

        //        for (var j = i + 1; j < basisBladeIndexSet1.Count; j++)
        //            indexList.Add(basisBladeIndexSet1[j]);

        //        break;
        //    }

        //    return indexList.ToEGaSignedBasisBlade(scalar);
        //}

        //public static EGaSignedBasisBlade BivectorVectorEGp(int basisBivectorIndex1, int basisBivectorIndex2, int basisVectorIndex)
        //{
        //    Debug.Assert(
        //        IsValidBasisVectorIndex(basisVectorIndex) &&
        //        IsValidBasisVectorIndex(basisBivectorIndex1) &&
        //        IsValidBasisVectorIndex(basisBivectorIndex2)
        //    );

        //    if (basisBivectorIndex1 == basisBivectorIndex2)
        //        return VectorVectorEGp(basisBivectorIndex1, basisVectorIndex);

        //    if (basisBivectorIndex1 < basisBivectorIndex2)
        //    {
        //        if (basisVectorIndex == basisBivectorIndex2)
        //            return EGaSignedBasisBlade.CreatePositiveVector(basisBivectorIndex1);

        //        if (basisVectorIndex == basisBivectorIndex1)
        //            return EGaSignedBasisBlade.CreateNegativeVector(basisBivectorIndex2);

        //        if (basisVectorIndex > basisBivectorIndex2)
        //            return EGaSignedBasisBlade.CreatePositive(
        //                GaMetricUtils.CreateBasisTrivector(basisBivectorIndex1, basisBivectorIndex2, basisVectorIndex)
        //            );

        //        if (basisVectorIndex > basisBivectorIndex1)
        //            return EGaSignedBasisBlade.CreateNegative(
        //                GaMetricUtils.CreateBasisTrivector(basisBivectorIndex1, basisVectorIndex, basisBivectorIndex2)
        //            );

        //        return EGaSignedBasisBlade.CreatePositive(
        //            GaMetricUtils.CreateBasisTrivector(basisVectorIndex, basisBivectorIndex1, basisBivectorIndex2)
        //        );
        //    }

        //    if (basisVectorIndex == basisBivectorIndex2)
        //        return EGaSignedBasisBlade.CreatePositiveVector(basisBivectorIndex1);

        //    if (basisVectorIndex == basisBivectorIndex1)
        //        return EGaSignedBasisBlade.CreateNegativeVector(basisBivectorIndex2);

        //    if (basisVectorIndex < basisBivectorIndex2)
        //        return EGaSignedBasisBlade.CreateNegative(
        //            GaMetricUtils.CreateBasisTrivector(basisVectorIndex, basisBivectorIndex2, basisBivectorIndex1)
        //        );

        //    if (basisVectorIndex < basisBivectorIndex1)
        //        return EGaSignedBasisBlade.CreatePositive(
        //            GaMetricUtils.CreateBasisTrivector(basisBivectorIndex2, basisVectorIndex, basisBivectorIndex1)
        //        );

        //    return EGaSignedBasisBlade.CreateNegative(
        //        GaMetricUtils.CreateBasisTrivector(basisBivectorIndex2, basisBivectorIndex1, basisVectorIndex)
        //    );
        //}

        //public static EGaSignedBasisBlade BladeVectorEGp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        //{
        //    Debug.Assert(
        //        IsValidBasisVectorIndex(basisVectorIndex) &&
        //        IsValidBasisBladeIndexSet(basisBladeIndexSet)
        //    );

        //    if (basisBladeIndexSet.Count == 0)
        //        return EGaSignedBasisBlade.CreatePositiveVector(basisVectorIndex);

        //    if (basisBladeIndexSet.Count == 1)
        //        return VectorVectorEGp(basisBladeIndexSet.First(), basisVectorIndex);

        //    var basisBladeIndexSet1 = basisBladeIndexSet.ToImmutableSortedSet();
        //    var scalar = 1;
        //    var indexList = new List<int>(basisBladeIndexSet1.Count + 1);
        
        //    for (var i = basisBladeIndexSet1.Count - 1; i >= 0; i--)
        //    {
        //        var index1 = basisBladeIndexSet1[i];
            
        //        Debug.Assert(
        //            index1 >= 0
        //        );

        //        if (index1 > basisVectorIndex)
        //        {
        //            indexList.Add(index1);
        //            scalar = -scalar;

        //            if (i == 0)
        //            {
        //                indexList.Add(basisVectorIndex);
        //                break;
        //            }

        //            continue;
        //        }

        //        if (index1 < basisVectorIndex)
        //        {
        //            indexList.Add(index1);
        //            indexList.Add(basisVectorIndex);
        //        }

        //        for (var j = i - 1; j >= 0; j--)
        //            indexList.Add(basisBladeIndexSet1[j]);

        //        break;
        //    }

        //    return indexList.ToEGaSignedBasisBlade(scalar);
        //}

        //public static EGaSignedBasisBlade BladeBladeEGp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        //{
        //    if (basisBlade1IndexSet.Count == 0)
        //        return basisBlade2IndexSet.ToEGaSignedBasisBlade(true);

        //    if (basisBlade2IndexSet.Count == 0)
        //        return basisBlade1IndexSet.ToEGaSignedBasisBlade(true);

        //    if (basisBlade1IndexSet.Count == 1)
        //        return VectorBladeEGp(basisBlade1IndexSet.First(), basisBlade2IndexSet);

        //    if (basisBlade2IndexSet.Count == 1)
        //        return BladeVectorEGp(basisBlade1IndexSet, basisBlade2IndexSet.First());

        //    var scalar = 1;
        //    var basisBlade = 
        //        basisBlade1IndexSet.Count > basisBlade2IndexSet.Count
        //            ? basisBlade1IndexSet 
        //            : basisBlade2IndexSet;

        //    if (basisBlade1IndexSet.Count > basisBlade2IndexSet.Count)
        //    {
        //        foreach (var index2 in basisBlade2IndexSet)
        //        {
        //            var term = BladeVectorEGp(basisBlade, index2);

        //            scalar *= term.Sign;
        //            basisBlade = term.BasisBlade;
        //        }
        //    }
        //    else
        //    {
        //        foreach (var index1 in basisBlade1IndexSet.Reverse())
        //        {
        //            var term = VectorBladeEGp(index1, basisBlade);

        //            scalar *= term.Sign;
        //            basisBlade = term.BasisBlade;
        //        }
        //    }

        //    return basisBlade.ToEGaSignedBasisBlade(scalar, true);
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorVectorOp(int basisVector1Index, int basisVector2Index)
        //{
        //    return IsZeroVectorVectorOp(basisVector1Index, basisVector2Index)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorVectorEGp(basisVector1Index, basisVector2Index);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeVectorOp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        //{
        //    return IsZeroBladeVectorOp(basisBladeIndexSet, basisVectorIndex)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeVectorEGp(basisBladeIndexSet, basisVectorIndex);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorBladeOp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        //{
        //    return IsZeroVectorBladeOp(basisVectorIndex, basisBladeIndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorBladeEGp(basisVectorIndex, basisBladeIndexSet);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeBladeOp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        //{
        //    return IsZeroBladeBladeOp(basisBlade1IndexSet, basisBlade2IndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeBladeEGp(basisBlade1IndexSet, basisBlade2IndexSet);
        //}

    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorVectorESp(int basisVector1Index, int basisVector2Index)
        //{
        //    return IsZeroVectorVectorESp(basisVector1Index, basisVector2Index)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorVectorEGp(basisVector1Index, basisVector2Index);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeVectorESp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        //{
        //    return IsZeroBladeVectorESp(basisBladeIndexSet, basisVectorIndex)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeVectorEGp(basisBladeIndexSet, basisVectorIndex);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorBladeESp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        //{
        //    return IsZeroVectorBladeESp(basisVectorIndex, basisBladeIndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorBladeEGp(basisVectorIndex, basisBladeIndexSet);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeBladeESp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        //{
        //    return IsZeroBladeBladeESp(basisBlade1IndexSet, basisBlade2IndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeBladeEGp(basisBlade1IndexSet, basisBlade2IndexSet);
        //}
    
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorVectorELcp(int basisVector1Index, int basisVector2Index)
        //{
        //    return IsZeroVectorVectorELcp(basisVector1Index, basisVector2Index)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorVectorEGp(basisVector1Index, basisVector2Index);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeVectorELcp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        //{
        //    return IsZeroBladeVectorELcp(basisBladeIndexSet, basisVectorIndex)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeVectorEGp(basisBladeIndexSet, basisVectorIndex);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorBladeELcp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        //{
        //    return IsZeroVectorBladeELcp(basisVectorIndex, basisBladeIndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorBladeEGp(basisVectorIndex, basisBladeIndexSet);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeBladeELcp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        //{
        //    return IsZeroBladeBladeELcp(basisBlade1IndexSet, basisBlade2IndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeBladeEGp(basisBlade1IndexSet, basisBlade2IndexSet);
        //}
    
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorVectorERcp(int basisVector1Index, int basisVector2Index)
        //{
        //    return IsZeroVectorVectorERcp(basisVector1Index, basisVector2Index)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorVectorEGp(basisVector1Index, basisVector2Index);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeVectorERcp(IReadOnlySet<int> basisBladeIndexSet, int basisVectorIndex)
        //{
        //    return IsZeroBladeVectorERcp(basisBladeIndexSet, basisVectorIndex)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeVectorEGp(basisBladeIndexSet, basisVectorIndex);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade VectorBladeERcp(int basisVectorIndex, IReadOnlySet<int> basisBladeIndexSet)
        //{
        //    return IsZeroVectorBladeERcp(basisVectorIndex, basisBladeIndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : VectorBladeEGp(basisVectorIndex, basisBladeIndexSet);
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static EGaSignedBasisBlade BladeBladeERcp(IReadOnlySet<int> basisBlade1IndexSet, IReadOnlySet<int> basisBlade2IndexSet)
        //{
        //    return IsZeroBladeBladeERcp(basisBlade1IndexSet, basisBlade2IndexSet)
        //        ? EGaSignedBasisBlade.CreateZeroScalar()
        //        : BladeBladeEGp(basisBlade1IndexSet, basisBlade2IndexSet);
        //}
      
    }
}
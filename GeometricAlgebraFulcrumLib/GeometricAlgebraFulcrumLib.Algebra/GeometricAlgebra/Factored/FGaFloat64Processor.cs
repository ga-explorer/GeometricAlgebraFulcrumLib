using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Factored
{
    public class FGaFloat64Processor : 
        XGaMetric
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FGaFloat64Processor Euclidean()
        {
            return new FGaFloat64Processor(0, 0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FGaFloat64Processor(int negativeCount, int zeroCount)
            : base(negativeCount, zeroCount)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign VectorSignature(int i)
        {
            if (i >= NonEuclideanBasisCount)
                return IntegerSign.Positive;

            if (i < NegativeSignatureBasisCount)
                return IntegerSign.Negative;

            return IntegerSign.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign BivectorSignature(int i, int j)
        {
            if (IsEuclidean)
                return IntegerSign.Positive;

            var isNegative = false;
            
            if (i >= NegativeSignatureBasisCount)
                return i >= NonEuclideanBasisCount
                    ? isNegative
                        ? IntegerSign.Negative
                        : IntegerSign.Positive
                    : IntegerSign.Zero;

            isNegative = !isNegative;
            
            if (j >= NegativeSignatureBasisCount)
                return j >= NonEuclideanBasisCount
                    ? isNegative
                        ? IntegerSign.Negative
                        : IntegerSign.Positive
                    : IntegerSign.Zero;

            isNegative = !isNegative;

            return isNegative
                ? IntegerSign.Negative
                : IntegerSign.Positive;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign TrivectorSignature(int i, int j, int k)
        {
            if (IsEuclidean)
                return IntegerSign.Positive;

            var isNegative = false;
            
            if (i >= NegativeSignatureBasisCount)
                return i >= NonEuclideanBasisCount
                    ? isNegative
                        ? IntegerSign.Negative
                        : IntegerSign.Positive
                    : IntegerSign.Zero;

            isNegative = !isNegative;
            
            if (j >= NegativeSignatureBasisCount)
                return j >= NonEuclideanBasisCount
                    ? isNegative
                        ? IntegerSign.Negative
                        : IntegerSign.Positive
                    : IntegerSign.Zero;

            isNegative = !isNegative;
            
            if (k >= NegativeSignatureBasisCount)
                return k >= NonEuclideanBasisCount
                    ? isNegative
                        ? IntegerSign.Negative
                        : IntegerSign.Positive
                    : IntegerSign.Zero;

            isNegative = !isNegative;

            return isNegative
                ? IntegerSign.Negative
                : IntegerSign.Positive;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int[] MeetVectorVector(int i, int j)
        {
            Debug.Assert(i >= 0 && j >= 0);

            return i == j ? [i] : [];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int[] MeetVectorBivector(int i, int j1, int j2)
        {
            Debug.Assert(i >= 0 && j1 >= 0 && j1 < j2);

            return i == j1 || i == j2 ? [i] : [];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int[] MeetVectorTrivector(int i, int j1, int j2, int j3)
        {
            Debug.Assert(i >= 0 && j1 >= 0 && j1 < j2 && j2 < j3);

            return i == j1 || i == j2 || i == j3 ? [i] : [];
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureVectorVector(int i, int j)
        {
            Debug.Assert(i >= 0 && j >= 0);

            if (IsEuclidean) return IntegerSign.Positive;

            return i == j 
                ? VectorSignature(i) 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureVectorBivector(int i, int j1, int j2)
        {
            Debug.Assert(i >= 0 && j1 >= 0 && j1 < j2);

            if (IsEuclidean) return IntegerSign.Positive;

            return i == j1 || i == j2 
                ? VectorSignature(i) 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureVectorTrivector(int i, int j1, int j2, int j3)
        {
            Debug.Assert(i >= 0 && j1 >= 0 && j1 < j2);

            if (IsEuclidean) return IntegerSign.Positive;

            return i == j1 || i == j2 || i == j3
                ? VectorSignature(i) 
                : IntegerSign.Positive;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureBivectorVector(int i1, int i2, int j)
        {
            Debug.Assert(i1 >= 0 && j >= 0 && i1 < i2);

            if (IsEuclidean) return IntegerSign.Positive;

            return i1 == j || i2 == j 
                ? VectorSignature(j) 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureBivectorBivector(int i1, int i2, int j1, int j2)
        {
            Debug.Assert(i1 >= 0 && j1 >= 0 && i1 < i2 && j1 < j2);

            if (IsEuclidean) return IntegerSign.Positive;

            if (i1 == j1)
                return i2 == j2 
                    ? BivectorSignature(i1, i2) 
                    : VectorSignature(i1);

            if (i1 == j2)
                return VectorSignature(i1);

            return i2 == j1 || i2 == j2 
                ? VectorSignature(i2) 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureBivectorTrivector(int i1, int i2, int j1, int j2, int j3)
        {
            Debug.Assert(i1 >= 0 && j1 >= 0 && i1 < i2 && j1 < j2 && j2 < j3);

            if (IsEuclidean) return IntegerSign.Positive;

            if (i1 == j1)
                return i2 == j2 || i2 == j3
                    ? BivectorSignature(i1, i2) 
                    : VectorSignature(i1);

            if (i1 == j2)
                return i2 == j3
                    ? BivectorSignature(i1, i2) 
                    : VectorSignature(i1);

            return i2 == j1 || i2 == j2 || i2 == j3
                ? VectorSignature(i2) 
                : IntegerSign.Positive;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureTrivectorVector(int i1, int i2, int i3, int j)
        {
            Debug.Assert(i1 >= 0 && j >= 0 && i1 < i2 && i2 < i3);

            if (IsEuclidean) return IntegerSign.Positive;

            return i1 == j || i2 == j || i3 == j
                ? VectorSignature(j) 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureTrivectorBivector(int i1, int i2, int i3, int j1, int j2)
        {
            Debug.Assert(i1 >= 0 && j1 >= 0 && i1 < i2 && i2 < i3 && j1 < j2);

            if (IsEuclidean) return IntegerSign.Positive;

            if (i1 == j1)
                return i2 == j2 || i3 == j2
                    ? BivectorSignature(i1, j2) 
                    : VectorSignature(i1);
            
            if (i1 == j2)
                return i3 == j2
                    ? BivectorSignature(i1, i3) 
                    : VectorSignature(i1);
            
            if (i2 == j1)
                return i3 == j2
                    ? BivectorSignature(i2, i3) 
                    : VectorSignature(i2);
            
            if (i2 == j2)
                return VectorSignature(i2);
            
            return i3 == j1 || i3 == j2
                ? VectorSignature(i3) 
                : IntegerSign.Positive;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntegerSign MeetSignatureTrivectorTrivector(int i1, int i2, int i3, int j1, int j2, int j3)
        {
            Debug.Assert(i1 >= 0 && j1 >= 0 && i1 < i2 && i2 < i3 && j1 < j2 && j2 < j3);

            if (IsEuclidean) return IntegerSign.Positive;

            if (i1 == j1)
            {
                if (i2 == j2)
                    return i3 == j3
                        ? TrivectorSignature(i1, i2, i3)
                        : BivectorSignature(i1, i2);
                
                if (i2 == j3)
                    return BivectorSignature(i1, i2);
                
                return i3 == j2 || i3 == j3
                    ? BivectorSignature(i1, i3) 
                    : VectorSignature(i1);
            }

            if (i1 == j2)
            {
                if (i2 == j3)
                    return BivectorSignature(i1, i2);
                
                return i3 == j3
                    ? BivectorSignature(i1, i3) 
                    : VectorSignature(i1);
            }

            if (i1 == j3)
                return VectorSignature(i1);

            if (i2 == j1)
                return i3 == j2 || i3 == j3 
                    ? BivectorSignature(i2, i3) 
                    : VectorSignature(i2);

            if (i2 == j2)
                return i3 == j3 
                    ? BivectorSignature(i2, i3) 
                    : VectorSignature(i2);

            if (i2 == j3)
                return VectorSignature(i2);
            
            return i3 == j1 || i3 == j2 || i3 == j3 
                ? VectorSignature(i3) 
                : IntegerSign.Positive;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (IntegerSign sign, SmallIndexSet id) EGpVectorVector(int i, int j)
        {
            if (i == j)
                return (IntegerSign.Positive, SmallIndexSet.EmptySet);

            return i < j 
                ? (IntegerSign.Positive, SmallIndexSet.Create(i, j)) 
                : (IntegerSign.Negative, SmallIndexSet.Create(j, i));
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public IXGaSignedBasisBlade EGp(SmallIndexSet basisBladeId1, SmallIndexSet basisBladeId2)
        //{
        //    var (swapCount, id) = 
        //        basisBladeId1.SetMergeCountSwaps(basisBladeId2);

        //    var basisBlade = new XGaBasisBlade(this, id);

        //    return swapCount.IsOdd()
        //        ? new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative)
        //        : basisBlade;
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public IXGaSignedBasisBlade Gp(SmallIndexSet basisBladeId1, SmallIndexSet basisBladeId2)
        //{
        //    if (IsEuclidean)
        //        return EGp(basisBladeId1, basisBladeId2);

        //    var (swapCount, id, meetSet) = 
        //        basisBladeId1.SetMergeCountSwapsTrackCommon(basisBladeId2);

        //    var meetBasisBladeSignature = 
        //        Signature(meetSet);
        
        //    if (meetBasisBladeSignature.IsZero)
        //        return ZeroBasisScalar;

        //    var basisBlade = new XGaBasisBlade(this, id);

        //    return swapCount.IsOdd() == meetBasisBladeSignature.IsNegative
        //        ? basisBlade
        //        : new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
        //}

    }
}

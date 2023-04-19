using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis
{
    public static class RGaBasisBladeComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisScalar(this RGaMetric metric)
        {
            return metric.BasisScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisVector(this RGaMetric metric, int index)
        {
            return new RGaBasisBlade(
                metric, 
                1UL << index
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBivector(this RGaMetric metric, int index1, int index2)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();

            return new RGaBasisBlade(
                metric, 
                BasisBivectorUtils.IndexPairToBivectorId(index1, index2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBivector(this RGaMetric metric, IPair<int> indexPair)
        {
            return metric.CreateBasisBivector(indexPair.Item1, indexPair.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisTrivector(this RGaMetric metric, int index1, int index2, int index3)
        {
            if (index1 >= index2 || index2 >= index3)
                throw new InvalidOperationException();

            return new RGaBasisBlade(
                metric, 
                (1UL << index1) | (1UL << index2) | (1UL << index3)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBlade(this RGaMetric metric, ImmutableSortedSet<int> basisVectorIndexSet)
        {
            return new RGaBasisBlade(
                metric, 
                basisVectorIndexSet.Aggregate(0UL, (a, b) => a | (1UL << b))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBlade(this RGaMetric metric, ulong basisBladeId)
        {
            return new RGaBasisBlade(metric, basisBladeId);
        }
      
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBlade(this RGaMetric metric, int grade, int index)
        {
            var id = BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, (ulong) index);

            return new RGaBasisBlade(metric, id);
        }
          
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBlade(this RGaMetric metric, int grade, ulong index)
        {
            var id = BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

            return new RGaBasisBlade(metric, id);
        }
       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisBlade(this RGaMetric metric, uint grade, ulong index)
        {
            var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

            return new RGaBasisBlade(metric, id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaBasisBlade CreateBasisPseudoScalar(this RGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return new RGaBasisBlade(metric, id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade CreateBasisPseudoScalar(this RGaMetric metric, int vSpaceDimensions, IntegerSign sign)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return sign.IsPositive
                ? new RGaBasisBlade(metric, id)
                : new RGaSignedBasisBlade(metric, id, sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade CreateBasisPseudoScalarReverse(this RGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return vSpaceDimensions.ReverseIsNegativeOfGrade()
                ? new RGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new RGaBasisBlade(metric, id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade CreateBasisPseudoScalarConjugate(this RGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);
            var sign = metric.ConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            return sign.IsNegative
                ? new RGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new RGaBasisBlade(metric, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade CreateBasisPseudoScalarEInverse(this RGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);
            var sign = metric.EGpSquaredSign(id);
        
            return sign.IsNegative
                ? new RGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new RGaBasisBlade(metric, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade CreateBasisPseudoScalarInverse(this RGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);
            var sign = metric.GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            return sign.IsNegative
                ? new RGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new RGaBasisBlade(metric, id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaBasisBlade> GetBasisVectors(this RGaMetric metric, int vSpaceDimensions)
        {
            return metric
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => new RGaBasisBlade(metric, id));
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaBasisBlade> GetBasisBivectors(this RGaMetric metric, int vSpaceDimensions)
        {
            return metric
                .GetBasisBivectorIds(vSpaceDimensions)
                .Select(id => new RGaBasisBlade(metric, id));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaBasisBlade> GetBasisKVectors(this RGaMetric metric, int vSpaceDimensions, int grade)
        {
            return metric
                .GetBasisKVectorIds(vSpaceDimensions, grade)
                .Select(id => new RGaBasisBlade(metric, id));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaBasisBlade> GetBasisBlades(this RGaMetric metric, int vSpaceDimensions)
        {
            return metric
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => new RGaBasisBlade(metric, id));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade EGp(this RGaMetric metric, int index1, int index2)
        {
            return metric.CreateBasisVector(index1).EGp(
                metric.CreateBasisVector(index2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaSignedBasisBlade Gp(this RGaMetric metric, int index1, int index2)
        {
            return metric.CreateBasisVector(index1).Gp(
                metric.CreateBasisVector(index2)
            );
        }

        public static IRGaSignedBasisBlade EGp(this RGaMetric metric, params int[] indexList)
        {
            IRGaSignedBasisBlade basisBlade = 
                metric.CreateBasisVector(indexList[0]);

            foreach (var index in indexList.Skip(1))
            {
                basisBlade = basisBlade.EGp(
                    metric.CreateBasisVector(index)
                );

                //if (basisBlade.IsZero) 
                //    return metric.ZeroBasisScalar;
            }
        
            return basisBlade;
        }
    
        public static IRGaSignedBasisBlade Gp(this RGaMetric metric, params int[] indexList)
        {
            IRGaSignedBasisBlade basisBlade = 
                metric.CreateBasisVector(indexList[0]);

            foreach (var index in indexList.Skip(1))
            {
                basisBlade = basisBlade.Gp(
                    metric.CreateBasisVector(index)
                );

                if (basisBlade.IsZero) 
                    return metric.ZeroBasisScalar;
            }
        
            return basisBlade;
        }
    
        public static IRGaSignedBasisBlade EGp(this RGaMetric metric, IReadOnlyList<int> indexList)
        {
            IRGaSignedBasisBlade basisBlade = 
                metric.CreateBasisVector(indexList[0]);

            foreach (var index in indexList.Skip(1))
            {
                basisBlade = basisBlade.EGp(
                    metric.CreateBasisVector(index)
                );

                //if (basisBlade.IsZero) 
                //    return metric.ZeroBasisScalar;
            }
        
            return basisBlade;
        }

        public static IRGaSignedBasisBlade Gp(this RGaMetric metric, IReadOnlyList<int> indexList)
        {
            IRGaSignedBasisBlade basisBlade = 
                metric.CreateBasisVector(indexList[0]);

            foreach (var index in indexList.Skip(1))
            {
                basisBlade = basisBlade.Gp(
                    metric.CreateBasisVector(index)
                );

                if (basisBlade.IsZero) 
                    return metric.ZeroBasisScalar;
            }
        
            return basisBlade;
        }
    }
}
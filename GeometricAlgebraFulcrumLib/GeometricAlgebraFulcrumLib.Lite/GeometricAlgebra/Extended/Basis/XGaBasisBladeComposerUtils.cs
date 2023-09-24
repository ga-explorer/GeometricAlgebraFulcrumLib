using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis
{
    public static class XGaBasisBladeComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisScalar(this XGaMetric metric)
        {
            return metric.BasisScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisVector(this XGaMetric metric, int index)
        {
            return new XGaBasisBlade(
                metric, 
                index.IndexToIndexSet()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBivector(this XGaMetric metric, int index1, int index2)
        {
            if (index1 >= index2)
                throw new InvalidOperationException();

            return new XGaBasisBlade(
                metric, 
                IndexSetUtils.IndexPairToIndexSet(index1, index2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBivector(this XGaMetric metric, IPair<int> indexPair)
        {
            return metric.CreateBasisBivector(indexPair.Item1, indexPair.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisTrivector(this XGaMetric metric, int index1, int index2, int index3)
        {
            if (index1 >= index2 || index2 >= index3)
                throw new InvalidOperationException();

            return new XGaBasisBlade(
                metric, 
                ImmutableSortedSet.Create(index1, index2, index3).ToIndexSet()
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBlade(this XGaMetric metric, IIndexSet basisVectorIndexSet)
        {
            return new XGaBasisBlade(
                metric, 
                basisVectorIndexSet
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBlade(this XGaMetric metric, ImmutableSortedSet<int> basisVectorIndexSet)
        {
            return new XGaBasisBlade(
                metric, 
                basisVectorIndexSet.ToIndexSet()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBlade(this XGaMetric metric, ulong basisBladeId)
        {
            var indexSet = basisBladeId.BitPatternToUInt64IndexSet();

            return new XGaBasisBlade(metric, indexSet);
        }
      
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBlade(this XGaMetric metric, int grade, int index)
        {
            var indexSet = 
                BasisBladeUtils
                    .BasisBladeGradeIndexToId((uint) grade, (ulong) index)
                    .BitPatternToUInt64IndexSet();

            return new XGaBasisBlade(metric, indexSet);
        }
          
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBlade(this XGaMetric metric, int grade, ulong index)
        {
            var indexSet = 
                BasisBladeUtils
                    .BasisBladeGradeIndexToId((uint) grade, index)
                    .BitPatternToUInt64IndexSet();

            return new XGaBasisBlade(metric, indexSet);
        }
       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisBlade(this XGaMetric metric, uint grade, ulong index)
        {
            var indexSet = 
                BasisBladeUtils
                    .BasisBladeGradeIndexToId(grade, index)
                    .BitPatternToUInt64IndexSet();

            return new XGaBasisBlade(metric, indexSet);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaBasisBlade CreateBasisPseudoScalar(this XGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return new XGaBasisBlade(metric, id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade CreateBasisPseudoScalar(this XGaMetric metric, int vSpaceDimensions, IntegerSign sign)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return sign.IsPositive
                ? new XGaBasisBlade(metric, id)
                : new XGaSignedBasisBlade(metric, id, sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade CreateBasisPseudoScalarReverse(this XGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);

            return vSpaceDimensions.ReverseIsNegativeOfGrade()
                ? new XGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new XGaBasisBlade(metric, id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade CreateBasisPseudoScalarConjugate(this XGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);
            var sign = metric.ConjugateSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            return sign.IsNegative
                ? new XGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new XGaBasisBlade(metric, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade CreateBasisPseudoScalarEInverse(this XGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);
            var sign = metric.EGpSquaredSign(id);
        
            return sign.IsNegative
                ? new XGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new XGaBasisBlade(metric, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade CreateBasisPseudoScalarInverse(this XGaMetric metric, int vSpaceDimensions)
        {
            var id = metric.GetBasisPseudoScalarId(vSpaceDimensions);
            var sign = metric.GpSquaredSign(id);

            if (sign.IsZero)
                throw new DivideByZeroException();

            return sign.IsNegative
                ? new XGaSignedBasisBlade(metric, id, IntegerSign.Negative)
                : new XGaBasisBlade(metric, id);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaBasisBlade> GetBasisVectors(this XGaMetric metric, int vSpaceDimensions)
        {
            return metric
                .GetBasisVectorIds(vSpaceDimensions)
                .Select(id => new XGaBasisBlade(metric, id));
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaBasisBlade> GetBasisBivectors(this XGaMetric metric, int vSpaceDimensions)
        {
            return metric
                .GetBasisBivectorIds(vSpaceDimensions)
                .Select(id => new XGaBasisBlade(metric, id));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaBasisBlade> GetBasisKVectors(this XGaMetric metric, int vSpaceDimensions, int grade)
        {
            return metric
                .GetBasisKVectorIds(vSpaceDimensions, grade)
                .Select(id => new XGaBasisBlade(metric, id));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaBasisBlade> GetBasisBlades(this XGaMetric metric, int vSpaceDimensions)
        {
            return metric
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => new XGaBasisBlade(metric, id));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade EGp(this XGaMetric metric, int index1, int index2)
        {
            return metric.CreateBasisVector(index1).EGp(
                metric.CreateBasisVector(index2)
            );
        }
    
        public static IXGaSignedBasisBlade EGp(this XGaMetric metric, params int[] indexList)
        {
            IXGaSignedBasisBlade basisBlade = 
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
    
        public static IXGaSignedBasisBlade EGp(this XGaMetric metric, IReadOnlyList<int> indexList)
        {
            IXGaSignedBasisBlade basisBlade = 
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
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade Gp(this XGaMetric metric, int index1, int index2)
        {
            return metric.CreateBasisVector(index1).Gp(
                metric.CreateBasisVector(index2)
            );
        }

        public static IXGaSignedBasisBlade Gp(this XGaMetric metric, params int[] indexList)
        {
            IXGaSignedBasisBlade basisBlade = 
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

        public static IXGaSignedBasisBlade Gp(this XGaMetric metric, IReadOnlyList<int> indexList)
        {
            IXGaSignedBasisBlade basisBlade = 
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
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IXGaSignedBasisBlade Op(this XGaMetric metric, int index1, int index2)
        {
            return metric.CreateBasisVector(index1).Op(
                metric.CreateBasisVector(index2)
            );
        }

        public static IXGaSignedBasisBlade Op(this XGaMetric metric, params int[] indexList)
        {
            IXGaSignedBasisBlade basisBlade = 
                metric.CreateBasisVector(indexList[0]);

            foreach (var index in indexList.Skip(1))
            {
                basisBlade = basisBlade.Op(
                    metric.CreateBasisVector(index)
                );

                if (basisBlade.IsZero) 
                    return metric.ZeroBasisScalar;
            }
        
            return basisBlade;
        }

        public static IXGaSignedBasisBlade Op(this XGaMetric metric, IReadOnlyList<int> indexList)
        {
            IXGaSignedBasisBlade basisBlade = 
                metric.CreateBasisVector(indexList[0]);

            foreach (var index in indexList.Skip(1))
            {
                basisBlade = basisBlade.Op(
                    metric.CreateBasisVector(index)
                );

                if (basisBlade.IsZero) 
                    return metric.ZeroBasisScalar;
            }
            
            return basisBlade;
        }
    }
}
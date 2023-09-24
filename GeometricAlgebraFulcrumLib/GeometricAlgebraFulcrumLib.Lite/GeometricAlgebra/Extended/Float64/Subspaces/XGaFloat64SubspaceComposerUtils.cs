using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Subspaces
{
    public static class XGaFloat64SubspaceComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreateScalarSubspace(this XGaFloat64Processor metric)
        {
            return new XGaFloat64Subspace(
                metric.CreateScalar(1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreateVectorSubspace(this XGaFloat64Processor metric, int index)
        {
            return new XGaFloat64Subspace(
                metric.CreateTermVector(index, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreateBivectorSubspace(this XGaFloat64Processor metric, ulong index)
        {
            var id = 
                index.BasisBivectorIndexToId();

            return new XGaFloat64Subspace(
                metric.CreateTermKVector(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreateBivectorSubspace(this XGaFloat64Processor metric, int index1, int index2)
        {
            var id =
                IndexSetUtils.IndexPairToUInt64IndexSetBitPattern(index1, index2);

            return new XGaFloat64Subspace(
                metric.CreateTermKVector(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreateSubspace(this XGaFloat64Processor metric, ulong id)
        {
            return new XGaFloat64Subspace(
                metric.CreateTermKVector(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreateSubspace(this XGaFloat64Processor metric, int grade, ulong index)
        {
            var id = 
                BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

            return new XGaFloat64Subspace(
                metric.CreateTermKVector(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace CreatePseudoScalarSubspace(this XGaFloat64Processor metric, int vSpaceDimensions)
        {
            return new XGaFloat64Subspace(
                metric.CreatePseudoScalar(vSpaceDimensions)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace ToSubspace(this XGaFloat64KVector blade)
        {
            return new XGaFloat64Subspace(blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace DualToSubspace(this XGaFloat64KVector blade, int vSpaceDimensions)
        {
            return new XGaFloat64Subspace(
                blade.Dual(vSpaceDimensions)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Subspace UnDualToSubspace(this XGaFloat64KVector blade, int vSpaceDimensions)
        {
            return new XGaFloat64Subspace(
                blade.UnDual(vSpaceDimensions)
            );
        }


    }
}
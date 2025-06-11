using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors
{
    public partial class XGaFloat64Processor
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreateScalarSubspace()
        {
            return new XGaFloat64Subspace(
                ScalarOne
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreateVectorSubspace(int index)
        {
            return new XGaFloat64Subspace(
                VectorTerm(index, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreateBivectorSubspace(ulong index)
        {
            var id = 
                index.BasisBivectorIndexToId();

            return new XGaFloat64Subspace(
                KVectorTerm(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreateBivectorSubspace(int index1, int index2)
        {
            var id =
                IndexSetUtils.IndexPairToUInt64IndexSetBitPattern(index1, index2);

            return new XGaFloat64Subspace(
                KVectorTerm(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreateSubspace(ulong id)
        {
            return new XGaFloat64Subspace(
                KVectorTerm(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreateSubspace(int grade, ulong index)
        {
            var id = 
                BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

            return new XGaFloat64Subspace(
                KVectorTerm(id, 1d)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Subspace CreatePseudoScalarSubspace(int vSpaceDimensions)
        {
            return new XGaFloat64Subspace(
                PseudoScalar(vSpaceDimensions)
            );
        }

    }
}

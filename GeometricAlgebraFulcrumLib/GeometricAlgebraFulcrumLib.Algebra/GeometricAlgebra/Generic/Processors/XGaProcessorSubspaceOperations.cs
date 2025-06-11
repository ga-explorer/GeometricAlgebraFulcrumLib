using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors
{
    public partial class XGaProcessor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateScalarSubspace()
        {
            return new XGaSubspace<T>(
                Scalar(ScalarProcessor.OneValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateVectorSubspace(int index)
        {
            return new XGaSubspace<T>(
                VectorTerm(index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateBivectorSubspace(ulong index)
        {
            var id =
                index.BasisBivectorIndexToId();

            return new XGaSubspace<T>(
                KVectorTerm(
                    id,
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateBivectorSubspace(int index1, int index2)
        {
            var id =
                IndexSet.CreatePair(index1, index2);

            return new XGaSubspace<T>(
                KVectorTerm(
                    id,
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateSubspace(ulong id)
        {
            return new XGaSubspace<T>(
                KVectorTerm(
                    id.ToUInt64IndexSet(),
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateSubspace(int grade, ulong index)
        {
            var id =
                BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, index);

            return new XGaSubspace<T>(
                KVectorTerm(
                    id,
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreateSubspace(IndexSet id)
        {
            return new XGaSubspace<T>(
                KVectorTerm(
                    id,
                    ScalarProcessor.OneValue
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> CreatePseudoScalarSubspace(int vSpaceDimensions)
        {
            return new XGaSubspace<T>(
                PseudoScalar(vSpaceDimensions)
            );
        }

    }
}

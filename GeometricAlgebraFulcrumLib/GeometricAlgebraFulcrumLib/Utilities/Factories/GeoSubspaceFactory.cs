using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GeoSubspaceFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return Subspace<T>.Create(processor, blade.CreateKVector(processor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromDual<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return Subspace<T>.CreateFromDual(processor, blade.CreateKVector(processor));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromBasisVector<T>(this IGeometricAlgebraProcessor<T> processor, ulong index)
        {
            return Subspace<T>.CreateFromBasisVector(processor, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromBasisBivector<T>(this IGeometricAlgebraProcessor<T> processor, ulong index)
        {
            return Subspace<T>.CreateFromBasisBivector(processor, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromBasisBivector<T>(this IGeometricAlgebraProcessor<T> processor, ulong index1, ulong index2)
        {
            return Subspace<T>.CreateFromBasisBivector(processor, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromBasisBlade<T>(this IGeometricAlgebraProcessor<T> processor, ulong id)
        {
            return Subspace<T>.CreateFromBasisBlade(processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromBasisBlade<T>(this IGeometricAlgebraProcessor<T> processor, uint grade, ulong index)
        {
            return Subspace<T>.CreateFromBasisBlade(processor, grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromPseudoScalar<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return Subspace<T>.CreateFromPseudoScalar(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateSubspaceFromPseudoScalar<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return Subspace<T>.CreateFromPseudoScalar(processor, vSpaceDimension);
        }
    }
}
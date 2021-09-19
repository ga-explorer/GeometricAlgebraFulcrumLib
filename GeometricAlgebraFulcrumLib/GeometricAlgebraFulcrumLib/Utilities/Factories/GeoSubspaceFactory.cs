using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GeoSubspaceFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateDirectSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return Subspace<T>.CreateDirect(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateDualSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return Subspace<T>.CreateDual(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateOpnsSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return Subspace<T>.CreateDirect(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreateIpnsSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return Subspace<T>.CreateDual(processor, blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreatePseudoScalarSubspace<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return Subspace<T>.CreateFromPseudoScalar(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Subspace<T> CreatePseudoScalarSubspace<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return Subspace<T>.CreateFromPseudoScalar(processor, vSpaceDimension);
        }
    }
}
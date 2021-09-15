using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GeoSubspaceFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateDirectSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return GeoSubspace<T>.CreateDirect(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateDualSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return GeoSubspace<T>.CreateDual(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateOpnsSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return GeoSubspace<T>.CreateDirect(processor, blade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateIpnsSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return GeoSubspace<T>.CreateDual(processor, blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreatePseudoScalarSubspace<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return GeoSubspace<T>.CreateFromPseudoScalar(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreatePseudoScalarSubspace<T>(this IGeometricAlgebraProcessor<T> processor, uint vSpaceDimension)
        {
            return GeoSubspace<T>.CreateFromPseudoScalar(processor, vSpaceDimension);
        }
    }
}
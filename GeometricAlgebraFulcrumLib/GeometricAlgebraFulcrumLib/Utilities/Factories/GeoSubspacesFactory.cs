using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GeoSubspacesFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return GeoSubspace<T>.CreateDirect(processor, blade);
        }

    }
}
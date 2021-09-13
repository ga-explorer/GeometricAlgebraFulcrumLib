using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GeoSubspacesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GeoSubspace<T> CreateSubspace<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> blade)
        {
            return GeoSubspace<T>.Create(processor, blade);
        }

    }
}
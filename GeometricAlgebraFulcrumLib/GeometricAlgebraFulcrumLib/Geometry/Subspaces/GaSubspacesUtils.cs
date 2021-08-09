using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public static class GaSubspacesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSubspace<T> CreateSubspace<T>(this IGaProcessor<T> processor, IGaStorageKVector<T> blade)
        {
            return GaSubspace<T>.Create(processor, blade);
        }

    }
}
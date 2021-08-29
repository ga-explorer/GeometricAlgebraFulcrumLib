using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaSubspacesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaSubspace<T> CreateSubspace<T>(this IGaProcessor<T> processor, IGaKVectorStorage<T> blade)
        {
            return GaSubspace<T>.Create(processor, blade);
        }

    }
}
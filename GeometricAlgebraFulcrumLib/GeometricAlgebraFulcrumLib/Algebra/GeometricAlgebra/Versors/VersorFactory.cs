using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors
{
    public static class VersorFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureVersorsSequence<T> CreateIdentityVersor<T>(this IGeometricAlgebraProcessor<T> processor)
        {
            return PureVersorsSequence<T>.CreateIdentity(processor);
        }
    }
}
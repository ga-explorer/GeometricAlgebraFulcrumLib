using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
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
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

public static class RGaVersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureVersorsSequence<T> CreateIdentityVersor<T>(this RGaProcessor<T> processor)
    {
        return RGaPureVersorsSequence<T>.CreateIdentity(processor);
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

public static class XGaVersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureVersorsSequence<T> CreateIdentityVersor<T>(this XGaProcessor<T> processor)
    {
        return XGaPureVersorsSequence<T>.CreateIdentity(processor);
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

public static class RGaFloat64VersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureVersorsSequence CreateIdentityVersor(this RGaFloat64Processor metric)
    {
        return RGaFloat64PureVersorsSequence.CreateIdentity(metric);
    }
}
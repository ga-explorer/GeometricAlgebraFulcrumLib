using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Versors;

public static class XGaFloat64VersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureVersorsSequence CreateIdentityVersor(this XGaFloat64Processor metric)
    {
        return XGaFloat64PureVersorsSequence.CreateIdentity(metric);
    }
}
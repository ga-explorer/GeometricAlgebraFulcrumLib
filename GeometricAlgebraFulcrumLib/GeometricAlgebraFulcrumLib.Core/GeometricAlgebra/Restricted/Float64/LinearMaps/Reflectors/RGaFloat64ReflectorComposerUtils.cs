using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors;

public static class RGaFloat64ReflectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureReflector ToPureReflector(this RGaFloat64Vector vector)
    {
        return RGaFloat64PureReflector.Create(vector);
    }
}
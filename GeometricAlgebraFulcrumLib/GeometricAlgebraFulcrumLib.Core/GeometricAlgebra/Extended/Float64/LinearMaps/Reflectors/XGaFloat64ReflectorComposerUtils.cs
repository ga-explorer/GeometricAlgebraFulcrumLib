using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.LinearMaps.Reflectors;

public static class XGaFloat64ReflectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureReflector ToPureReflector(this XGaFloat64Vector vector)
    {
        return XGaFloat64PureReflector.Create(vector);
    }
}
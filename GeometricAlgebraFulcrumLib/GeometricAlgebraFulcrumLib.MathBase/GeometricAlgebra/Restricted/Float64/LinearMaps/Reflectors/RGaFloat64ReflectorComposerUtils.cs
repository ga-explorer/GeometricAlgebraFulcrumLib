using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Reflectors
{
    public static class RGaFloat64ReflectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64PureReflector ToPureReflector(this RGaFloat64Vector vector)
        {
            return RGaFloat64PureReflector.Create(vector);
        }
    }
}
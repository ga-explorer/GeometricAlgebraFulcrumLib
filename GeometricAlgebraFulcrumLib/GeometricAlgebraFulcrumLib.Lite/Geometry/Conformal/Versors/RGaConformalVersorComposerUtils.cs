using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Versors;

public static class RGaConformalVersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaConformalVersor ToConformalCGaVersor(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalVersor(conformalSpace, cgaMultivector);
    }
}
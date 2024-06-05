using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Versors;

public static class RGaConformalVersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaConformalVersor ToConformalCGaVersor(this RGaFloat64Multivector cgaMultivector, RGaConformalSpace conformalSpace)
    {
        return new RGaConformalVersor(conformalSpace, cgaMultivector);
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Versors;

public static class CGaFloat64VersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaFloat64Versor ToConformalCGaVersor(this RGaFloat64Multivector cgaMultivector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return new CGaFloat64Versor(cgaGeometricSpace, cgaMultivector);
    }
}
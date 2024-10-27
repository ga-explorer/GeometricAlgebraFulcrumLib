using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Versors;

public static class CGaVersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> ToConformalCGaRotor<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaVersor<T>(cgaGeometricSpace, cgaMultivector, cgaMultivector.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaVersor<T> ToConformalCGaVersor<T>(this XGaMultivector<T> cgaMultivector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return new CGaVersor<T>(cgaGeometricSpace, cgaMultivector);
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Versors;

public static class XGaConformalVersorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalVersor<T> ToConformalCGaVersor<T>(this XGaMultivector<T> cgaMultivector, XGaConformalSpace<T> conformalSpace)
    {
        return new XGaConformalVersor<T>(conformalSpace, cgaMultivector);
    }
}
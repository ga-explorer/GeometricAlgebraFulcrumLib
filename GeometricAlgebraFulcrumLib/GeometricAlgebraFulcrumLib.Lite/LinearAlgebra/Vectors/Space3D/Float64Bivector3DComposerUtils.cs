using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public static class Float64Bivector3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector3D ToXyBivector3D(this Float64Bivector2D bivector)
    {
        return Float64Bivector3D.Create(bivector.Xy, 0, 0);
    }

}
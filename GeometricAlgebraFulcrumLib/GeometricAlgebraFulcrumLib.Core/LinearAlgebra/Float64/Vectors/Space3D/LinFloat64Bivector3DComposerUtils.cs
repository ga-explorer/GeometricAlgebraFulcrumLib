using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Bivector3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D ToXyBivector3D(this LinFloat64Bivector2D bivector)
    {
        return LinFloat64Bivector3D.Create(bivector.Xy, 0, 0);
    }

}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinBivector3DComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> ToXyBivector3D<T>(this LinBivector2D<T> bivector)
    {
        var zero = bivector.ScalarProcessor.Zero;

        return LinBivector3D<T>.Create(bivector.Xy, zero, zero);
    }

}
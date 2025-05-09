using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Reflectors;

public static class RGaReflectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureReflector<T> ToPureReflector<T>(this RGaVector<T> vector)
    {
        return RGaPureReflector<T>.Create(vector);
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Reflectors;

public static class XGaReflectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureReflector<T> ToPureReflector<T>(this XGaVector<T> vector)
    {
        return XGaPureReflector<T>.Create(vector);
    }
}
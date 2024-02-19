using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public static class XGaConformalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetDistance<T>(this XGaConformalIpnsVector<T> ipnsVector, XGaVector<T> positionVector)
    {
        var space = ipnsVector.Space;

        var distance = 
            ipnsVector.GetUnitWeightVector().Sp(
                space.CreateIpnsPoint(positionVector).Vector
            );

        return ipnsVector.HasZeroWeight()
            ? distance
            : -2d * distance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetDistance<T>(this XGaConformalIpnsVector<T> ipnsVector1, XGaConformalIpnsVector<T> ipnsVector2)
    {
        var distance =
            ipnsVector1.GetUnitWeightVector().Sp(ipnsVector2.GetUnitWeightVector());

        return ipnsVector1.HasZeroWeight() || ipnsVector2.HasZeroWeight()
            ? distance
            : -2d * distance;
    }
}
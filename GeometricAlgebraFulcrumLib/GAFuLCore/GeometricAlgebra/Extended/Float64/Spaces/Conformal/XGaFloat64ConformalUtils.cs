using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Spaces.Conformal;

public static class XGaFloat64ConformalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistance(this XGaFloat64ConformalIpnsVector ipnsVector, XGaFloat64Vector positionVector)
    {
        var space = ipnsVector.Space;

        var distance = 
            ipnsVector.GetUnitWeightVector().Sp(
                space.CreateIpnsPoint(positionVector).Vector
            ).ScalarValue;

        return ipnsVector.HasZeroWeight()
            ? distance
            : -2d * distance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetDistance(this XGaFloat64ConformalIpnsVector ipnsVector1, XGaFloat64ConformalIpnsVector ipnsVector2)
    {
        var distance =
            ipnsVector1.GetUnitWeightVector().Sp(ipnsVector2.GetUnitWeightVector()).ScalarValue;

        return ipnsVector1.HasZeroWeight() || ipnsVector2.HasZeroWeight()
            ? distance
            : -2d * distance;
    }
}
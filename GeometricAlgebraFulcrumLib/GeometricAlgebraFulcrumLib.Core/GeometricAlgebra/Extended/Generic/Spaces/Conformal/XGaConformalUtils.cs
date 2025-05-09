using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Spaces.Conformal;

public static class XGaConformalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistance<T>(this XGaConformalIpnsVector<T> ipnsVector, XGaVector<T> positionVector)
    {
        var space = ipnsVector.Space;

        var distance = 
            ipnsVector.GetUnitWeightVector().Sp(
                space.CreateIpnsPoint(positionVector).Vector
            );

        return (ipnsVector.HasZeroWeight() ? distance : -2d * distance).ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistance<T>(this XGaConformalIpnsVector<T> ipnsVector1, XGaConformalIpnsVector<T> ipnsVector2)
    {
        var distance =
            ipnsVector1.GetUnitWeightVector().Sp(ipnsVector2.GetUnitWeightVector());

        return (ipnsVector1.HasZeroWeight() || ipnsVector2.HasZeroWeight() ? distance : -2d * distance).ToScalar();
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Spaces.Conformal;

public static class RGaConformalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistance<T>(this RGaConformalIpnsVector<T> ipnsVector, RGaVector<T> positionVector)
    {
        var space = ipnsVector.Space;

        var distance = 
            ipnsVector.GetUnitWeightVector().Sp(
                space.CreateIpnsPoint(positionVector).Vector
            );

        return (ipnsVector.HasZeroWeight() ? distance : -2d * distance).ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetDistance<T>(this RGaConformalIpnsVector<T> ipnsVector1, RGaConformalIpnsVector<T> ipnsVector2)
    {
        var distance =
            ipnsVector1.GetUnitWeightVector().Sp(ipnsVector2.GetUnitWeightVector());

        return (ipnsVector1.HasZeroWeight() || ipnsVector2.HasZeroWeight() ? distance : -2d * distance).ToScalar();
    }
}
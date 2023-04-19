using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Spaces.Conformal
{
    public static class RGaFloat64ConformalUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetDistance(this RGaFloat64ConformalIpnsVector ipnsVector, RGaFloat64Vector positionVector)
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
        public static double GetDistance(this RGaFloat64ConformalIpnsVector ipnsVector1, RGaFloat64ConformalIpnsVector ipnsVector2)
        {
            var distance =
                ipnsVector1.GetUnitWeightVector().Sp(ipnsVector2.GetUnitWeightVector()).ScalarValue;

            return ipnsVector1.HasZeroWeight() || ipnsVector2.HasZeroWeight()
                ? distance
                : -2d * distance;
        }
    }
}
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Angles
{
    public class ComputedParametricAngle :
        IParametricAngle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricAngle Create(Func<double, Float64PlanarAngle> getPointFunc)
        {
            return new ComputedParametricAngle(Float64ScalarRange.Infinite, getPointFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricAngle Create(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc)
        {
            return new ComputedParametricAngle(parameterRange, getPointFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricAngle Create(Func<double, Float64PlanarAngle> getPointFunc, Func<double, Float64PlanarAngle> getTangentFunc)
        {
            return new ComputedParametricAngle(Float64ScalarRange.Infinite, getPointFunc, getTangentFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricAngle Create(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc, Func<double, Float64PlanarAngle> getTangentFunc)
        {
            return new ComputedParametricAngle(parameterRange, getPointFunc, getTangentFunc);
        }


        public Func<double, Float64PlanarAngle> GetPointFunc { get; }

        public Func<double, Float64PlanarAngle>? GetTangentFunc { get; }

        public Float64ScalarRange ParameterRange { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricAngle(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc)
        {
            ParameterRange = parameterRange;
            GetPointFunc = getPointFunc;
            GetTangentFunc = t => 
                Differentiate.FirstDerivative(
                    x => getPointFunc(x).Radians, 
                    t
                ).RadiansToAngle();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricAngle(Float64ScalarRange parameterRange, Func<double, Float64PlanarAngle> getPointFunc, Func<double, Float64PlanarAngle> getTangentFunc)
        {
            ParameterRange = parameterRange;
            GetPointFunc = getPointFunc;
            GetTangentFunc = getTangentFunc;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetAngle(double parameterValue)
        {
            return GetPointFunc(parameterValue).GetAngleInPositiveRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetDerivative1Angle(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue).GetAngleInPositiveRange();

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return ((p2 - p1) / (2 * epsilon)).GetAngleInPositiveRange();
        }
    }
}
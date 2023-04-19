using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Mapped
{
    public class GrC1ArcLengthRotatedNormalsCurve3D :
        IArcLengthC1Curve3D
    {
        public IArcLengthC1Curve3D BaseCurve { get; }

        public Func<double, Float64PlanarAngle> AngleFunction { get; }

        public double ParameterValueMin
            => BaseCurve.ParameterValueMin;

        public double ParameterValueMax
            => BaseCurve.ParameterValueMax;


        public GrC1ArcLengthRotatedNormalsCurve3D(IArcLengthC1Curve3D baseCurve, Func<double, Float64PlanarAngle> angleFunction)
        {
            BaseCurve = baseCurve;
            AngleFunction = angleFunction;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseCurve.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return BaseCurve.GetPoint(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return BaseCurve.GetDerivative1Point(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return BaseCurve
                .GetFrame(parameterValue)
                .RotateNormalsBy(
                    AngleFunction(parameterValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLength()
        {
            return BaseCurve.GetLength();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ParameterToLength(double parameterValue)
        {
            return BaseCurve.ParameterToLength(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthToParameter(double length)
        {
            return BaseCurve.LengthToParameter(length);
        }
    }
}
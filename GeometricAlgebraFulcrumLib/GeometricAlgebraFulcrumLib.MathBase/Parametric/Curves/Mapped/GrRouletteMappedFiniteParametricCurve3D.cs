using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Mapped
{
    public class GrRouletteMappedFiniteParametricCurve3D :
        IArcLengthC1Curve3D
    {
        public IArcLengthC1Curve3D BaseCurve { get; }

        public RouletteMap3D RouletteMap { get; }

        public double ParameterValueMin
            => BaseCurve.ParameterValueMin;

        public double ParameterValueMax
            => BaseCurve.ParameterValueMax;


        public GrRouletteMappedFiniteParametricCurve3D(IArcLengthC1Curve3D baseCurve, RouletteMap3D rouletteMap)
        {
            BaseCurve = baseCurve;
            RouletteMap = rouletteMap;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseCurve.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return RouletteMap.MapPoint(BaseCurve.GetPoint(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return BaseCurve.GetDerivative1Point(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            var frame = BaseCurve.GetFrame(parameterValue);

            var point = RouletteMap.MapPoint(frame.Point);
            var (tangent, normal1, normal2) =
                RouletteMap.RotationQuaternion.QuaternionRotate(
                    frame.Tangent,
                    frame.Normal1,
                    frame.Normal2
                );

            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                point,
                normal1,
                normal2,
                tangent
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
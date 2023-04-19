using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Roulettes
{
    public class RouletteCurve3D :
        IParametricC2Curve3D
    {
        public IArcLengthC1Curve3D FixedCurve { get; }

        public IArcLengthC1Curve3D MovingCurve { get; }

        public Float64Tuple3D GeneratorPoint { get; }

        public double ParameterValueMin
            => 0;

        public double ParameterValueMax { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RouletteCurve3D(IArcLengthC1Curve3D fixedCurve, IArcLengthC1Curve3D movingCurve, double parameterValueMax)
        {
            FixedCurve = fixedCurve;
            MovingCurve = movingCurve;
            GeneratorPoint = movingCurve.GetPoint(movingCurve.ParameterValueMin);
            ParameterValueMax = parameterValueMax;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RouletteCurve3D(IArcLengthC1Curve3D fixedCurve, IArcLengthC1Curve3D movingCurve, IFloat64Tuple3D generatorPoint, double parameterValueMax)
        {
            FixedCurve = fixedCurve;
            MovingCurve = movingCurve;
            GeneratorPoint = generatorPoint.ToTuple3D();
            ParameterValueMax = parameterValueMax;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return FixedCurve.IsValid() &&
                   MovingCurve.IsValid();
        }

        public RouletteMap3D GetRouletteMap(double parameterValue)
        {
            var t1 = MovingCurve.LengthToParameter(parameterValue);
            var movingFrame = MovingCurve.GetFrame(t1);

            var t2 = FixedCurve.LengthToParameter(parameterValue);
            var fixedFrame = FixedCurve.GetFrame(t2);

            var quaternion =
                movingFrame.CreateFrameToFrameRotationQuaternion(fixedFrame);

            return new RouletteMap3D(
                fixedFrame.Point,
                movingFrame.Point,
                quaternion
            );
        }

        public Float64Tuple3D GetPoint(double parameterValue)
        {
            var t1 = MovingCurve.LengthToParameter(parameterValue);
            var movingFrame = MovingCurve.GetFrame(t1);

            var t2 = FixedCurve.LengthToParameter(parameterValue);
            var fixedFrame = FixedCurve.GetFrame(t2);

            var quaternion =
                movingFrame.CreateFrameToFrameRotationQuaternion(fixedFrame);

            return fixedFrame.Point +
                   quaternion.QuaternionRotate(GeneratorPoint - movingFrame.Point);
        }

        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            //TODO: May be add a caching mechanism to speed this more
            var fX =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).X);

            var fY =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).Y);

            var fZ =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).Z);

            return new Float64Tuple3D(
                fX(parameterValue),
                fY(parameterValue),
                fZ(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return this.GetFrenetSerretFrame(parameterValue);
        }

        public Float64Tuple3D GetDerivative2Point(double parameterValue)
        {
            var fX =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).X);

            var fY =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).Y);

            var fZ =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).Z);

            return new Float64Tuple3D(
                fX(parameterValue),
                fY(parameterValue),
                fZ(parameterValue)
            );
        }
    }
}

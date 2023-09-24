using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Roulettes
{
    public class RouletteCurve3D :
        IParametricC2Curve3D
    {
        public IArcLengthCurve3D FixedCurve { get; }

        public IArcLengthCurve3D MovingCurve { get; }

        public Float64Vector3D GeneratorPoint { get; }

        public Float64ScalarRange ParameterRange { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RouletteCurve3D(IArcLengthCurve3D fixedCurve, IArcLengthCurve3D movingCurve, double parameterValueMax)
        {
            FixedCurve = fixedCurve;
            MovingCurve = movingCurve;
            GeneratorPoint = movingCurve.GetPoint(movingCurve.ParameterRange.MinValue);
            ParameterRange = Float64ScalarRange.Create(0, parameterValueMax);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RouletteCurve3D(IArcLengthCurve3D fixedCurve, IArcLengthCurve3D movingCurve, IFloat64Vector3D generatorPoint, double parameterValueMax)
        {
            FixedCurve = fixedCurve;
            MovingCurve = movingCurve;
            GeneratorPoint = generatorPoint.ToVector3D();
            ParameterRange = Float64ScalarRange.Create(0, parameterValueMax);
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

        public Float64Vector3D GetPoint(double parameterValue)
        {
            var t1 = MovingCurve.LengthToParameter(parameterValue);
            var movingFrame = MovingCurve.GetFrame(t1);

            var t2 = FixedCurve.LengthToParameter(parameterValue);
            var fixedFrame = FixedCurve.GetFrame(t2);

            var quaternion =
                movingFrame.CreateFrameToFrameRotationQuaternion(fixedFrame);

            return fixedFrame.Point +
                   quaternion.RotateVector(GeneratorPoint - movingFrame.Point);
        }

        public Float64Vector3D GetDerivative1Point(double parameterValue)
        {
            //TODO: May be add a caching mechanism to speed this more
            var fX =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).X);

            var fY =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).Y);

            var fZ =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).Z);

            return Float64Vector3D.Create(fX(parameterValue),
                fY(parameterValue),
                fZ(parameterValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return this.GetFrenetSerretFrame(parameterValue);
        }

        public Float64Vector3D GetDerivative2Point(double parameterValue)
        {
            var fX =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).X);

            var fY =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).Y);

            var fZ =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).Z);

            return Float64Vector3D.Create(fX(parameterValue),
                fY(parameterValue),
                fZ(parameterValue));
        }
    }
}

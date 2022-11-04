using MathNet.Numerics;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using System.Runtime.CompilerServices;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Roulettes
{
    public class GrRouletteCurve3D :
        IGraphicsC2ParametricCurve3D
    {
        public IGraphicsC1ArcLengthCurve3D FixedCurve { get; }

        public IGraphicsC1ArcLengthCurve3D MovingCurve { get; }

        public Tuple3D GeneratorPoint { get; }

        public double ParameterValueMin 
            => 0;

        public double ParameterValueMax { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrRouletteCurve3D(IGraphicsC1ArcLengthCurve3D fixedCurve, IGraphicsC1ArcLengthCurve3D movingCurve, double parameterValueMax)
        {
            FixedCurve = fixedCurve;
            MovingCurve = movingCurve;
            GeneratorPoint = movingCurve.GetPoint(movingCurve.ParameterValueMin);
            ParameterValueMax = parameterValueMax;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrRouletteCurve3D(IGraphicsC1ArcLengthCurve3D fixedCurve, IGraphicsC1ArcLengthCurve3D movingCurve, ITuple3D generatorPoint, double parameterValueMax)
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

        public Tuple3D GetPoint(double parameterValue)
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

        public Tuple3D GetTangent(double parameterValue)
        {
            //TODO: May be add a caching mechanism to speed this more
            var fX = 
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).X);

            var fY =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).Y);

            var fZ =
                Differentiate.FirstDerivativeFunc(t => GetPoint(t).Z);

            return new Tuple3D(
                fX(parameterValue),
                fY(parameterValue),
                fZ(parameterValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return this.GetFrenetSerretFrame(parameterValue);
        }

        public Tuple3D GetSecondDerivative(double parameterValue)
        {
            var fX =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).X);

            var fY =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).Y);

            var fZ =
                Differentiate.SecondDerivativeFunc(t => GetPoint(t).Z);

            return new Tuple3D(
                fX(parameterValue),
                fY(parameterValue),
                fZ(parameterValue)
            );
        }
    }
}

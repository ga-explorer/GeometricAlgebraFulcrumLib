using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Lines
{
    public class ArcLengthLineSegment3D :
        IParametricC2Curve3D,
        IArcLengthC1Curve3D
    {
        public Float64Tuple3D Point1 { get; }

        public Float64Tuple3D Point2 { get; }

        public double ParameterValueMin
            => 0d;

        public double ParameterValueMax
            => 1d;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArcLengthLineSegment3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2)
        {
            Point1 = point1.ToTuple3D();
            Point2 = point2.ToTuple3D();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetLength()
        {
            return Point1.GetDistanceToPoint(Point2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ParameterToLength(double parameterValue)
        {
            return parameterValue.ClampPeriodic(1d) * GetLength();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthToParameter(double length)
        {
            var curveLength = GetLength();

            return length.ClampPeriodic(curveLength) / curveLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid() &&
                   Point2.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return parameterValue.Lerp(Point1, Point2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return Point2 - Point1;
        }

        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue.ClampPeriodic(1d),
                GetPoint(parameterValue),
                (Point2 - Point1).ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative2Point(double parameterValue)
        {
            return Float64Tuple3D.Zero;
        }
    }
}
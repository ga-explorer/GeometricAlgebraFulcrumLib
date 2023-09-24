using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Lines
{
    public class ArcLengthLineSegment3D :
        IParametricC2Curve3D,
        IArcLengthCurve3D
    {
        public Float64Vector3D Point1 { get; }

        public Float64Vector3D Point2 { get; }

        public Float64ScalarRange ParameterRange
            => Float64ScalarRange.ZeroToOne;
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArcLengthLineSegment3D(IFloat64Vector3D point1, IFloat64Vector3D point2)
        {
            Point1 = point1.ToVector3D();
            Point2 = point2.ToVector3D();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar GetLength()
        {
            return Point1.GetDistanceToPoint(Point2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar ParameterToLength(double parameterValue)
        {
            return parameterValue.ClampPeriodic(1d) * GetLength();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar LengthToParameter(double length)
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
        public Float64Vector3D GetPoint(double parameterValue)
        {
            return parameterValue.Lerp(Point1, Point2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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
        public Float64Vector3D GetDerivative2Point(double parameterValue)
        {
            return Float64Vector3D.Zero;
        }
    }
}
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Bezier
{
    public class BezierCurve2Degree2D :
        IParametricCurve2D
    {
        public Float64Vector2D Point1 { get; }

        public Float64Vector2D Point2 { get; }

        public Float64Vector2D Point3 { get; }
        
        public Float64Range1D ParameterRange 
            => Float64Range1D.Infinite;


        public BezierCurve2Degree2D(IFloat64Tuple2D point1, IFloat64Tuple2D point2, IFloat64Tuple2D point3)
        {
            Point1 = point1.ToLinVector2D();
            Point2 = point2.ToLinVector2D();
            Point3 = point3.ToLinVector2D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid() &&
                   Point2.IsValid() &&
                   Point3.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BezierCurve1Degree2D GetDerivativeCurve()
        {
            return new BezierCurve1Degree2D(
                2 * (Point2 - Point1),
                2 * (Point3 - Point2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetPoint(double t)
        {
            var (p1, p2, p3) = t.BernsteinBasis_2();

            return new Float64Vector2D(
                p1 * Point1.X + p2 * Point2.X + p3 * Point3.X,
                p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GetDerivative1Point(double t)
        {
            var s = 1 - t;

            var p1 = 2 * s;
            var p2 = 2 * t;

            return new Float64Vector2D(
                p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X),
                p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame2D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}

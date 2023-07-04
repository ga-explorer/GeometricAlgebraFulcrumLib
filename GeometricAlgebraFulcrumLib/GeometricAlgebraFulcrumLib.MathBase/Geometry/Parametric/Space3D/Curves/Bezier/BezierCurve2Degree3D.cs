using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Bezier
{
    public class BezierCurve2Degree3D :
        IParametricCurve3D
    {
        public Float64Vector3D Point1 { get; }

        public Float64Vector3D Point2 { get; }

        public Float64Vector3D Point3 { get; }
        
        public Float64Range1D ParameterRange 
            => Float64Range1D.Infinite;


        public BezierCurve2Degree3D(IFloat64Vector3D point1, IFloat64Vector3D point2, IFloat64Vector3D point3)
        {
            Point1 = point1.ToVector3D();
            Point2 = point2.ToVector3D();
            Point3 = point3.ToVector3D();

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
        public BezierCurve1Degree3D GetDerivativeCurve()
        {
            return new BezierCurve1Degree3D(
                2 * (Point2 - Point1),
                2 * (Point3 - Point2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double t)
        {
            var (p1, p2, p3) = t.BernsteinBasis_2();

            return Float64Vector3D.Create(p1 * Point1.X + p2 * Point2.X + p3 * Point3.X,
                p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y,
                p1 * Point1.Z + p2 * Point2.Z + p3 * Point3.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double t)
        {
            var s = 1 - t;

            var p1 = 2 * s;
            var p2 = 2 * t;

            return Float64Vector3D.Create(p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X),
                p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y),
                p1 * (Point2.Z - Point1.Z) + p2 * (Point3.Z - Point2.Z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetDerivative1Point(parameterValue)
            );
        }
    }
}

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Bezier
{
    public class GrBezierCurve2Degree3D :
        IGraphicsC1ParametricCurve3D
    {
        public Float64Tuple3D Point1 { get; }

        public Float64Tuple3D Point2 { get; }

        public Float64Tuple3D Point3 { get; }


        public GrBezierCurve2Degree3D([NotNull] IFloat64Tuple3D point1, [NotNull] IFloat64Tuple3D point2, [NotNull] IFloat64Tuple3D point3)
        {
            Point1 = point1.ToTuple3D();
            Point2 = point2.ToTuple3D();
            Point3 = point3.ToTuple3D();

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
        public GrBezierCurve1Degree3D GetDerivativeCurve()
        {
            return new GrBezierCurve1Degree3D(
                2 * (Point2 - Point1),
                2 * (Point3 - Point2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double t)
        {
            var (p1, p2, p3) = t.BernsteinBasis_2();

            return new Float64Tuple3D(
                p1 * Point1.X + p2 * Point2.X + p3 * Point3.X,
                p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y,
                p1 * Point1.Z + p2 * Point2.Z + p3 * Point3.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetTangent(double t)
        {
            var s = 1 - t;

            var p1 = 2 * s;
            var p2 = 2 * t;

            return new Float64Tuple3D(
                p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X),
                p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y),
                p1 * (Point2.Z - Point1.Z) + p2 * (Point3.Z - Point2.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetUnitTangent(double t)
        {
            return GetTangent(t).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetTangent(parameterValue)
            );
        }
    }
}

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Bezier
{
    public class GrBezierCurve3Degree3D :
        IGraphicsC2ParametricCurve3D
    {
        public Tuple3D Point1 { get; }

        public Tuple3D Point2 { get; }

        public Tuple3D Point3 { get; }

        public Tuple3D Point4 { get; }


        public GrBezierCurve3Degree3D([NotNull] ITuple3D point1, [NotNull] ITuple3D point2, [NotNull] ITuple3D point3, [NotNull] ITuple3D point4)
        {
            Point1 = point1.ToTuple3D();
            Point2 = point2.ToTuple3D();
            Point3 = point3.ToTuple3D();
            Point4 = point4.ToTuple3D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid() &&
                   Point2.IsValid() &&
                   Point3.IsValid() &&
                   Point4.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrBezierCurve2Degree3D GetDerivativeCurve()
        {
            return new GrBezierCurve2Degree3D(
                3 * (Point2 - Point1),
                3 * (Point3 - Point2),
                3 * (Point4 - Point3)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue)
        {
            var (p1, p2, p3, p4) = parameterValue.BernsteinBasis_3();

            return new Tuple3D(
                p1 * Point1.X + p2 * Point2.X + p3 * Point3.X + p4 * Point4.X,
                p1 * Point1.Y + p2 * Point2.Y + p3 * Point3.Y + p4 * Point4.Y,
                p1 * Point1.Z + p2 * Point2.Z + p3 * Point3.Z + p4 * Point4.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            var s = 1 - parameterValue;

            var p1 = 3 * s * s;
            var p2 = 6 * parameterValue * s;
            var p3 = 3 * parameterValue * parameterValue;

            return new Tuple3D(
                p1 * (Point2.X - Point1.X) + p2 * (Point3.X - Point2.X) + p3 * (Point4.X - Point3.X),
                p1 * (Point2.Y - Point1.Y) + p2 * (Point3.Y - Point2.Y) + p3 * (Point4.Y - Point3.Y),
                p1 * (Point2.Z - Point1.Z) + p2 * (Point3.Z - Point2.Z) + p3 * (Point4.Z - Point3.Z)
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
            return GrParametricCurveLocalFrame3D.CreateFrame(
                parameterValue,
                GetPoint(parameterValue),
                GetTangent(parameterValue)
            );
        }

        public Tuple3D GetSecondDerivative(double parameterValue)
        {
            var derivative2 = 
                GetDerivativeCurve().GetDerivativeCurve();

            return derivative2.GetPoint(parameterValue);
        }

        //public GrParametricCurveLocalFrame3D GetFrenetFrame(double parameterValue)
        //{
        //    var derivative2 = GetDerivativeCurve().GetDerivativeCurve();

        //    var firstDerivativeVector = GetTangent(parameterValue);
        //    var secondDerivativeVector = derivative2.GetPoint(parameterValue);

        //    return GrParametricCurveLocalFrame3D.CreateFrenetFrame(
        //        parameterValue,
        //        GetPoint(parameterValue),
        //        firstDerivativeVector, 
        //        secondDerivativeVector
        //    );
        //}

        //public GrParametricCurveLocalFrame3D GetFrenetFrameAt0()
        //{
        //    var firstDerivativeVector = 3 * (Point2 - Point1);
        //    var secondDerivativeVector = 6 * (Point3 - 2 * Point2 + Point1);

        //    return GrParametricCurveLocalFrame3D.CreateFrenetFrame(
        //        0,
        //        Point1,
        //        firstDerivativeVector, 
        //        secondDerivativeVector
        //    );
        //}
    }
}

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Bezier
{
    public class GrBezierCurve1Degree3D :
        IGraphicsC1ParametricCurve3D
    {
        public Tuple3D Point1 { get; }

        public Tuple3D Point2 { get; }


        public GrBezierCurve1Degree3D([NotNull] ITuple3D point1, [NotNull] ITuple3D point2)
        {
            Point1 = point1.ToTuple3D();
            Point2 = point2.ToTuple3D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid() &&
                   Point2.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrBezierCurve0Degree3D GetDerivativeCurve()
        {
            return new GrBezierCurve0Degree3D(Point2 - Point1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue)
        {
            var (p1, p2) = parameterValue.BernsteinBasis_1();

            return new Tuple3D(
                p1 * Point1.X + p2 * Point2.X,
                p1 * Point1.Y + p2 * Point2.Y,
                p1 * Point1.Z + p2 * Point2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            return Point2 - Point1;
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
    }
}
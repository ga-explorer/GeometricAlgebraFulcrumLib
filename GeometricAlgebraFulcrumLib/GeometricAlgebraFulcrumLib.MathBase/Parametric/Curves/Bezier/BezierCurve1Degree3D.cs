using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Bezier
{
    public class BezierCurve1Degree3D :
        IParametricCurve3D
    {
        public Float64Tuple3D Point1 { get; }

        public Float64Tuple3D Point2 { get; }


        public BezierCurve1Degree3D(IFloat64Tuple3D point1, IFloat64Tuple3D point2)
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
        public BezierCurve0Degree3D GetDerivativeCurve()
        {
            return new BezierCurve0Degree3D(Point2 - Point1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            var (p1, p2) = parameterValue.BernsteinBasis_1();

            return new Float64Tuple3D(
                p1 * Point1.X + p2 * Point2.X,
                p1 * Point1.Y + p2 * Point2.Y,
                p1 * Point1.Z + p2 * Point2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return Point2 - Point1;
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
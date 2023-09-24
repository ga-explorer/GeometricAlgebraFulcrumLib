using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Bezier
{
    public class BezierCurve1Degree3D :
        IParametricCurve3D
    {
        public Float64Vector3D Point1 { get; }

        public Float64Vector3D Point2 { get; }
        
        public Float64ScalarRange ParameterRange 
            => Float64ScalarRange.Infinite;


        public BezierCurve1Degree3D(IFloat64Vector3D point1, IFloat64Vector3D point2)
        {
            Point1 = point1.ToVector3D();
            Point2 = point2.ToVector3D();

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
        public Float64Vector3D GetPoint(double parameterValue)
        {
            var (p1, p2) = parameterValue.BernsteinBasis_1();

            return Float64Vector3D.Create(p1 * Point1.X + p2 * Point2.X,
                p1 * Point1.Y + p2 * Point2.Y,
                p1 * Point1.Z + p2 * Point2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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
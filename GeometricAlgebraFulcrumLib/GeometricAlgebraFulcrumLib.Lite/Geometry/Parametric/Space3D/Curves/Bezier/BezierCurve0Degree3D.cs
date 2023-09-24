using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Bezier
{
    public class BezierCurve0Degree3D :
        IParametricCurve3D
    {
        public Float64Vector3D Point1 { get; }

        public Float64ScalarRange ParameterRange 
            => Float64ScalarRange.Infinite;


        public BezierCurve0Degree3D(IFloat64Vector3D point1)
        {
            Point1 = point1.ToVector3D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double t)
        {
            return Point1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double t)
        {
            return Float64Vector3D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                Point1,
                Float64Vector3D.UnitSymmetric
            );
        }
    }
}
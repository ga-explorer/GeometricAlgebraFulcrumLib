using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Bezier
{
    public class BezierCurve0Degree3D :
        IParametricCurve3D
    {
        public Float64Tuple3D Point1 { get; }


        public BezierCurve0Degree3D(IFloat64Tuple3D point1)
        {
            Point1 = point1.ToTuple3D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double t)
        {
            return Point1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double t)
        {
            return Float64Tuple3D.Zero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                Point1,
                Float64Tuple3D.Zero,
                Float64Tuple3D.Zero,
                Float64Tuple3D.Zero
            );
        }
    }
}
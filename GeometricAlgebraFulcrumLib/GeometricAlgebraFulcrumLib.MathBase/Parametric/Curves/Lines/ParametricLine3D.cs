using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Lines
{
    public class ParametricLine3D :
        IParametricC2Curve3D
    {
        public Float64Tuple3D Point { get; }

        public Float64Tuple3D Vector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricLine3D(IFloat64Tuple3D point, IFloat64Tuple3D vector)
        {
            Point = point.ToTuple3D();
            Vector = vector.ToTuple3D();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point.IsValid() &&
                   Vector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return new Float64Tuple3D(
                Point.X + parameterValue * Vector.X,
                Point.Y + parameterValue * Vector.Y,
                Point.Z + parameterValue * Vector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return ParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                Vector.ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative2Point(double parameterValue)
        {
            return Float64Tuple3D.Zero;
        }
    }
}
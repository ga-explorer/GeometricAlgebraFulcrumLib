using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Lines
{
    public class ParametricLine3D :
        IParametricC2Curve3D
    {
        public Float64ScalarRange ParameterRange 
            => Float64ScalarRange.Infinite;

        public Float64Vector3D Point { get; }

        public Float64Vector3D Vector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricLine3D(IFloat64Vector3D point, IFloat64Vector3D vector)
        {
            Point = point.ToVector3D();
            Vector = vector.ToVector3D();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point.IsValid() &&
                   Vector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double parameterValue)
        {
            return Float64Vector3D.Create(Point.X + parameterValue * Vector.X,
                Point.Y + parameterValue * Vector.Y,
                Point.Z + parameterValue * Vector.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
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
        public Float64Vector3D GetDerivative2Point(double parameterValue)
        {
            return Float64Vector3D.Zero;
        }
    }
}
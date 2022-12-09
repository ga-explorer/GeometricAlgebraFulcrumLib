using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Lines
{
    public class GrParametricLine3D :
        IGraphicsC2ParametricCurve3D
    {
        public Float64Tuple3D Point { get; }

        public Float64Tuple3D Vector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricLine3D([NotNull] IFloat64Tuple3D point, [NotNull] IFloat64Tuple3D vector)
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
        public Float64Tuple3D GetTangent(double parameterValue)
        {
            return Vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetUnitTangent(double parameterValue)
        {
            return Vector.ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                Vector.ToUnitVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetSecondDerivative(double parameterValue)
        {
            return Float64Tuple3D.Zero;
        }
    }
}
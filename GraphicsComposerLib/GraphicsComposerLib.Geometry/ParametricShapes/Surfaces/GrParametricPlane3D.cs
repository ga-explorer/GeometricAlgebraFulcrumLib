using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces
{
    public class GrParametricPlane3D :
        IGraphicsParametricSurface3D
    {
        public Tuple3D Point { get; }

        public Tuple3D Vector1 { get; }

        public Tuple3D Vector2 { get; }

        public Tuple3D Normal { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricPlane3D([NotNull] ITuple3D point, [NotNull] ITuple3D vector1, [NotNull] ITuple3D vector2)
        {
            Point = point.ToTuple3D();
            Vector1 = vector1.ToTuple3D();
            Vector2 = vector2.ToTuple3D();
            Normal = Vector1.VectorUnitCross(Vector2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricPlane3D([NotNull] ITuple3D point, [NotNull] ITuple3D normal)
        {
            Point = point.ToTuple3D();
            Vector1 = normal.GetUnitNormal();
            Vector2 = normal.VectorUnitCross(Vector1);
            Normal = normal.ToUnitVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point.IsValid() &&
                   Vector1.IsValid() &&
                   Vector2.IsValid() &&
                   Normal.IsValid() &&
                   Normal.IsNearUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue1, double parameterValue2)
        {
            return new Tuple3D(
                Point.X + parameterValue1 * Vector1.X + parameterValue2 * Vector2.X,
                Point.Y + parameterValue1 * Vector1.Y + parameterValue2 * Vector2.Y,
                Point.Z + parameterValue1 * Vector1.Z + parameterValue2 * Vector2.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetNormal(double parameterValue1, double parameterValue2)
        {
            return Normal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitNormal(double parameterValue1, double parameterValue2)
        {
            return Normal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
        {
            return new GrParametricSurfaceLocalFrame3D(
                parameterValue1,
                parameterValue2,
                GetPoint(parameterValue1, parameterValue2),
                Normal
            );
        }
    }
}

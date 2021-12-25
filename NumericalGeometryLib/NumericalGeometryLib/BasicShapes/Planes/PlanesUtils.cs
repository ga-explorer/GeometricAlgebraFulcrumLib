using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicShapes.Planes
{
    public static class PlanesUtils
    {
        #region Operations on Planes
        public static Tuple3D GetOrigin(this IPlane3D plane)
        {
            return new Tuple3D(
                plane.OriginX,
                plane.OriginY,
                plane.OriginZ
            );
        }

        public static Tuple3D GetDirection1(this IPlane3D plane)
        {
            return new Tuple3D(
                plane.Direction1X,
                plane.Direction1Y,
                plane.Direction1Z
            );
        }

        public static Tuple3D GetDirection2(this IPlane3D plane)
        {
            return new Tuple3D(
                plane.Direction2X,
                plane.Direction2Y,
                plane.Direction2Z
            );
        }

        public static Tuple3D GetNormal(this IPlane3D plane)
        {
            return plane
                .GetDirection1()
                .VectorCross(plane.GetDirection2());
        }

        public static Tuple3D GetUnitNormal(this IPlane3D plane)
        {
            return plane
                .GetDirection1()
                .VectorUnitCross(plane.GetDirection2());
        }

        #endregion
    }
}

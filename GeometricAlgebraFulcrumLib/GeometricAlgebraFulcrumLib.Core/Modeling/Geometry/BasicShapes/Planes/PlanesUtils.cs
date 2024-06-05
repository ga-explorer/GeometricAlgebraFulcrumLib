using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Planes;

public static class PlanesUtils
{
    #region Operations on Planes
    public static LinFloat64Vector3D GetOrigin(this IPlane3D plane)
    {
        return LinFloat64Vector3D.Create(plane.OriginX,
            plane.OriginY,
            plane.OriginZ);
    }

    public static LinFloat64Vector3D GetDirection1(this IPlane3D plane)
    {
        return LinFloat64Vector3D.Create(plane.Direction1X,
            plane.Direction1Y,
            plane.Direction1Z);
    }

    public static LinFloat64Vector3D GetDirection2(this IPlane3D plane)
    {
        return LinFloat64Vector3D.Create(plane.Direction2X,
            plane.Direction2Y,
            plane.Direction2Z);
    }

    public static LinFloat64Vector3D GetNormal(this IPlane3D plane)
    {
        return plane
            .GetDirection1()
            .VectorCross(plane.GetDirection2());
    }

    public static LinFloat64Vector3D GetUnitNormal(this IPlane3D plane)
    {
        return plane
            .GetDirection1()
            .VectorUnitCross(plane.GetDirection2());
    }

    #endregion
}
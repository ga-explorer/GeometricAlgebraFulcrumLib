﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Planes.Space3D.Float64;

public static class Float64PlaneUtils
{
    #region Operations on Planes
    public static LinFloat64Vector3D GetOrigin(this IFloat64Plane3D plane)
    {
        return LinFloat64Vector3D.Create(plane.OriginX,
            plane.OriginY,
            plane.OriginZ);
    }

    public static LinFloat64Vector3D GetDirection1(this IFloat64Plane3D plane)
    {
        return LinFloat64Vector3D.Create(plane.Direction1X,
            plane.Direction1Y,
            plane.Direction1Z);
    }

    public static LinFloat64Vector3D GetDirection2(this IFloat64Plane3D plane)
    {
        return LinFloat64Vector3D.Create(plane.Direction2X,
            plane.Direction2Y,
            plane.Direction2Z);
    }

    public static LinFloat64Vector3D GetNormal(this IFloat64Plane3D plane)
    {
        return plane
            .GetDirection1()
            .VectorCross(plane.GetDirection2());
    }

    public static LinFloat64Vector3D GetUnitNormal(this IFloat64Plane3D plane)
    {
        return plane
            .GetDirection1()
            .VectorUnitCross(plane.GetDirection2());
    }

    #endregion
}
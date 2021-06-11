﻿using EuclideanGeometryLib.BasicMath.Maps.Space2D;
using EuclideanGeometryLib.BasicShapes;

namespace EuclideanGeometryLib.Borders
{
    /// <summary>
    /// This interface represents a convex bounding surface (i.e. a closed convex curve) in 2D
    /// </summary>
    public interface IBorderCurve2D : IFiniteGeometricShape2D, IIntersectable
    {
        IBorderCurve2D MapUsing(IAffineMap2D affineMap);
    }
}
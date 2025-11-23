using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

/// <summary>
/// Axis-aligned bounding box
/// </summary>
public struct SimpleAabb2D
{
    public static SimpleAabb2D Create(LinFloat64Vector2D min, LinFloat64Vector2D max)
    {
        return new SimpleAabb2D(min, max);
    }


    public LinFloat64Vector2D Min { get; private set; }

    public LinFloat64Vector2D Max { get; private set; }


    private SimpleAabb2D(LinFloat64Vector2D min, LinFloat64Vector2D max)
    {
        Min = min;
        Max = max;
    }


    public void Extend(LinFloat64Vector2D p)
    {
        Min = Min.ElementWiseMin(p);
        Max = Max.ElementWiseMax(p);
    }

    public double Area
        => (Max.X - Min.X) * (Max.Y - Min.Y);
    

    public bool Overlaps(SimpleAabb2D box2)
    {
        return !(Max.X < box2.Min.X || Min.X > box2.Max.X ||
                 Max.Y < box2.Min.Y || Min.Y > box2.Max.Y);
    }
    

    /// <summary>
    /// squared distance between AABB and point (0 if point inside AABB)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public double GetDistanceSquared(in LinFloat64Vector2D point)
    {
        var dx = 0.0;
        var dy = 0.0;

        if (Max.X < point.X)
            dx = point.X - Max.X;
        else if (point.X < Min.X)
            dx = Min.X - point.X;

        if (Max.Y < point.Y)
            dy = point.Y - Max.Y;
        else if (point.Y < Min.Y)
            dy = Min.Y - point.Y;

        return dx * dx + dy * dy;
    }
    /// <summary>
    /// squared distance between two AABBs (0 if they overlap)
    /// </summary>
    /// <param name="box2"></param>
    /// <returns></returns>
    public double GetDistanceSquared(in SimpleAabb2D box2)
    {
        var dx = 0.0;
        var dy = 0.0;

        if (Max.X < box2.Min.X)
            dx = box2.Min.X - Max.X;
        else if (box2.Max.X < Min.X)
            dx = Min.X - box2.Max.X;

        if (Max.Y < box2.Min.Y)
            dy = box2.Min.Y - Max.Y;
        else if (box2.Max.Y < Min.Y)
            dy = Min.Y - box2.Max.Y;

        return dx * dx + dy * dy;
    }
    
    /// <summary>
    /// squared distance between two AABBs (0 if they overlap)
    /// </summary>
    /// <param name="box2"></param>
    /// <returns></returns>
    public double GetDistanceSquared(in Float64BoundingBox2D box2)
    {
        var dx = 0.0;
        var dy = 0.0;

        if (Max.X < box2.MinX)
            dx = box2.MinX - Max.X;
        else if (box2.MaxX < Min.X)
            dx = Min.X - box2.MaxX;

        if (Max.Y < box2.MinY)
            dy = box2.MinY - Max.Y;
        else if (box2.MaxY < Min.Y)
            dy = Min.Y - box2.MaxY;

        return dx * dx + dy * dy;
    }

}
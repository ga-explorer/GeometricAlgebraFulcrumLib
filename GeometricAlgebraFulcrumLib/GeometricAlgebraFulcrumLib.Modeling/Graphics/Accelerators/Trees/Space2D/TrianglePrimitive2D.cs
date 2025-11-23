using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D;

public sealed class TrianglePrimitive2D : ISpatialPrimitive2D
{
    public Vector2 A { get; }
    public Vector2 B { get; }
    public Vector2 C { get; }
    public Bounds2 Bounds { get; }

    public TrianglePrimitive2D(Vector2 a, Vector2 b, Vector2 c)
    {
        A = a; B = b; C = c;
        var min = Vector2.Min(Vector2.Min(a, b), c);
        var max = Vector2.Max(Vector2.Max(a, b), c);
        Bounds = new Bounds2(min, max);
    }

    public float DistanceSquared(Vector2 p)
    {
        var d1 = PointSegmentDistanceSquared(p, A, B);
        var d2 = PointSegmentDistanceSquared(p, B, C);
        var d3 = PointSegmentDistanceSquared(p, C, A);
        return Math.Min(d1, Math.Min(d2, d3));
    }

    public bool Intersects(Ray2D ray, out float t)
    {
        // Möller–Trumbore style ray-triangle intersection in 2D
        // Treat triangle edges as segments, find nearest hit
        t = float.PositiveInfinity;
        var hit = false;

        if (RaySegmentIntersect(ray, A, B, ref t)) hit = true;
        if (RaySegmentIntersect(ray, B, C, ref t)) hit = true;
        if (RaySegmentIntersect(ray, C, A, ref t)) hit = true;

        return hit;
    }

    static float PointSegmentDistanceSquared(Vector2 p, Vector2 a, Vector2 b)
    {
        var ab = b - a;
        var ap = p - a;
        var t = Vector2.Dot(ap, ab) / Vector2.Dot(ab, ab);
        t = Math.Clamp(t, 0, 1);
        var closest = a + t * ab;
        return (p - closest).LengthSquared();
    }

    static bool RaySegmentIntersect(Ray2D ray, Vector2 a, Vector2 b, ref float t)
    {
        var v1 = ray.Origin - a;
        var v2 = b - a;
        var v3 = new Vector2(-ray.Direction.Y, ray.Direction.X);

        var dot = Vector2.Dot(v2, v3);
        if (Math.Abs(dot) < 1e-6f) return false;

        var t1 = Cross(v2, v1) / dot;
        var t2 = Vector2.Dot(v1, v3) / dot;

        if (t1 >= 0 && t2 >= 0 && t2 <= 1)
        {
            if (t1 < t)
            {
                t = t1;
            }
            return true;
        }
        return false;
    }

    static float Cross(Vector2 a, Vector2 b) => a.X * b.Y - a.Y * b.X;
}
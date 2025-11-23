using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D;

public sealed class CirclePrimitive : ISpatialPrimitive2D
{
    public Vector2 Center { get; }
    public float Radius { get; }
    public Bounds2 Bounds { get; }

    public CirclePrimitive(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
        Bounds = new Bounds2(center - new Vector2(radius), center + new Vector2(radius));
    }

    public float DistanceSquared(Vector2 p)
    {
        var d = p - Center;
        var ds = d.LengthSquared();
        if (ds <= Radius * Radius) return 0f;
        return ds - Radius * Radius;
    }

    public bool Intersects(Ray2D ray, out float t)
    {
        var oc = ray.Origin - Center;
        var a = Vector2.Dot(ray.Direction, ray.Direction);
        var b = 2f * Vector2.Dot(oc, ray.Direction);
        var c = Vector2.Dot(oc, oc) - Radius * Radius;
        var disc = b * b - 4f * a * c;
        if (disc < 0)
        {
            t = float.PositiveInfinity;
            return false;
        }
        var sqrtd = (float)Math.Sqrt(disc);
        var t0 = (-b - sqrtd) / (2f * a);
        var t1 = (-b + sqrtd) / (2f * a);
        t = (t0 >= 0) ? t0 : t1;
        return t >= 0;
    }
}
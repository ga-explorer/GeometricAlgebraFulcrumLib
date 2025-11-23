using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space3D;

public sealed class SpherePrimitive : ISpatialPrimitive
{
    public Vector3 Center { get; }
    public float Radius { get; }
    public Bounds3 Bounds { get; }

    public SpherePrimitive(Vector3 center, float radius)
    {
        Center = center; Radius = radius;
        Bounds = new Bounds3(center - new Vector3(radius), center + new Vector3(radius));
    }

    public float DistanceSquared(Vector3 point)
    {
        var d = point - Center;
        var ds = d.LengthSquared();
        var r = Radius;
        if (ds <= r * r) return 0f;
        // exact squared distance to sphere surface = (sqrt(ds)-r)^2 = ds + r^2 - 2*r*sqrt(ds)
        // but we can return (sqrt(ds)-r)^2 cheaply via sqrt if desired; for ordering this is fine
        var dist = (float)Math.Sqrt(ds) - r;
        return dist <= 0 ? 0f : dist * dist;
    }

    public bool Intersects(Ray ray, out float t)
    {
        var oc = ray.Origin - Center;
        var a = Vector3.Dot(ray.Direction, ray.Direction);
        var b = 2f * Vector3.Dot(oc, ray.Direction);
        var c = Vector3.Dot(oc, oc) - Radius * Radius;
        var disc = b * b - 4f * a * c;
        if (disc < 0)
        {
            t = float.PositiveInfinity; return false;
        }
        var sqrtd = (float)Math.Sqrt(disc);
        var t0 = (-b - sqrtd) / (2f * a);
        var t1 = (-b + sqrtd) / (2f * a);
        t = t0 >= 0 ? t0 : t1;
        return t >= 0;
    }
}
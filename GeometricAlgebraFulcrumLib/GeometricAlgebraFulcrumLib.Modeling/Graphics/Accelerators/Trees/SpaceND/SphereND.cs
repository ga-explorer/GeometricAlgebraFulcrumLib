namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND;

public sealed class SphereND : ISpatialPrimitiveND
{
    public readonly float[] Center;
    public readonly float Radius;
    public BoundsND Bounds { get; }

    public SphereND(float[] center, float radius)
    {
        Center = center;
        Radius = radius;
        var min = new float[center.Length];
        var max = new float[center.Length];
        for (var i = 0; i < center.Length; i++)
        {
            min[i] = center[i] - radius;
            max[i] = center[i] + radius;
        }
        Bounds = new BoundsND(min, max);
    }

    public float DistanceSquared(float[] p)
    {
        var sum = 0f;
        for (var i = 0; i < Center.Length; i++)
        {
            var d = p[i] - Center[i];
            sum += d * d;
        }
        if (sum <= Radius * Radius) return 0f;
        return sum - Radius * Radius;
    }

    public bool Intersects(RayND ray, out float t)
    {
        float a = 0, b = 0, c = 0;
        for (var i = 0; i < Center.Length; i++)
        {
            var oc = ray.Origin[i] - Center[i];
            a += ray.Direction[i] * ray.Direction[i];
            b += 2 * oc * ray.Direction[i];
            c += oc * oc;
        }
        c -= Radius * Radius;
        var disc = b * b - 4 * a * c;
        if (disc < 0)
        {
            t = float.PositiveInfinity;
            return false;
        }
        var sqrtd = (float)Math.Sqrt(disc);
        var t0 = (-b - sqrtd) / (2 * a);
        var t1 = (-b + sqrtd) / (2 * a);
        t = (t0 >= 0) ? t0 : t1;
        return t >= 0;
    }
}
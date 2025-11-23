namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND;

public sealed class AxisAlignedSimplexND : ISpatialPrimitiveND
{
    public readonly float[] Min;
    public readonly float[] Max;
    public BoundsND Bounds { get; }

    public AxisAlignedSimplexND(float[] min, float[] max)
    {
        Min = min;
        Max = max;
        Bounds = new BoundsND(min, max);
    }

    public float DistanceSquared(float[] p)
    {
        return Bounds.DistanceSquaredToPoint(p);
    }

    public bool Intersects(RayND ray, out float t)
    {
        // Axis-aligned bounding box test
        var tmin = float.NegativeInfinity;
        var tmax = float.PositiveInfinity;
        for (var i = 0; i < Min.Length; i++)
        {
            if (Math.Abs(ray.Direction[i]) < 1e-6f)
            {
                if (ray.Origin[i] < Min[i] || ray.Origin[i] > Max[i])
                {
                    t = float.PositiveInfinity;
                    return false;
                }
            }
            else
            {
                var ood = 1f / ray.Direction[i];
                var t1 = (Min[i] - ray.Origin[i]) * ood;
                var t2 = (Max[i] - ray.Origin[i]) * ood;
                if (t1 > t2) (t1, t2) = (t2, t1);
                tmin = Math.Max(tmin, t1);
                tmax = Math.Min(tmax, t2);
                if (tmin > tmax)
                {
                    t = float.PositiveInfinity;
                    return false;
                }
            }
        }
        t = tmin;
        return true;
    }
}
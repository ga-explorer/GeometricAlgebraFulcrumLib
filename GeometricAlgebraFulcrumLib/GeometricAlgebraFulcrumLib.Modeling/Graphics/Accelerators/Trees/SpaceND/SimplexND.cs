namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND;

public sealed class SimplexND : ISpatialPrimitiveND
{
    public readonly float[][] Vertices;
    public BoundsND Bounds { get; }

    public SimplexND(params float[][] vertices)
    {
        Vertices = vertices;
        var d = vertices[0].Length;
        var min = new float[d];
        var max = new float[d];
        Array.Copy(vertices[0], min, d);
        Array.Copy(vertices[0], max, d);
        foreach (var v in vertices)
        {
            for (var i = 0; i < d; i++)
            {
                if (v[i] < min[i]) min[i] = v[i];
                if (v[i] > max[i]) max[i] = v[i];
            }
        }
        Bounds = new BoundsND(min, max);
    }

    public float DistanceSquared(float[] p)
    {
        // Approximate: distance to bounding box
        return Bounds.DistanceSquaredToPoint(p);
    }

    public bool Intersects(RayND ray, out float t)
    {
        // Conservative: ray-box intersection
        return new AxisAlignedSimplexND(Bounds.Min, Bounds.Max).Intersects(ray, out t);
    }
}
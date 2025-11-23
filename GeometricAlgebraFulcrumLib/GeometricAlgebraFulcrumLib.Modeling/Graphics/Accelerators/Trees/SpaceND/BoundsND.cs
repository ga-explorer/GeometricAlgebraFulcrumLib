namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND;

public readonly struct BoundsND
{
    public readonly float[] Min;
    public readonly float[] Max;
    public int Dimensions => Min.Length;

    public BoundsND(float[] min, float[] max)
    {
        Min = min;
        Max = max;
    }

    public float[] Center()
    {
        var c = new float[Dimensions];
        for (var i = 0; i < Dimensions; i++)
            c[i] = 0.5f * (Min[i] + Max[i]);
        return c;
    }

    public bool Contains(float[] p)
    {
        for (var i = 0; i < Dimensions; i++)
        {
            if (p[i] < Min[i] || p[i] > Max[i]) return false;
        }
        return true;
    }

    public bool Intersects(BoundsND other)
    {
        for (var i = 0; i < Dimensions; i++)
        {
            if (Max[i] < other.Min[i] || Min[i] > other.Max[i]) return false;
        }
        return true;
    }

    public float DistanceSquaredToPoint(float[] p)
    {
        var sum = 0f;
        for (var i = 0; i < Dimensions; i++)
        {
            float dx = 0;
            if (p[i] < Min[i]) dx = Min[i] - p[i];
            else if (p[i] > Max[i]) dx = p[i] - Max[i];
            sum += dx * dx;
        }
        return sum;
    }
}
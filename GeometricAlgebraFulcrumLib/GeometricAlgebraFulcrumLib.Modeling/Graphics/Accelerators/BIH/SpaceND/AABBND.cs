using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.SpaceND;

public sealed class AABBND
{
    public readonly double[] Min;
    public readonly double[] Max;
    public int Dim => Min.Length;

    public AABBND(int dim)
    {
        Min = new double[dim];
        Max = new double[dim];
        for (var i = 0; i < dim; i++)
        {
            Min[i] = double.PositiveInfinity;
            Max[i] = double.NegativeInfinity;
        }
    }

    public AABBND(double[] min, double[] max)
    {
        if (min.Length != max.Length) throw new ArgumentException("min/max dimension mismatch");
        Min = (double[])min.Clone();
        Max = (double[])max.Clone();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(AABBND other)
    {
        for (var i = 0; i < Min.Length; i++)
        {
            if (Max[i] < other.Min[i] || other.Max[i] < Min[i]) return false;
        }
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double DistanceSquaredToPoint(double[] p)
    {
        var d2 = 0.0;
        for (var i = 0; i < Min.Length; i++)
        {
            var v = 0.0;
            if (p[i] < Min[i]) v = Min[i] - p[i];
            else if (p[i] > Max[i]) v = p[i] - Max[i];
            d2 += v * v;
        }
        return d2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Center(int axis) => (Min[axis] + Max[axis]) * 0.5;

    public static AABBND FromPoints(double[][] points)
    {
        if (points.Length == 0) throw new ArgumentException("Empty points");
        var dim = points[0].Length;
        var aabb = new AABBND(dim);
        for (var i = 0; i < points.Length; i++)
        {
            for (var d = 0; d < dim; d++)
            {
                var v = points[i][d];
                if (v < aabb.Min[d]) aabb.Min[d] = v;
                if (v > aabb.Max[d]) aabb.Max[d] = v;
            }
        }
        return aabb;
    }
}
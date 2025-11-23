using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space3D;

public readonly struct Bounds3
{
    public Vector3 Min { get; }
    public Vector3 Max { get; }

    public Bounds3(Vector3 min, Vector3 max)
    {
        Min = min;
        Max = max;
    }

    public Vector3 Center => (Min + Max) * 0.5f;
    public Vector3 Extents => (Max - Min) * 0.5f;

    public bool Contains(Bounds3 other)
    {
        return Min.X <= other.Min.X && Min.Y <= other.Min.Y && Min.Z <= other.Min.Z
               && Max.X >= other.Max.X && Max.Y >= other.Max.Y && Max.Z >= other.Max.Z;
    }

    public bool Intersects(Bounds3 other)
    {
        if (Max.X < other.Min.X || Min.X > other.Max.X) return false;
        if (Max.Y < other.Min.Y || Min.Y > other.Max.Y) return false;
        if (Max.Z < other.Min.Z || Min.Z > other.Max.Z) return false;
        return true;
    }

    public bool Contains(Vector3 point)
    {
        return point.X >= Min.X && point.Y >= Min.Y && point.Z >= Min.Z
               && point.X <= Max.X && point.Y <= Max.Y && point.Z <= Max.Z;
    }

    public static Bounds3 Encapsulate(Bounds3 a, Bounds3 b)
    {
        var min = Vector3.Min(a.Min, b.Min);
        var max = Vector3.Max(a.Max, b.Max);
        return new Bounds3(min, max);
    }

    public float DistanceSquaredToPoint(Vector3 p)
    {
        var dx = Math.Max(0, Math.Max(Min.X - p.X, p.X - Max.X));
        var dy = Math.Max(0, Math.Max(Min.Y - p.Y, p.Y - Max.Y));
        var dz = Math.Max(0, Math.Max(Min.Z - p.Z, p.Z - Max.Z));
        return dx * dx + dy * dy + dz * dz;
    }
}
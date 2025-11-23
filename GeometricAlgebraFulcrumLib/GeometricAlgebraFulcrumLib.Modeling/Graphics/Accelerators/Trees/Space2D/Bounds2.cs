using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D;

public readonly struct Bounds2
{
    public Vector2 Min { get; }

    public Vector2 Max { get; }

    public Bounds2(Vector2 min, Vector2 max)
    {
        Min = min;
        Max = max;
    }

    public Vector2 Center => (Min + Max) * 0.5f;
    public Vector2 Extents => (Max - Min) * 0.5f;

    public bool Contains(Vector2 p)
    {
        return p.X >= Min.X && p.Y >= Min.Y && p.X <= Max.X && p.Y <= Max.Y;
    }

    public bool Intersects(Bounds2 other)
    {
        if (Max.X < other.Min.X || Min.X > other.Max.X) return false;
        if (Max.Y < other.Min.Y || Min.Y > other.Max.Y) return false;
        return true;
    }

    public float DistanceSquaredToPoint(Vector2 p)
    {
        var dx = Math.Max(0, Math.Max(Min.X - p.X, p.X - Max.X));
        var dy = Math.Max(0, Math.Max(Min.Y - p.Y, p.Y - Max.Y));
        return dx * dx + dy * dy;
    }
}
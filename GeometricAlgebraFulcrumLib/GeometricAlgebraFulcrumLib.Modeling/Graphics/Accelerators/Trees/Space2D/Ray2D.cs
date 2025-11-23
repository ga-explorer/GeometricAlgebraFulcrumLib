using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D;

public readonly struct Ray2D
{
    public Vector2 Origin { get; }
    public Vector2 Direction { get; } // normalized

    public Ray2D(Vector2 origin, Vector2 dir)
    {
        Origin = origin;
        Direction = Vector2.Normalize(dir);
    }
}
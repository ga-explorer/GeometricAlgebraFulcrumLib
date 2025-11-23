using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space3D;

public readonly struct Ray
{
    public Vector3 Origin { get; }
    public Vector3 Direction { get; } // assumed normalized

    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = Vector3.Normalize(direction);
    }
}
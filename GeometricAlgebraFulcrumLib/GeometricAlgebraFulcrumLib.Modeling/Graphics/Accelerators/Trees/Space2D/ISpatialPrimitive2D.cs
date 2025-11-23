using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D;

public interface ISpatialPrimitive2D
{
    Bounds2 Bounds { get; }
    float DistanceSquared(Vector2 p);
    bool Intersects(Ray2D ray, out float t);
}
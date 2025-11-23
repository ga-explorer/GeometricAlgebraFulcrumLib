using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space3D;

public interface ISpatialPrimitive
{
    // Axis-aligned bounding box of the primitive
    Bounds3 Bounds { get; }

    // Squared distance from a point to the primitive (used in NN queries)
    float DistanceSquared(Vector3 point);

    // Ray intersection test. Return true if hit and set distance t along ray.
    bool Intersects(Ray ray, out float t);
}
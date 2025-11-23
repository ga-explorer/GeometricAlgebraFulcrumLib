namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND;

public interface ISpatialPrimitiveND
{
    BoundsND Bounds { get; }
    float DistanceSquared(float[] p);
    bool Intersects(RayND ray, out float t);
}
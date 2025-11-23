// BinaryTree2D.cs
// Efficient, generic binary spatial tree for 2D problems
// - Generic primitive type T constrained by ISpatialPrimitive2D
// - Non-alloc query APIs with spans/visitors
// - Range, ray, and nearest-neighbor queries
// - Custom heap for NN queries to avoid allocations

using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D
{
    static class Bounds2Extensions
    {
        public static Vector2 Center(this Bounds2 b) => b.Center;
    }
}

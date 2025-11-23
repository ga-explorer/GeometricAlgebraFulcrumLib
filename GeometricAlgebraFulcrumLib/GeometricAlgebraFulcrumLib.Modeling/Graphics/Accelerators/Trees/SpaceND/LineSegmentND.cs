// SpatialTreeND.cs
// Generalized n-dimensional space partition trees (HyperOctreeND, KDTreeND)
// Extended with example n-D primitives: SphereND, AxisAlignedSimplexND, SimplexND, LineSegmentND

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.SpaceND
{
    public sealed class LineSegmentND : ISpatialPrimitiveND
    {
        public readonly float[] A;
        public readonly float[] B;
        public BoundsND Bounds { get; }

        public LineSegmentND(float[] a, float[] b)
        {
            A = a;
            B = b;
            var d = a.Length;
            var min = new float[d];
            var max = new float[d];
            for (var i = 0; i < d; i++)
            {
                min[i] = Math.Min(a[i], b[i]);
                max[i] = Math.Max(a[i], b[i]);
            }
            Bounds = new BoundsND(min, max);
        }

        public float DistanceSquared(float[] p)
        {
            var d = p.Length;
            var ab = new float[d];
            var ap = new float[d];
            float dotABAB = 0f, dotAPAB = 0f;
            for (var i = 0; i < d; i++)
            {
                ab[i] = B[i] - A[i];
                ap[i] = p[i] - A[i];
                dotABAB += ab[i] * ab[i];
                dotAPAB += ap[i] * ab[i];
            }
            var t = dotAPAB / dotABAB;
            t = Math.Clamp(t, 0f, 1f);
            var sum = 0f;
            for (var i = 0; i < d; i++)
            {
                var closest = A[i] + t * ab[i];
                var diff = p[i] - closest;
                sum += diff * diff;
            }
            return sum;
        }

        public bool Intersects(RayND ray, out float t)
        {
            // For nD, use conservative bounding box intersection
            return new AxisAlignedSimplexND(Bounds.Min, Bounds.Max).Intersects(ray, out t);
        }
    }
}

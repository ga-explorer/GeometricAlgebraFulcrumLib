// HighPerformanceOctree.cs
// Generic, high-performance octree for spatial queries in graphics and simulation.
// Enhancements added:
// - Span-based, zero-allocation query API: QueryRange into a Span<T> buffer + visitor-based traversal
// - Zero-allocation visitor APIs (Action<T> / Func<T,bool> for early-out)
// - Triangle primitive with AABB, point-distance, and ray intersection (Möller–Trumbore)
// - Custom reusable binary heaps for nearest-neighbor to avoid PriorityQueue allocations
// - GPU-friendly export helper and layout suggestions in comments
// - Performance-minded small improvements and documentation

using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space3D
{
    public sealed class TrianglePrimitive : ISpatialPrimitive
    {
        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }
        public Bounds3 Bounds { get; }

        public TrianglePrimitive(Vector3 a, Vector3 b, Vector3 c)
        {
            A = a; B = b; C = c;
            var min = Vector3.Min(a, Vector3.Min(b, c));
            var max = Vector3.Max(a, Vector3.Max(b, c));
            Bounds = new Bounds3(min, max);
        }

        // Distance squared from point to triangle (exact). Uses algorithm projecting to triangle plane and checking barycentric coordinates.
        public float DistanceSquared(Vector3 p)
        {
            // from Real-Time Collision Detection (Christer Ericson)
            var ab = B - A; var ac = C - A; var ap = p - A;
            var d1 = Vector3.Dot(ab, ap);
            var d2 = Vector3.Dot(ac, ap);
            if (d1 <= 0f && d2 <= 0f) return (p - A).LengthSquared(); // barycentric (1,0,0)

            var bp = p - B;
            var d3 = Vector3.Dot(ab, bp);
            var d4 = Vector3.Dot(ac, bp);
            if (d3 >= 0f && d4 <= d3) return (p - B).LengthSquared(); // (0,1,0)

            var vc = d1 * d4 - d3 * d2;
            if (vc <= 0f && d1 >= 0f && d3 <= 0f)
            {
                var v = d1 / (d1 - d3);
                var proj = A + v * ab;
                return (p - proj).LengthSquared();
            }

            var cp = p - C;
            var d5 = Vector3.Dot(ab, cp);
            var d6 = Vector3.Dot(ac, cp);
            if (d6 >= 0f && d5 <= d6) return (p - C).LengthSquared(); // (0,0,1)

            var vb = d5 * d2 - d1 * d6;
            if (vb <= 0f && d2 >= 0f && d6 <= 0f)
            {
                var w = d2 / (d2 - d6);
                var proj = A + w * ac;
                return (p - proj).LengthSquared();
            }

            var va = d3 * d6 - d5 * d4;
            if (va <= 0f && d4 - d3 >= 0f && d5 - d6 >= 0f)
            {
                var w = (d4 - d3) / (d4 - d3 + (d5 - d6));
                var proj = B + w * (C - B);
                return (p - proj).LengthSquared();
            }

            // inside face region. Compute projection onto plane
            var normal = Vector3.Cross(ab, ac);
            var area2 = normal.Length();
            if (area2 == 0f) return Math.Min((p - A).LengthSquared(), Math.Min((p - B).LengthSquared(), (p - C).LengthSquared()));
            normal = Vector3.Normalize(normal);
            var distanceToPlane = Vector3.Dot(p - A, normal);
            return distanceToPlane * distanceToPlane;
        }

        // Ray-triangle intersection using Möller–Trumbore. Returns distance t along ray if hit.
        public bool Intersects(Ray ray, out float t)
        {
            const float EPSILON = 1e-6f;
            var edge1 = B - A;
            var edge2 = C - A;
            var h = Vector3.Cross(ray.Direction, edge2);
            var a = Vector3.Dot(edge1, h);
            if (a > -EPSILON && a < EPSILON) { t = float.PositiveInfinity; return false; } // parallel
            var f = 1.0f / a;
            var s = ray.Origin - A;
            var u = f * Vector3.Dot(s, h);
            if (u < 0.0f || u > 1.0f) { t = float.PositiveInfinity; return false; }
            var q = Vector3.Cross(s, edge1);
            var v = f * Vector3.Dot(ray.Direction, q);
            if (v < 0.0f || u + v > 1.0f) { t = float.PositiveInfinity; return false; }
            // compute t
            var tt = f * Vector3.Dot(edge2, q);
            if (tt > EPSILON)
            {
                t = tt; return true;
            }
            t = float.PositiveInfinity; return false;
        }
    }
}

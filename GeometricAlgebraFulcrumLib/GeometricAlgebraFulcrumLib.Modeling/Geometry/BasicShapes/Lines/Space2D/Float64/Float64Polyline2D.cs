using Clipper2Lib;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Collections;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64
{
    public sealed class Float64Polyline2D :
        IReadOnlyList<LinFloat64Vector2D>
    {
        public sealed class PolylineBvh
        {
            /// <summary>
            /// comparer used for median split (centroid along chosen axis) - works on implicit segments
            /// </summary>
            /// <param name="points"></param>
            /// <param name="splitX"></param>
            private sealed class SegmentCentroidComparer(IReadOnlyList<LinFloat64Vector2D> points, bool splitX) :
                IComparer<int>
            {
                public int Compare(int ia, int ib)
                {
                    var pa1 = points[ia];
                    var pa2 = points[ia + 1];
                    var pb1 = points[ib];
                    var pb2 = points[ib + 1];

                    var ca = (pa1 + pa2) * 0.5;
                    var cb = (pb1 + pb2) * 0.5;

                    return splitX ? ca.X.CompareTo(cb.X) : ca.Y.CompareTo(cb.Y);
                }
            }


            /// <summary>
            /// squared distance between two segments p1q1 and p2q2
            /// </summary>
            /// <param name="p1"></param>
            /// <param name="q1"></param>
            /// <param name="p2"></param>
            /// <param name="q2"></param>
            /// <returns></returns>
            internal static double SegmentSegmentDistSq(LinFloat64Vector2D p1, LinFloat64Vector2D q1, LinFloat64Vector2D p2, LinFloat64Vector2D q2)
            {
                // Algorithm: compute distance between two segments in 2D via standard param formulation
                var d1 = q1 - p1; // direction of segment1
                var d2 = q2 - p2; // direction of segment2
                var r = p1 - p2;
                double a = d1.VectorDot(d1); // squared length seg1
                double e = d2.VectorDot(d2); // squared length seg2
                double f = d2.VectorDot(r);

                double s, t;
                const double eps = 1e-12;

                if (a <= eps && e <= eps)
                {
                    // both segments are points
                    return p1.GetDistanceSquaredToPoint(p2);
                }

                if (a <= eps)
                {
                    // first is a point, project p1 onto seg2
                    t = Math.Clamp(f / e, 0.0, 1.0);
                    var proj = p2 + t * d2;
                    return p1.GetDistanceSquaredToPoint(proj);
                }

                if (e <= eps)
                {
                    // second is a point
                    s = Math.Clamp(-r.VectorDot(d1) / a, 0.0, 1.0);
                    var proj = p1 + s * d1;
                    return proj.GetDistanceSquaredToPoint(p2);
                }

                double c = d1.VectorDot(r);
                double b = d1.VectorDot(d2);
                var denom = a * e - b * b;

                s = denom != 0.0
                    ? Math.Clamp((b * f - c * e) / denom, 0.0, 1.0)
                    : 0.0; // parallel; choose s = 0 arbitrarily

                // compute t for the s we have
                var tNom = b * s + f;
                if (tNom <= 0.0)
                {
                    t = 0.0;
                    s = Math.Clamp(-c / a, 0.0, 1.0);
                }
                else if (tNom >= e)
                {
                    t = 1.0;
                    s = Math.Clamp((b - c) / a, 0.0, 1.0);
                }
                else
                {
                    t = tNom / e;
                }

                var c1 = p1 + s * d1;
                var c2 = p2 + t * d2;

                return c1.GetDistanceSquaredToPoint(c2);
            }


            /// <summary>
            /// BVH Node 
            /// </summary>& builder (operates on implicit segments: segment i = points[i] -> points[i+1]) --------
            internal struct Node
            {
                public SimpleAabb2D Box;

                public int Start;   // start index in _segIndices array
                public int Count;   // number of segments (if leaf)
                public int Left;    // child index (if internal)
                public int Right;   // child index (if internal)

                public bool IsLeaf
                    => Count > 0;
            }


            private const int MaxLeafSize = 4;

            /// <summary>
            /// reference to polyline points
            /// </summary>
            internal readonly IReadOnlyList<LinFloat64Vector2D> Points;

            /// <summary>
            /// permutation of segment indices used by nodes;
            /// each index refers to segment [i, i+1]
            /// </summary>
            internal readonly int[] SegIndices;
            private readonly int _segmentCount;
            internal readonly List<Node> Nodes;

            internal PolylineBvh(IReadOnlyList<LinFloat64Vector2D> points)
            {
                Points = points ?? throw new ArgumentNullException(nameof(points));

                _segmentCount = Math.Max(0, points.Count - 1);
                SegIndices = new int[_segmentCount];
                for (var i = 0; i < _segmentCount; ++i) SegIndices[i] = i;

                Nodes = new List<Node>();

                Build();
            }

            private void Build()
            {
                Nodes.Clear();
                if (_segmentCount == 0) return;

                BuildRecursive(0, _segmentCount);
            }

            private int BuildRecursive(int start, int count)
            {
                var node = new Node
                {
                    Start = start,
                    Count = 0,
                    Left = -1,
                    Right = -1
                };

                // compute bounding box for segments [start, start+count)
                var box = SimpleAabb2D.Create(
                    (double.PositiveInfinity, double.PositiveInfinity),
                    (double.NegativeInfinity, double.NegativeInfinity)
                );

                for (var i = start; i < start + count; ++i)
                {
                    var segIdx = SegIndices[i];
                    var p = Points[segIdx];
                    var q = Points[segIdx + 1];
                    box.Extend(p);
                    box.Extend(q);
                }
                node.Box = box;

                var nodeIndex = Nodes.Count;
                Nodes.Add(node); // placeholder

                if (count <= MaxLeafSize)
                {
                    node = Nodes[nodeIndex];
                    node.Count = count;
                    node.Start = start;
                    Nodes[nodeIndex] = node;
                    return nodeIndex;
                }

                // split by longest axis median
                var dx = box.Max.X - box.Min.X;
                var dy = box.Max.Y - box.Min.Y;
                var splitX = dx >= dy;

                var mid = start + count / 2;

                // median-by-centroid using Array.Sort on _segIndices slice with comparer
                Array.Sort(SegIndices, start, count, new SegmentCentroidComparer(Points, splitX));

                var leftChild = BuildRecursive(start, mid - start);
                var rightChild = BuildRecursive(mid, start + count - mid);

                node = Nodes[nodeIndex];
                node.Count = 0; // internal
                node.Left = leftChild;
                node.Right = rightChild;
                Nodes[nodeIndex] = node;

                // update parent's box to encompass children (should already be encompassed)
                var combined = Nodes[leftChild].Box;
                combined.Extend(Nodes[rightChild].Box.Min);
                combined.Extend(Nodes[rightChild].Box.Max);
                node.Box = combined;
                Nodes[nodeIndex] = node;

                return nodeIndex;
            }


            internal void TraverseAgainst(PolylineBvh other, ref double bestSq)
            {
                if (_segmentCount == 0 || other._segmentCount == 0)
                    return;

                TraverseNodePair(0, 0, other, ref bestSq);
            }

            /// <summary>
            /// Pairwise traversal with prune
            /// </summary>
            /// <param name="idxA"></param>
            /// <param name="idxB"></param>
            /// <param name="other"></param>
            /// <param name="bestSq"></param>
            private void TraverseNodePair(int idxA, int idxB, PolylineBvh other, ref double bestSq)
            {
                var nodeA = Nodes[idxA];
                var nodeB = other.Nodes[idxB];

                var lowBound = nodeA.Box.GetDistanceSquared(in nodeB.Box);
                if (lowBound >= bestSq) return; // prune

                if (nodeA.IsLeaf && nodeB.IsLeaf)
                {
                    // exact checks (iterate segment indices and compute segment-segment distances on-the-fly)
                    for (var i = nodeA.Start; i < nodeA.Start + nodeA.Count; ++i)
                    {
                        var segAi = SegIndices[i];
                        var aP1 = Points[segAi];
                        var aP2 = Points[segAi + 1];

                        for (var j = nodeB.Start; j < nodeB.Start + nodeB.Count; ++j)
                        {
                            var segBj = other.SegIndices[j];
                            var bP1 = other.Points[segBj];
                            var bP2 = other.Points[segBj + 1];

                            var d2 = SegmentSegmentDistSq(aP1, aP2, bP1, bP2);
                            if (d2 < bestSq) bestSq = d2;
                        }
                    }
                    return;
                }

                // Not both leaves: choose which to expand to get better pruning order.
                // We get the child pairs and their lower bounds, visit smaller bound first.
                if (!nodeA.IsLeaf && !nodeB.IsLeaf)
                {
                    // four combos: A.left vs B.left, A.left vs B.right, A.right vs B.left, A.right vs B.right
                    int aL = nodeA.Left, aR = nodeA.Right, bL = nodeB.Left, bR = nodeB.Right;
                    var node = Nodes[aL];
                    var otherNode = other.Nodes[bL];
                    var otherNode1 = other.Nodes[bR];
                    var node1 = Nodes[aR];
                    var pairs = new (int aIdx, int bIdx, double lb)[]
                    {
                (aL, bL, node.Box.GetDistanceSquared(in otherNode.Box)),
                (aL, bR, node.Box.GetDistanceSquared(in otherNode1.Box)),
                (aR, bL, node1.Box.GetDistanceSquared(in otherNode.Box)),
                (aR, bR, node1.Box.GetDistanceSquared(in otherNode1.Box))
                    };

                    Array.Sort(pairs, (x, y) => x.lb.CompareTo(y.lb));

                    foreach (var p in pairs)
                    {
                        if (p.lb >= bestSq) continue;
                        TraverseNodePair(p.aIdx, p.bIdx, other, ref bestSq);
                    }
                }
                else if (!nodeA.IsLeaf) // node A internal, node B leaf
                {
                    int aL = nodeA.Left, aR = nodeA.Right;
                    var node = Nodes[aL];
                    var lbL = node.Box.GetDistanceSquared(in nodeB.Box);
                    var node1 = Nodes[aR];
                    var lbR = node1.Box.GetDistanceSquared(in nodeB.Box);
                    if (lbL < lbR)
                    {
                        if (lbL < bestSq) TraverseNodePair(aL, idxB, other, ref bestSq);
                        if (lbR < bestSq) TraverseNodePair(aR, idxB, other, ref bestSq);
                    }
                    else
                    {
                        if (lbR < bestSq) TraverseNodePair(aR, idxB, other, ref bestSq);
                        if (lbL < bestSq) TraverseNodePair(aL, idxB, other, ref bestSq);
                    }
                }
                else // node A leaf, node B internal
                {
                    int bL = nodeB.Left, bR = nodeB.Right;
                    var otherNode = other.Nodes[bL];
                    var lbL = nodeA.Box.GetDistanceSquared(in otherNode.Box);
                    var node = other.Nodes[bR];
                    var lbR = nodeA.Box.GetDistanceSquared(in node.Box);
                    if (lbL < lbR)
                    {
                        if (lbL < bestSq) TraverseNodePair(idxA, bL, other, ref bestSq);
                        if (lbR < bestSq) TraverseNodePair(idxA, bR, other, ref bestSq);
                    }
                    else
                    {
                        if (lbR < bestSq) TraverseNodePair(idxA, bR, other, ref bestSq);
                        if (lbL < bestSq) TraverseNodePair(idxA, bL, other, ref bestSq);
                    }
                }
            }


            internal static bool TraverseNodePairForHasSelfIntersection(int idxA, int idxB, PolylineBvh bvh)
            {
                var nodeA = bvh.Nodes[idxA];
                var nodeB = bvh.Nodes[idxB];

                // Skip non-overlapping bounding boxes
                if (!nodeA.Box.Overlaps(nodeB.Box))
                    return false;

                // --- Leaf vs Leaf case ---
                if (nodeA.IsLeaf && nodeB.IsLeaf)
                {
                    for (var i = nodeA.Start; i < nodeA.Start + nodeA.Count; ++i)
                    {
                        var segAi = bvh.SegIndices[i];
                        var aP1 = bvh.Points[segAi];
                        var aP2 = bvh.Points[segAi + 1];

                        for (var j = nodeB.Start; j < nodeB.Start + nodeB.Count; ++j)
                        {
                            var segBj = bvh.SegIndices[j];
                            var bP1 = bvh.Points[segBj];
                            var bP2 = bvh.Points[segBj + 1];

                            // ✅ Skip adjacent or identical segments
                            if (Math.Abs(segAi - segBj) <= 1)
                                continue;

                            // Also skip wraparound adjacency in closed polylines
                            if (segAi == 0 && segBj == bvh.Points.Count - 2)
                                continue;

                            if (TrySegmentSegmentIntersection(aP1, aP2, bP1, bP2, out _))
                                return true; // Found first self-intersection
                        }
                    }

                    return false;
                }

                // --- Recursive descent ---
                if (!nodeA.IsLeaf && !nodeB.IsLeaf)
                {
                    // Avoid redundant double-traversal (A,B) and (B,A)
                    if (idxA == idxB)
                    {
                        // only traverse children combinations once
                        return TraverseNodePairForHasSelfIntersection(nodeA.Left, nodeA.Right, bvh);
                    }

                    // Both internal nodes: recurse into all combinations
                    return
                        TraverseNodePairForHasSelfIntersection(nodeA.Left, nodeB.Left, bvh) ||
                        TraverseNodePairForHasSelfIntersection(nodeA.Left, nodeB.Right, bvh) ||
                        TraverseNodePairForHasSelfIntersection(nodeA.Right, nodeB.Left, bvh) ||
                        TraverseNodePairForHasSelfIntersection(nodeA.Right, nodeB.Right, bvh);
                }

                if (!nodeA.IsLeaf)
                {
                    return
                        TraverseNodePairForHasSelfIntersection(nodeA.Left, idxB, bvh) ||
                        TraverseNodePairForHasSelfIntersection(nodeA.Right, idxB, bvh);
                }

                return
                    TraverseNodePairForHasSelfIntersection(idxA, nodeB.Left, bvh) ||
                    TraverseNodePairForHasSelfIntersection(idxA, nodeB.Right, bvh);
            }
            internal static void TraverseNodePairForSelfIntersections(
    int idxA, int idxB,
    PolylineBvh bvh,
    List<LinFloat64Vector2D> results)
            {
                var nodeA = bvh.Nodes[idxA];
                var nodeB = bvh.Nodes[idxB];

                // Skip if bounding boxes do not overlap
                if (!nodeA.Box.Overlaps(nodeB.Box))
                    return;

                // --- Leaf vs Leaf case ---
                if (nodeA.IsLeaf && nodeB.IsLeaf)
                {
                    for (var i = nodeA.Start; i < nodeA.Start + nodeA.Count; ++i)
                    {
                        var segAi = bvh.SegIndices[i];
                        var aP1 = bvh.Points[segAi];
                        var aP2 = bvh.Points[segAi + 1];

                        for (var j = nodeB.Start; j < nodeB.Start + nodeB.Count; ++j)
                        {
                            var segBj = bvh.SegIndices[j];
                            var bP1 = bvh.Points[segBj];
                            var bP2 = bvh.Points[segBj + 1];

                            // Skip same or adjacent segments (shared vertex)
                            if (Math.Abs(segAi - segBj) <= 1)
                                continue;

                            // Skip wrap-around adjacency in closed polylines
                            if (segAi == 0 && segBj == bvh.Points.Count - 2)
                                continue;

                            if (TrySegmentSegmentIntersection(aP1, aP2, bP1, bP2, out var pt))
                            {
                                // Avoid duplicates (within tolerance)
                                if (!results.Any(p => p.GetDistanceToPoint(pt) < 1e-12))
                                    results.Add(pt);
                            }
                        }
                    }

                    return;
                }

                // --- Internal traversal ---
                if (!nodeA.IsLeaf && !nodeB.IsLeaf)
                {
                    if (idxA == idxB)
                    {
                        TraverseNodePairForSelfIntersections(nodeA.Left, nodeA.Right, bvh, results);
                    }
                    else
                    {
                        TraverseNodePairForSelfIntersections(nodeA.Left, nodeB.Left, bvh, results);
                        TraverseNodePairForSelfIntersections(nodeA.Left, nodeB.Right, bvh, results);
                        TraverseNodePairForSelfIntersections(nodeA.Right, nodeB.Left, bvh, results);
                        TraverseNodePairForSelfIntersections(nodeA.Right, nodeB.Right, bvh, results);
                    }

                    return;
                }

                if (!nodeA.IsLeaf)
                {
                    TraverseNodePairForSelfIntersections(nodeA.Left, idxB, bvh, results);
                    TraverseNodePairForSelfIntersections(nodeA.Right, idxB, bvh, results);
                }
                else
                {
                    TraverseNodePairForSelfIntersections(idxA, nodeB.Left, bvh, results);
                    TraverseNodePairForSelfIntersections(idxA, nodeB.Right, bvh, results);
                }
            }

            internal static void TraverseNodePairForIntersections(int idxA, int idxB, PolylineBvh bvhA, PolylineBvh bvhB, List<LinFloat64Vector2D> results)
            {
                var nodeA = bvhA.Nodes[idxA];
                var nodeB = bvhB.Nodes[idxB];

                // prune if bounding boxes do not overlap
                if (!nodeA.Box.Overlaps(nodeB.Box))
                    return;

                if (nodeA.IsLeaf && nodeB.IsLeaf)
                {
                    // brute-force check inside leaf nodes
                    for (var i = nodeA.Start; i < nodeA.Start + nodeA.Count; ++i)
                    {
                        var segAi = bvhA.SegIndices[i];
                        var aP1 = bvhA.Points[segAi];
                        var aP2 = bvhA.Points[segAi + 1];

                        for (var j = nodeB.Start; j < nodeB.Start + nodeB.Count; ++j)
                        {
                            var segBj = bvhB.SegIndices[j];
                            var bP1 = bvhB.Points[segBj];
                            var bP2 = bvhB.Points[segBj + 1];

                            if (TrySegmentSegmentIntersection(aP1, aP2, bP1, bP2, out var p))
                                results.Add(p);
                        }
                    }
                    return;
                }

                // recursive traversal
                if (!nodeA.IsLeaf && !nodeB.IsLeaf)
                {
                    TraverseNodePairForIntersections(nodeA.Left, nodeB.Left, bvhA, bvhB, results);
                    TraverseNodePairForIntersections(nodeA.Left, nodeB.Right, bvhA, bvhB, results);
                    TraverseNodePairForIntersections(nodeA.Right, nodeB.Left, bvhA, bvhB, results);
                    TraverseNodePairForIntersections(nodeA.Right, nodeB.Right, bvhA, bvhB, results);
                }
                else if (!nodeA.IsLeaf)
                {
                    TraverseNodePairForIntersections(nodeA.Left, idxB, bvhA, bvhB, results);
                    TraverseNodePairForIntersections(nodeA.Right, idxB, bvhA, bvhB, results);
                }
                else // nodeB internal
                {
                    TraverseNodePairForIntersections(idxA, nodeB.Left, bvhA, bvhB, results);
                    TraverseNodePairForIntersections(idxA, nodeB.Right, bvhA, bvhB, results);
                }
            }

            internal static bool TraverseNodePairForHasIntersection(int idxA, int idxB, PolylineBvh bvhA, PolylineBvh bvhB)
            {
                var nodeA = bvhA.Nodes[idxA];
                var nodeB = bvhB.Nodes[idxB];

                // prune if bounding boxes do not overlap
                if (!nodeA.Box.Overlaps(nodeB.Box))
                    return false;

                if (nodeA.IsLeaf && nodeB.IsLeaf)
                {
                    // brute-force check inside leaf nodes
                    for (var i = nodeA.Start; i < nodeA.Start + nodeA.Count; ++i)
                    {
                        var segAi = bvhA.SegIndices[i];
                        var aP1 = bvhA.Points[segAi];
                        var aP2 = bvhA.Points[segAi + 1];

                        for (var j = nodeB.Start; j < nodeB.Start + nodeB.Count; ++j)
                        {
                            var segBj = bvhB.SegIndices[j];
                            var bP1 = bvhB.Points[segBj];
                            var bP2 = bvhB.Points[segBj + 1];

                            if (TrySegmentSegmentIntersection(aP1, aP2, bP1, bP2, out _))
                                return true; // stop at first hit
                        }
                    }
                    return false;
                }

                // recursive traversal
                if (!nodeA.IsLeaf && !nodeB.IsLeaf)
                {
                    return TraverseNodePairForHasIntersection(nodeA.Left, nodeB.Left, bvhA, bvhB) || TraverseNodePairForHasIntersection(nodeA.Left, nodeB.Right, bvhA, bvhB) || TraverseNodePairForHasIntersection(nodeA.Right, nodeB.Left, bvhA, bvhB) || TraverseNodePairForHasIntersection(nodeA.Right, nodeB.Right, bvhA, bvhB);
                }
                else if (!nodeA.IsLeaf)
                {
                    return TraverseNodePairForHasIntersection(nodeA.Left, idxB, bvhA, bvhB) || TraverseNodePairForHasIntersection(nodeA.Right, idxB, bvhA, bvhB);
                }
                else // nodeB internal
                {
                    return TraverseNodePairForHasIntersection(idxA, nodeB.Left, bvhA, bvhB) || TraverseNodePairForHasIntersection(idxA, nodeB.Right, bvhA, bvhB);
                }
            }

            internal static bool TraversePolylineAgainstSegmentForHasIntersection(
                int idx,
                PolylineBvh bvhPoly,
                LinFloat64Vector2D segP1,
                LinFloat64Vector2D segP2,
                SimpleAabb2D segBox)
            {
                var node = bvhPoly.Nodes[idx];

                if (!node.Box.Overlaps(segBox))
                    return false;

                if (node.IsLeaf)
                {
                    for (var i = node.Start; i < node.Start + node.Count; ++i)
                    {
                        var segIdx = bvhPoly.SegIndices[i];
                        var p1 = bvhPoly.Points[segIdx];
                        var p2 = bvhPoly.Points[segIdx + 1];

                        if (TrySegmentSegmentIntersection(segP1, segP2, p1, p2, out _))
                            return true;
                    }
                    return false;
                }

                return
                    TraversePolylineAgainstSegmentForHasIntersection(node.Left, bvhPoly, segP1, segP2, segBox) ||
                    TraversePolylineAgainstSegmentForHasIntersection(node.Right, bvhPoly, segP1, segP2, segBox);
            }

            internal static void TraversePolylineAgainstSegment(
                int idx,
                PolylineBvh bvhPoly,
                LinFloat64Vector2D segP1,
                LinFloat64Vector2D segP2,
                SimpleAabb2D segBox,
                List<LinFloat64Vector2D> results)
            {
                var node = bvhPoly.Nodes[idx];

                // prune if bounding boxes do not overlap
                if (!node.Box.Overlaps(segBox))
                    return;

                if (node.IsLeaf)
                {
                    // check all segments in this leaf
                    for (var i = node.Start; i < node.Start + node.Count; ++i)
                    {
                        var segIdx = bvhPoly.SegIndices[i];
                        var p1 = bvhPoly.Points[segIdx];
                        var p2 = bvhPoly.Points[segIdx + 1];

                        if (TrySegmentSegmentIntersection(segP1, segP2, p1, p2, out var inter))
                            results.Add(inter);
                    }
                }
                else
                {
                    TraversePolylineAgainstSegment(node.Left, bvhPoly, segP1, segP2, segBox, results);
                    TraversePolylineAgainstSegment(node.Right, bvhPoly, segP1, segP2, segBox, results);
                }
            }

        }

        public sealed record VertexData
        {
            public int Index { get; }

            public LinFloat64Vector2D Point { get; }

            public LinFloat64Vector2D Tangent { get; }

            public double CumulativeLength { get; }


            internal VertexData(int index, LinFloat64Vector2D point, LinFloat64Vector2D tangent, double cumLength)
            {
                Index = index;
                Point = point;
                Tangent = tangent;
                CumulativeLength = cumLength;
            }
        }

        public sealed class PathMetrics
        {
            public Float64Polyline2D Path { get; }

            public Float64LineSegment2DBvh Obstacles { get; }

            public int PointCount 
                => Path.Count;

            public double Length 
                => Path.Length;


            internal PathMetrics(Float64Polyline2D path, Float64LineSegment2DBvh obstacles)
            {
                Path = path;
                Obstacles = obstacles;
            }


            public double GetMinObstacleDistance()
            {
                return Path.GetShortestDistanceTo(Obstacles);
            }

            public double GetMaxVertexAngleRadians()
            {
                return Path
                    .GetVertexTangentAngles()
                    .Max(a => a.RadiansValue);
            }
            
            public double GetMaxVertexAngleDegrees()
            {
                return Path
                    .GetVertexTangentAngles()
                    .Max(a => a.DegreesValue);
            }


            public override string ToString()
            {
                return new StringBuilder()
                    .AppendLine($"Point count: {PointCount}")
                    .AppendLine($"Length: {Length:G}")
                    .AppendLine($"Min obstacle distance: {GetMinObstacleDistance():G}")
                    .AppendLine($"Max vertex angle: {GetMaxVertexAngleDegrees():G} degrees")
                    .AppendLine()
                    .ToString();
            }
        }


        private readonly LinFloat64Vector2D[] _pointArray;
        private readonly LinFloat64Vector2D[] _tangentArray;
        private readonly double[] _segmentLengthArray;
        private readonly double[] _cumLengthArray;
        private PolylineBvh? _bvh;


        public bool IsClosed { get; }

        public int SegmentCount 
            => _pointArray.Length - 1;

        public int Count
            => _pointArray.Length;

        public LinFloat64Vector2D this[int index]
            => _pointArray[index];

        public double Length { get; }

        public PolylineBvh Bvh
        {
            get
            {
                _bvh ??= new PolylineBvh(_pointArray);

                return _bvh;
            }
        }


        public Float64Polyline2D(params LinFloat64Vector2D[] pointList)
            : this((IEnumerable<LinFloat64Vector2D>)pointList)
        {
        }

        public Float64Polyline2D(IEnumerable<LinFloat64Vector2D> pointList)
        {
            _pointArray = pointList.ToArray();

            if (_pointArray.Length < 2)
                throw new InvalidOperationException();

            IsClosed = _pointArray[0].Equals(_pointArray[^1]);

            _tangentArray = new LinFloat64Vector2D[_pointArray.Length];
            _tangentArray[0] = ComputeVertexTangent(_pointArray, 0);

            _segmentLengthArray = new double[_pointArray.Length - 1];

            _cumLengthArray = new double[_pointArray.Length];
            _cumLengthArray[0] = 0;

            var length = 0d;
            var p0 = _pointArray[0];
            for (var i = 1; i < _pointArray.Length; i++)
            {
                var p1 = _pointArray[i];
                var segmentLength = p1.GetDistanceToPoint(p0);
                length += segmentLength;

                _segmentLengthArray[i - 1] = segmentLength;
                _cumLengthArray[i] = length;
                _tangentArray[i] = ComputeVertexTangent(_pointArray, i);

                p0 = p1;
            }

            Length = length;
        }


        private static LinFloat64Vector2D ComputeVertexTangent(LinFloat64Vector2D[] pointArray, int index)
        {
            if (index == 0)
            {
                var tangent = pointArray[1] - pointArray[0];
                var tangentNorm = tangent.L2Norm();

                return tangentNorm.IsNearZero()
                    ? LinFloat64Vector2D.Zero
                    : tangent / tangentNorm;
            }

            if (index == pointArray.Length - 1)
            {
                var tangent = pointArray[^1] - pointArray[^2];
                var tangentNorm = tangent.L2Norm();

                return tangentNorm.IsNearZero()
                    ? LinFloat64Vector2D.Zero
                    : tangent / tangentNorm;
            }

            {
                var tangent1 = pointArray[index + 1] - pointArray[index];
                var tangent2 = pointArray[index] - pointArray[index - 1];

                var norm1 = tangent1.L2Norm();
                var norm2 = tangent2.L2Norm();

                tangent1 = norm1.IsNearZero() ? LinFloat64Vector2D.Zero : tangent1 / norm1;
                tangent2 = norm2.IsNearZero() ? LinFloat64Vector2D.Zero : tangent2 / norm2;

                var tangent = tangent1 + tangent2;
                var tangentNorm = tangent.L2Norm();

                return tangentNorm.IsNearZero()
                    ? LinFloat64Vector2D.Zero
                    : tangent / tangentNorm;
            }
        }

        /// <summary>
        /// Find segment index where s in [s_i, s_{i+1})
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private int FindSegmentIndex(double distance)
        {
            // binary search on _cumLengths
            var low = 0;
            var high = _cumLengthArray.Length - 2;
            while (low <= high)
            {
                var mid = (low + high) >> 1;

                if (distance < _cumLengthArray[mid])
                    high = mid - 1;
                else if (distance >= _cumLengthArray[mid + 1])
                    low = mid + 1;
                else
                    return mid;
            }

            return Math.Max(0, Math.Min(_cumLengthArray.Length - 2, low));
        }

        /// <summary>
        /// Clamp s and return segment and local u
        /// </summary>
        /// <param name="s"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        private int ClampSegmentIndexForS(double s, out double u)
        {
            if (s <= 0)
            {
                u = 0;
                return 0;
            }

            if (s >= Length)
            {
                u = 1;
                return _cumLengthArray.Length - 2;
            }

            var seg = FindSegmentIndex(s);
            var s0 = _cumLengthArray[seg];
            var length = _segmentLengthArray[seg];

            u = length > 0 ? (s - s0) / length : 0f;
            return seg;
        }


        /// <summary>
        /// Get exact polyline position at arc length s (clamped)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public LinFloat64Vector2D GetPoint(double s)
        {
            if (s <= 0) return _pointArray[0];
            if (s >= Length) return _pointArray[^1];

            var segmentIndex = FindSegmentIndex(s);
            var s0 = _cumLengthArray[segmentIndex];
            var length = _segmentLengthArray[segmentIndex];
            var u = length > 0 ? (s - s0) / length : 0f;

            return u.Lerp(
                _pointArray[segmentIndex],
                _pointArray[segmentIndex + 1]
            );
        }

        /// <summary>
        /// Get smoothed tangent (unit) at arc length s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public LinFloat64Vector2D GetTangentVector(double s)
        {
            var seg = ClampSegmentIndexForS(s, out var u);

            var t0 = _tangentArray[seg];
            var t1 = _tangentArray[seg + 1];

            //var angle = t0.GetAngleCCw(t1).AngleTimes(u);
            //return t0.RotateBy(angle);

            return u.Lerp(t0, t1);
        }

        /// <summary>
        /// Get smoothed tangent (unit) at arc length s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public LinFloat64Vector2D GetUnitTangentVector(double s)
        {
            var seg = ClampSegmentIndexForS(s, out var u);

            var t0 = _tangentArray[seg];
            var t1 = _tangentArray[seg + 1];

            //var angle = t0.GetAngleCCw(t1).AngleTimes(u);
            //return t0.RotateBy(angle);

            var v = u.Lerp(t0, t1);
            var vNorm = v.L2Norm();

            return vNorm.IsNearZero() ? LinFloat64Vector2D.Zero : v / vNorm;
        }

        public LinFloat64Vector2D GetAccelerationVector(double s)
        {
            // Clamp s to polyline domain
            if (s <= 0) s = 0;
            if (s >= Length) s = Length;

            // Choose a step for numerical derivative.
            // Step size relative to overall polyline length to avoid absolute underflow/overflow.
            // Tunable constants: 1e-6 and 1e-9 are conservative choices.
            var eps = Math.Max(1e-8 * Math.Max(Length, 1.0), 1e-9);

            // If the polyline is very short, reduce eps to a small absolute value
            eps = Math.Min(eps, Length * 0.1); // never step more than 10% of length

            // Use central difference when possible, otherwise forward/backward
            LinFloat64Vector2D tPlus, tMinus;

            if (s - eps >= 0 && s + eps <= Length)
            {
                tPlus = GetTangentVector(s + eps);
                tMinus = GetTangentVector(s - eps);

                // central difference
                return (tPlus - tMinus) / (2.0 * eps);
            }

            if (s - eps < 0 && s + eps <= Length) // near start: forward difference
            {
                tPlus = GetTangentVector(s + eps);
                var t0 = GetTangentVector(s);

                return (tPlus - t0) / eps;
            }

            if (s + eps > Length && s - eps >= 0) // near end: backward difference
            {
                var t0 = GetTangentVector(s);
                tMinus = GetTangentVector(s - eps);

                return (t0 - tMinus) / eps;
            }

            // Fallback (degenerate): return zero vector
            return LinFloat64Vector2D.Zero;
        }


        public LinFloat64Vector2D GetVertexPoint(int index)
        {
            return _pointArray[index];
        }
        
        public LinFloat64Vector2D GetVertexTangentVector(int index)
        {
            return _tangentArray[index];
        }

        public VertexData GetVertexData(int index)
        {
            return new VertexData(
                index,
                _pointArray[index],
                _tangentArray[index],
                _cumLengthArray[index]
            );
        }

        public IReadOnlyList<VertexData> GetVertexData()
        {
            return Count.GetRange(GetVertexData).ToArray();
        }
        
        /// <summary>
        /// Get the angles between each two consecutive tangents at each vertex
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LinFloat64PolarAngle> GetVertexTangentAngles()
        {
            for (var i = 1; i < _pointArray.Length - 1; i++)
            {
                var p1 = _pointArray[i - 1];
                var p2 = _pointArray[i];
                var p3 = _pointArray[i + 1];

                var v1 = p2 - p1;
                var v2 = p3 - p2;

                yield return v1.GetAngle(v2);
            }
        }

        public IEnumerable<LinFloat64PolarAngle> GetVertexAngles()
        {
            for (var i = 1; i < _pointArray.Length - 1; i++)
            {
                var p1 = _pointArray[i - 1];
                var p2 = _pointArray[i];
                var p3 = _pointArray[i + 1];

                var v1 = p1 - p2;
                var v2 = p3 - p2;

                yield return v1.GetAngle(v2);
            }
        }

        public IEnumerable<LinFloat64PolarAngle> GetHodographCcwAngles()
        {
            for (var i = 1; i < _pointArray.Length - 1; i++)
            {
                var p1 = _pointArray[i - 1];
                var p2 = _pointArray[i];
                var p3 = _pointArray[i + 1];

                var v1 = p2 - p1;
                var v2 = p3 - p2;

                yield return v1.GetAngleCCw(v2);
            }
        }
        
        public Float64LineSegment2D GetSegment(int index)
        {
            var p1 = _pointArray[index];
            var p2 = _pointArray[index + 1];

            return Float64LineSegment2D.Create(p1, p2);
        }

        public IEnumerable<Float64LineSegment2D> GetSegments()
        {
            var p1 = _pointArray[0];

            for (var i = 1; i < _pointArray.Length; i++)
            {
                var p2 = _pointArray[i];

                yield return Float64LineSegment2D.Create(p1, p2);

                p1 = p2;
            }
        }

        public Float64Polyline2D MapUsing(IFloat64AffineMap2D affineMap)
        {
            return new Float64Polyline2D(
                _pointArray.Select(
                    p => affineMap.MapPoint(p)
                )
            );
        }

        public Float64Polyline2D ReverseCurve()
        {
            return new Float64Polyline2D(
                _pointArray.Reverse()
            );
        }

        public Float64Polyline2D GetSubPolyline(int index1, int index2)
        {
            if (index1 < 0 || index2 > Count - 1 || index1 >= index2)
                throw new ArgumentOutOfRangeException();

            var n = index2 - index1 + 1;
            var pointArray = new LinFloat64Vector2D[n];

            Array.Copy(_pointArray, index1, pointArray, 0, n);

            return new Float64Polyline2D(pointArray);
        }

        public Pair<Float64Polyline2D> SplitAtPoint(int index)
        {
            if (index < 1 || index > Count - 2)
                throw new ArgumentOutOfRangeException(nameof(index));

            var pointArray1 = new LinFloat64Vector2D[index + 1];
            var pointArray2 = new LinFloat64Vector2D[Count - index];

            Array.Copy(_pointArray, 0, pointArray1, 0, pointArray1.Length);
            Array.Copy(_pointArray, index, pointArray2, 0, pointArray2.Length);

            return new Pair<Float64Polyline2D>(
                new Float64Polyline2D(pointArray1),
                new Float64Polyline2D(pointArray2)
            );
        }

        public Pair<Float64Polyline2D> SplitAtLength(double length, double snapTolerance = 1e-3)
        {
            if (length <= 0 || length >= Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            var accumulated = 0d;
            var splitIndex = -1;
            LinFloat64Vector2D splitPoint = default;

            for (var i = 0; i < _pointArray.Length - 1; i++)
            {
                var p0 = _pointArray[i];
                var p1 = _pointArray[i + 1];
                var segLength = p0.GetDistanceToPoint(p1);

                if (accumulated + segLength >= length)
                {
                    var remaining = length - accumulated;
                    var t = remaining / segLength;

                    // Interpolated split point
                    splitPoint = p0 + t * (p1 - p0);

                    // Snap to existing vertices if close
                    if (splitPoint.GetDistanceToPoint(p0) <= snapTolerance)
                        splitPoint = p0;

                    else if (splitPoint.GetDistanceToPoint(p1) <= snapTolerance)
                        splitPoint = p1;

                    splitIndex = i;
                    break;
                }

                accumulated += segLength;
            }

            if (splitIndex < 0)
                throw new InvalidOperationException("Failed to split polyline.");

            // Build new point lists
            var firstPoints = new List<LinFloat64Vector2D>();
            for (var i = 0; i <= splitIndex; i++)
                firstPoints.Add(_pointArray[i]);

            if (!firstPoints[^1].Equals(splitPoint))
                firstPoints.Add(splitPoint);

            var secondPoints = new List<LinFloat64Vector2D>();
            if (!splitPoint.Equals(_pointArray[splitIndex + 1]))
                secondPoints.Add(splitPoint);

            for (var i = splitIndex + 1; i < _pointArray.Length; i++)
                secondPoints.Add(_pointArray[i]);

            return new Pair<Float64Polyline2D>(
                new Float64Polyline2D(firstPoints),
                new Float64Polyline2D(secondPoints)
            );
        }

        public IReadOnlyList<Float64Polyline2D> Split(int polylineCount)
        {
            var length = Length / polylineCount;
            var polylineArray = new Float64Polyline2D[polylineCount];

            var polyline = this;

            for (var i = 0; i < polylineCount - 1; i++)
            {
                (polylineArray[i], polyline) = polyline.SplitAtLength(length);
            }

            polylineArray[^1] = polyline;

            return polylineArray;
        }

        public Float64Polyline2D CreateRandom(Float64LineSegment2DBvh obstacles, double safetyDistance, Random? randomGenerator = null)
        {
            randomGenerator ??= new Random();

            var pointList = new List<LinFloat64Vector2D>(Count) { _pointArray[0] };
            var endPoint = _pointArray[^1];
            var lastPoint = pointList[0];

            for (var i = 1; i < _pointArray.Length - 1; i++)
            {
                var center = _pointArray[i];
                var maxRadius = obstacles.GetShortestDistanceToPoint(center) - safetyDistance;

                if (maxRadius <= 0 || obstacles.GetShortestDistanceToSegment(lastPoint, center) - safetyDistance < 0)
                {
                    pointList.Add(center);
                    lastPoint = center;
                    
                    if (obstacles.GetShortestDistanceToSegment(lastPoint, endPoint) - safetyDistance >= 0)
                    {
                        pointList.Add(endPoint);

                        return new Float64Polyline2D(pointList);
                    }

                    continue;
                }

                if (center.GetDistanceToPoint(lastPoint) <= maxRadius)
                    continue;

                while (true)
                {
                    var point =
                        randomGenerator.GetPointInsideCircle(center, maxRadius);

                    if (obstacles.GetShortestDistanceToSegment(lastPoint, point) - safetyDistance < 0) 
                        continue;

                    pointList.Add(point);
                    lastPoint = point;
                    break;
                }

                if (obstacles.GetShortestDistanceToSegment(lastPoint, endPoint) - safetyDistance >= 0)
                {
                    pointList.Add(endPoint);

                    return new Float64Polyline2D(pointList);
                }
            }

            pointList.Add(endPoint);

            return new Float64Polyline2D(pointList);
        }
        
        public Float64Polyline2D CreateFromGene(double[] gene, Float64LineSegment2DBvh obstacles, double safetyDistance)
        {
            if (gene.Length != 2 * (_pointArray.Length - 2))
                throw new InvalidOperationException();

            var pointList = new List<LinFloat64Vector2D>(Count) { _pointArray[0] };
            var endPoint = _pointArray[^1];
            var lastPoint = pointList[0];

            var j = 0;
            for (var i = 1; i < _pointArray.Length - 1; i++)
            {
                var center = _pointArray[i];
                var maxRadius = obstacles.GetShortestDistanceToPoint(center) - safetyDistance;

                if (maxRadius <= 0 || obstacles.GetShortestDistanceToSegment(lastPoint, center) - safetyDistance < 0)
                {
                    pointList.Add(center);
                    lastPoint = center;
                    
                    if (obstacles.GetShortestDistanceToSegment(lastPoint, endPoint) - safetyDistance >= 0)
                    {
                        pointList.Add(endPoint);

                        return new Float64Polyline2D(pointList);
                    }

                    continue;
                }

                if (center.GetDistanceToPoint(lastPoint) <= maxRadius)
                    continue;

                var r = maxRadius * gene[j];
                var a = Math.Tau * gene[j + 1];
                j += 2;

                var point = LinFloat64Vector2D.Create(
                    center.X + r * Math.Cos(a),
                    center.Y + r * Math.Sin(a)
                );

                pointList.Add(point);
                lastPoint = point;

                if (obstacles.GetShortestDistanceToSegment(lastPoint, endPoint) - safetyDistance >= 0)
                {
                    pointList.Add(endPoint);

                    return new Float64Polyline2D(pointList);
                }
            }

            pointList.Add(endPoint);

            return new Float64Polyline2D(pointList);
        }

        public Float64Polyline2D IncreaseVertexAngles(Float64LineSegment2DBvh obstacles, double safetyDistance)
        {
            var pointArray = new List<LinFloat64Vector2D>(2 * Count)
            {
                _pointArray[0]
            };

            for (var i = 1; i < _pointArray.Length - 1; i++)
            {
                var p1 = _pointArray[i - 1];
                var p2 = _pointArray[i];
                var p3 = _pointArray[i + 1];

                var r = 
                    obstacles.GetShortestDistanceToPoint(p2) - safetyDistance;

                if (r < 0)
                {
                    pointArray.Add(_pointArray[i]);
                    continue;
                }

                var u12 = p1 - p2;
                var u32 = p3 - p2;

                var l = Math.Min(
                    Math.Min(u12.L2Norm(), u32.L2Norm()), 
                    r / u12.GetAngle(u32).HalfPolarAngle().CosValue
                ) / 3;

                pointArray.Add(p2 + u12.SetLength(l));
                pointArray.Add(p2 + u32.SetLength(l));
            }

            pointArray.Add(_pointArray[^1]);

            return new Float64Polyline2D(pointArray);
        }

        public Path64 ToClipper2Path64(long scalingFactor = 1000000)
        {
            var path = new Path64();
            path.AddRange(
                _pointArray.Select(pt => 
                    new Point64(
                        (long)(pt.X * scalingFactor), 
                        (long)(pt.Y * scalingFactor)
                    )
                )
            );
            return path;
        }

        public PathMetrics GetMetrics(Float64LineSegment2DBvh obstacles)
        {
            return new PathMetrics(this, obstacles);
        }


        public IEnumerator<LinFloat64Vector2D> GetEnumerator()
        {
            return ((IEnumerable<LinFloat64Vector2D>)_pointArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Tests whether this polyline has any self-intersections.
        /// </summary>
        public bool HasSelfIntersection()
        {
            var segmentList = GetSegments().ToArray();

            for (var i = 0; i < segmentList.Length - 2; i++)
            {
                var s1 = segmentList[i];

                for (var j = i + 2; j < segmentList.Length; j++)
                {
                    var s2 = segmentList[j];

                    if (s1.GetSegmentIntersectionParameters(s2).Item1) 
                        return true;
                }
            }

            return false;

            // Empty or single-segment polylines cannot self-intersect
            if (Count < 4)
                return false;

            // Traverse BVH against itself
            return PolylineBvh.TraverseNodePairForHasSelfIntersection(0, 0, Bvh);
        }

        /// <summary>
        /// Finds all self-intersection points of this polyline.
        /// </summary>
        public IReadOnlyList<LinFloat64Vector2D> GetSelfIntersections()
        {
            var results = new List<LinFloat64Vector2D>();

            // Polylines with < 4 points can't self-intersect
            if (Count < 4)
                return results;

            var bvh = Bvh;

            PolylineBvh.TraverseNodePairForSelfIntersections(0, 0, bvh, results);

            return results;
        }

        /// <summary>
        /// Returns true if two polylines intersect (stops at first intersection).
        /// </summary>
        public bool HasIntersectionWith(Float64Polyline2D polyline2)
        {
            if (polyline2 == null)
                throw new ArgumentNullException();

            if (polyline2.Count < 2)
                throw new ArgumentException("Each polyline must contain at least two points.");

            return PolylineBvh.TraverseNodePairForHasIntersection(0, 0, Bvh, polyline2.Bvh);
        }

        /// <summary>
        /// Returns true if a polyline intersects a single segment (stops at first hit).
        /// </summary>
        public bool HasIntersectionWithSegment(Float64LineSegment2D segment)
        {
            var (segP1, segP2) = segment;

            var segBox = SimpleAabb2D.Create(
                segP1.ElementWiseMin(segP2),
                segP1.ElementWiseMax(segP2)
            );

            return PolylineBvh.TraversePolylineAgainstSegmentForHasIntersection(0, Bvh, segP1, segP2, segBox);
        }

        public Float64Polyline2D RemoveShortSegments(double minSegmentLength)
        {
            var pointArray = new List<LinFloat64Vector2D>(Count);
            
            var p1 = _pointArray[0];
            pointArray.Add(p1);
            for (var i = 1; i < _pointArray.Length; i++)
            {
                var p2 = _pointArray[i];

                if (p1.GetDistanceToPoint(p2) < minSegmentLength) 
                    continue;

                pointArray.Add(p2);

                p1 = p2;
            }

            return new Float64Polyline2D(pointArray);
        }

        /// <summary>
        /// Calculates the absolute area between two polylines that share the same endpoints.
        /// </summary>
        /// <param name="polyline2">Second polyline</param>
        /// <param name="minSegmentLength"></param>
        /// <param name="scalingFactor">Factor to scale coordinates for integer precision (default 1000000)</param>
        /// <returns>Absolute area between the polylines</returns>
        public double GetArea(Float64Polyline2D polyline2, double minSegmentLength, long scalingFactor = 1000000)
        {
            var polyline1 = this;

            if (polyline1.Count == 0 || polyline2.Count == 0)
                return 0.0;

            // Create the closed path by combining polyline1 and reversed polyline2
            var path = new Path64();

            // Add points from polyline1
            var p1 = polyline1[0];
            
            path.Add(new Point64(
                (long)(p1.X * scalingFactor),
                (long)(p1.Y * scalingFactor)
            ));

            for (var i = 1; i < polyline1.Count; i++)
            {
                var p2 = polyline1[i];

                if (p1.GetDistanceToPoint(p2) < minSegmentLength) 
                    continue;

                path.Add(new Point64(
                    (long)(p2.X * scalingFactor),
                    (long)(p2.Y * scalingFactor)
                ));

                p1 = p2;
            }

            // Add points from polyline2 in reverse order
            for (var i = polyline2.Count - 1; i >= 0; i--)
            {
                var p2 = polyline2[i];
                
                if (p1.GetDistanceToPoint(p2) < minSegmentLength) 
                    continue;

                path.Add(
                    new Point64(
                        (long)(p2.X * scalingFactor),
                        (long)(p2.Y * scalingFactor)
                    )
                );

                p1 = p2;
            }

            // Close the polygon if not already closed
            if (path.Count > 2 && path[0] != path[^1]) 
                path.Add(path[0]);

            // Use Clipper to clean and calculate area
            var paths = new Paths64([path]);
            
            // Clean the path to remove self-intersections and collinear points
            //paths = Clipper.CleanPolygons(paths, 0); // Minimal distance for cleaning
            
            if (paths.Count == 0 || paths[0].Count < 3)
                return 0.0;

            // Calculate the area of the resulting polygon
            // Clipper2's Area function returns a signed area, so we take the absolute value
            var scaledArea = Math.Abs(Clipper.Area(paths[0]));
            
            // Convert back to original coordinate scale
            return scaledArea / (scalingFactor * scalingFactor);
        }
        
        ///// <summary>
        ///// Calculates the absolute area between this polyline and another polyline.
        ///// Both polylines should have the same start and end points for meaningful results.
        ///// </summary>
        ///// <param name="other">The other polyline to calculate area with</param>
        ///// <returns>The absolute area between the two polylines</returns>
        //public double GetArea2(Float64Polyline2D other)
        //{
        //    if (Count < 2 || other.Count < 2)
        //        return 0.0;

        //    // Create a closed polygon by combining this polyline with the reverse of the other polyline
        //    var combinedPolygon = new List<LinFloat64Vector2D>();
        
        //    // Add points from this polyline (forward direction)
        //    combinedPolygon.AddRange(_pointArray);
        
        //    // Add points from other polyline in reverse direction (excluding the last point to avoid duplication)
        //    for (var i = other.Count - 2; i >= 0; i--) 
        //        combinedPolygon.Add(other._pointArray[i]);

        //    // Calculate the area of the closed polygon using the shoelace formula
        //    return Math.Abs(CalculatePolygonArea(combinedPolygon));

        //    // <summary>
        //    // Calculates the signed area of a polygon using the shoelace formula.
        //    // </summary>
        //    // <param name="polygon">The vertices of the polygon</param>
        //    // <returns>The signed area of the polygon</returns>
        //    double CalculatePolygonArea(IList<LinFloat64Vector2D> polygon)
        //    {
        //        if (polygon.Count < 3)
        //            return 0.0;

        //        var area = 0.0;
        //        var n = polygon.Count;

        //        for (var i = 0; i < n; i++)
        //        {
        //            var j = (i + 1) % n;
        //            area += polygon[i].X * polygon[j].Y;
        //            area -= polygon[j].X * polygon[i].Y;
        //        }

        //        return area / 2.0;
        //    }
        //}


        /// <summary>
        /// Finds all intersection points between two polylines.
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> GetIntersectionsWith(Float64Polyline2D polyline2)
        {
            if (polyline2 == null)
                throw new ArgumentNullException();

            if (polyline2.Count < 2)
                throw new ArgumentException("Each polyline must contain at least two points.");

            var results = new List<LinFloat64Vector2D>();
            PolylineBvh.TraverseNodePairForIntersections(0, 0, Bvh, polyline2.Bvh, results);
            return results;
        }

        /// <summary>
        /// Finds all intersection points between a polyline and a single segment.
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> GetIntersectionsWithSegment(Float64LineSegment2D segment)
        {
            var (segP1, segP2) = segment;

            var results = new List<LinFloat64Vector2D>();

            // Manually create bounding box for the single segment
            var segBox = SimpleAabb2D.Create(
                segP1.ElementWiseMin(segP2),
                segP1.ElementWiseMax(segP2)
            );

            PolylineBvh.TraversePolylineAgainstSegment(0, Bvh, segP1, segP2, segBox, results);
            return results;
        }


        /// <summary>
        /// Shortest distance between a point and a polyline.
        /// </summary>
        public double GetShortestDistanceTo(LinFloat64Vector2D point)
        {
            var bestSq = double.PositiveInfinity;

            for (var i = 0; i < _pointArray.Length - 1; i++)
            {
                var p1 = _pointArray[i];
                var p2 = _pointArray[i + 1];

                var distSq = GetPointSegmentDistSq(point, p1, p2);
                if (distSq < bestSq) bestSq = distSq;
            }

            return Math.Sqrt(bestSq);
        }

        /// <summary>
        /// Shortest distance between a set of segments and a polyline.
        /// </summary>
        public double GetShortestDistanceTo(IReadOnlyList<Float64LineSegment2D> segments)
        {
            if (segments == null)
                throw new ArgumentNullException();

            if (segments.Count == 0)
                throw new ArgumentException("Invalid input.");

            var bvhSegments = new Float64LineSegment2DBvh(segments);

            return GetShortestDistanceTo(bvhSegments);
        }

        /// <summary>
        /// Shortest distance between a set of segments and a polyline.
        /// </summary>
        public double GetShortestDistanceTo(Float64LineSegment2DBvh bvhSegments)
        {
            var bestSq = double.PositiveInfinity;
            bvhSegments.TraverseAgainst(Bvh, ref bestSq);

            return Math.Sqrt(bestSq);
        }


        /// <summary>
        /// Shortest distance between two polylines.
        /// polyline1 and polyline2 are sequences of vertices for polylines.
        /// There should be at least two points in each polyline.
        /// </summary>
        public double GetShortestDistanceTo(Float64Polyline2D polyline2)
        {
            if (polyline2 == null)
                throw new ArgumentNullException();

            if (polyline2.Count < 2)
                throw new ArgumentException("Each polyline must contain at least two points.");

            var bestSq = double.PositiveInfinity;
            Bvh.TraverseAgainst(polyline2.Bvh, ref bestSq);

            return Math.Sqrt(bestSq);
        }


        /// <summary>
        /// Returns all points on the polyline at a given distance from a fixed point.
        /// </summary>
        public IEnumerable<LinFloat64Vector2D> GetPointsAtDistance(LinFloat64Vector2D point, double distance)
        {
            for (var i = 0; i < _pointArray.Length - 1; i++)
            {
                var p1 = _pointArray[i];
                var p2 = _pointArray[i + 1];

                foreach (var x in GetCircleSegmentIntersections(point, distance, p1, p2))
                    yield return x;
            }
        }


        ///// <summary>
        ///// Finds all intersection points between two polylines.
        ///// </summary>
        //public static IEnumerable<LinFloat64Vector2D> GetIntersectionsWith(this IReadOnlyList<LinFloat64Vector2D> polyline1, IReadOnlyList<LinFloat64Vector2D> polyline2)
        //{
        //    if (polyline1 == null || polyline2 == null)
        //        throw new ArgumentNullException();

        //    if (polyline1.Count < 2 || polyline2.Count < 2)
        //        throw new ArgumentException("Each polyline must contain at least two points.");

        //    for (var i = 0; i < polyline1.Count - 1; i++)
        //    {
        //        var a1 = polyline1[i];
        //        var a2 = polyline1[i + 1];

        //        for (var j = 0; j < polyline2.Count - 1; j++)
        //        {
        //            var b1 = polyline2[j];
        //            var b2 = polyline2[j + 1];

        //            if (TrySegmentSegmentIntersection(a1, a2, b1, b2, out var p))
        //                yield return p;
        //        }
        //    }
        //}

        private static double GetPointSegmentDistSq(LinFloat64Vector2D p, LinFloat64Vector2D a, LinFloat64Vector2D b)
        {
            var ab = b - a;
            var t = (p - a).VectorDot(ab) / ab.VectorDot(ab);
            t = Math.Clamp(t, 0.0, 1.0);
            var proj = a + t * ab;
            return p.GetDistanceSquaredToPoint(proj);
        }

        private static IEnumerable<LinFloat64Vector2D> GetCircleSegmentIntersections(LinFloat64Vector2D c, double r, LinFloat64Vector2D a, LinFloat64Vector2D b)
        {
            var d = b - a;
            var f = a - c;

            double A = d.VectorDot(d);
            double B = 2 * f.VectorDot(d);
            double C = f.VectorDot(f) - r * r;

            var disc = B * B - 4 * A * C;
            if (disc < 0) yield break;

            var sqrtD = Math.Sqrt(disc);
            var t1 = (-B - sqrtD) / (2 * A);
            var t2 = (-B + sqrtD) / (2 * A);

            if (t1 is >= 0 and <= 1)
                yield return a + t1 * d;

            if (t2 is >= 0 and <= 1 && !t2.Equals(t1))
                yield return a + t2 * d;
        }

        internal static bool TrySegmentSegmentIntersection(LinFloat64Vector2D p, LinFloat64Vector2D p2, LinFloat64Vector2D q, LinFloat64Vector2D q2, out LinFloat64Vector2D intersection)
        {
            intersection = default;
            var r = p2 - p;
            var s = q2 - q;
            var rxs = r.VectorCross(s);
            var qpxr = (q - p).VectorCross(r);

            if (Math.Abs(rxs) < 1e-12 && Math.Abs(qpxr) < 1e-12)
                return false; // collinear

            if (Math.Abs(rxs) < 1e-12)
                return false; // parallel

            var t = (q - p).VectorCross(s) / rxs;
            var u = (q - p).VectorCross(r) / rxs;

            if (t is >= 0 and <= 1 && u is >= 0 and <= 1)
            {
                intersection = p + t * r;
                return true;
            }

            return false;
        }

        
        public double[,] ToArray2D()
        {
            var array = new double[_pointArray.Length - 1, 4];

            for (var i = 0; i < _pointArray.Length - 1; i++)
            {
                var (sx1, sy1) = _pointArray[i];
                var (sx2, sy2) = _pointArray[i + 1];

                array[i, 0] = sx1;
                array[i, 1] = sy1;
                array[i, 2] = sx2;
                array[i, 3] = sy2;
            }

            return array;
        }

        public override string ToString()
        {
            var composer = new StringBuilder();

            composer.Append('[');

            foreach (var (x, y) in _pointArray)
                composer.Append($"{x:G}, {y:G}; ");

            if (_pointArray.Length > 0)
                composer.Length -= 2;

            composer.Append(']');

            return composer.ToString();
        }
    }
}

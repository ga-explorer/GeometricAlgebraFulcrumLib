using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public sealed class Float64LineSegment2DBvh :
    IReadOnlyList<Float64LineSegment2D>
{
    private sealed class Float64LineSegment2DCentroidComparer(bool splitX, Float64LineSegment2D[] segments) :
        IComparer<int>
    {
        public int Compare(int i1, int i2)
        {
            var s1 = segments[i1];
            var s2 = segments[i2];

            var c1 = (s1.Point1 + s1.Point2) * 0.5;
            var c2 = (s2.Point1 + s2.Point2) * 0.5;

            return splitX ? c1.X.CompareTo(c2.X) : c1.Y.CompareTo(c2.Y);
        }
    }

    private struct Node
    {
        public SimpleAabb2D Box;

        public int Start;
        public int Count;
        public int Left;
        public int Right;

        public bool IsLeaf
            => Count > 0;
    }


    private readonly Float64LineSegment2D[] _segments;
    private readonly int[] _indices;
    private readonly List<Node> _nodes;
    private const int MaxLeafSize = 4;


    public int Count 
        => _segments.Length;

    public Float64LineSegment2D this[int index] 
        => _segments[index];


    public Float64LineSegment2DBvh(IReadOnlyList<Float64LineSegment2D> segments)
    {
        _segments = segments.ToArray();
        _indices = Enumerable.Range(0, _segments.Length).ToArray();
        _nodes = [];

        BuildRecursive(0, _segments.Length);
    }


    private int BuildRecursive(int start, int count)
    {
        var node = new Node { Start = start };
        var box = SimpleAabb2D.Create(
            (double.PositiveInfinity, double.PositiveInfinity),
            (double.NegativeInfinity, double.NegativeInfinity)
        );

        for (var i = start; i < start + count; i++)
        {
            var s = _segments[_indices[i]];
            box.Extend(s.Point1);
            box.Extend(s.Point2);
        }
        node.Box = box;

        var nodeIndex = _nodes.Count;
        _nodes.Add(node);

        if (count <= MaxLeafSize)
        {
            node.Count = count;
            _nodes[nodeIndex] = node;
            return nodeIndex;
        }

        var dx = box.Max.X - box.Min.X;
        var dy = box.Max.Y - box.Min.Y;
        var splitX = dx >= dy;

        Array.Sort(
            _indices,
            start,
            count,
            new Float64LineSegment2DCentroidComparer(splitX, _segments)
        );

        var mid = start + count / 2;

        node.Left = BuildRecursive(start, mid - start);
        node.Right = BuildRecursive(mid, count - (mid - start));
        node.Count = 0;

        _nodes[nodeIndex] = node;
        return nodeIndex;
    }


    public void TraverseAgainst(Float64Polyline2D.PolylineBvh bvhPolyline, ref double bestSq)
    {
        if (_segments.Length == 0) return;

        TraverseNodePair(0, 0, bvhPolyline, ref bestSq);
    }


    /// <summary>
    /// squared distance between two segments p1q1 and p2q2
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="q1"></param>
    /// <param name="p2"></param>
    /// <param name="q2"></param>
    /// <returns></returns>
    private static double SegmentSegmentDistSq(LinFloat64Vector2D p1, LinFloat64Vector2D q1, LinFloat64Vector2D p2, LinFloat64Vector2D q2)
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

    private void TraverseNodePair(int idxSeg, int idxPoly, Float64Polyline2D.PolylineBvh other, ref double bestSq)
    {
        var nodeA = _nodes[idxSeg];
        var nodeB = other.Nodes[idxPoly];
        var lowBound = nodeA.Box.GetDistanceSquared(in nodeB.Box);

        if (lowBound >= bestSq) return;

        if (nodeA.IsLeaf && nodeB.IsLeaf)
        {
            for (var i = nodeA.Start; i < nodeA.Start + nodeA.Count; i++)
            {
                var seg = _segments[_indices[i]];
                for (var j = nodeB.Start; j < nodeB.Start + nodeB.Count; j++)
                {
                    var segBj = other.SegIndices[j];
                    var p1 = other.Points[segBj];
                    var p2 = other.Points[segBj + 1];
                    var d2 = SegmentSegmentDistSq(seg.Point1, seg.Point2, p1, p2);

                    if (d2 < bestSq)
                        bestSq = d2;
                }
            }

            return;
        }

        // same traversal logic as the original BVH...
        if (!nodeA.IsLeaf)
        {
            TraverseNodePair(nodeA.Left, idxPoly, other, ref bestSq);
            TraverseNodePair(nodeA.Right, idxPoly, other, ref bestSq);
        }
        else
        {
            var nbL = nodeB.Left;
            var nbR = nodeB.Right;
            if (nodeB.IsLeaf)
                TraverseNodePair(idxSeg, nodeB.Left, other, ref bestSq);
            else
            {
                TraverseNodePair(idxSeg, nbL, other, ref bestSq);
                TraverseNodePair(idxSeg, nbR, other, ref bestSq);
            }
        }
    }


    public double GetShortestDistanceToPoint(LinFloat64Vector2D point)
    {
        if (_segments.Length == 0)
            return double.PositiveInfinity;

        var bestSq = double.PositiveInfinity;

        TraversePointNode(0);

        return double.IsPositiveInfinity(bestSq)
            ? double.PositiveInfinity
            : Math.Sqrt(bestSq);

        void TraversePointNode(int nodeIndex)
        {
            var node = _nodes[nodeIndex];
            var lowBound = node.Box.GetDistanceSquared(in point);
            if (lowBound >= bestSq) return;

            if (node.IsLeaf)
            {
                for (var i = node.Start; i < node.Start + node.Count; i++)
                {
                    var seg = _segments[_indices[i]];
                    var a = seg.Point1;
                    var b = seg.Point2;
                    var ab = b - a;
                    var abLenSq = ab.VectorDot(ab);
                    const double eps = 1e-12;
                    if (abLenSq <= eps)
                    {
                        // segment is a point
                        var d2 = point.GetDistanceSquaredToPoint(a);
                        if (d2 < bestSq)
                        {
                            bestSq = d2;
                        }
                    }
                    else
                    {
                        double t = (point - a).VectorDot(ab) / abLenSq;
                        t = Math.Clamp(t, 0.0, 1.0);
                        var proj = a + t * ab;
                        var d2 = proj.GetDistanceSquaredToPoint(point);
                        if (d2 < bestSq)
                        {
                            bestSq = d2;
                        }
                    }
                }

                return;
            }

            // Not a leaf: traverse nearer child first to improve pruning
            var left = node.Left;
            var right = node.Right;
            var dl = _nodes[left].Box.GetDistanceSquared(in point);
            var dr = _nodes[right].Box.GetDistanceSquared(in point);

            if (dl < dr)
            {
                if (dl < bestSq) TraversePointNode(left);
                if (dr < bestSq) TraversePointNode(right);
            }
            else
            {
                if (dr < bestSq) TraversePointNode(right);
                if (dl < bestSq) TraversePointNode(left);
            }
        }
    }

    public LinFloat64Vector2D GetNearestPointToPoint(LinFloat64Vector2D point)
    {
        if (_segments.Length == 0)
            return default;

        var bestSq = double.PositiveInfinity;
        var bestPoint = default(LinFloat64Vector2D);

        TraversePointNode(0);

        return bestPoint;

        void TraversePointNode(int nodeIndex)
        {
            var node = _nodes[nodeIndex];
            var lowBound = node.Box.GetDistanceSquared(in point);
            if (lowBound >= bestSq) return;

            if (node.IsLeaf)
            {
                for (var i = node.Start; i < node.Start + node.Count; i++)
                {
                    var seg = _segments[_indices[i]];
                    var a = seg.Point1;
                    var b = seg.Point2;
                    var ab = b - a;
                    var abLenSq = ab.VectorDot(ab);
                    const double eps = 1e-12;
                    if (abLenSq <= eps)
                    {
                        // segment is a point
                        var d2 = point.GetDistanceSquaredToPoint(a);
                        if (d2 < bestSq)
                        {
                            bestSq = d2;
                            bestPoint = a;
                        }
                    }
                    else
                    {
                        double t = (point - a).VectorDot(ab) / abLenSq;
                        t = Math.Clamp(t, 0.0, 1.0);
                        var proj = a + t * ab;
                        var d2 = proj.GetDistanceSquaredToPoint(point);
                        if (d2 < bestSq)
                        {
                            bestSq = d2;
                            bestPoint = proj;
                        }
                    }
                }

                return;
            }

            // Not a leaf: traverse nearer child first
            var left = node.Left;
            var right = node.Right;
            var dl = _nodes[left].Box.GetDistanceSquared(in point);
            var dr = _nodes[right].Box.GetDistanceSquared(in point);

            if (dl < dr)
            {
                if (dl < bestSq) TraversePointNode(left);
                if (dr < bestSq) TraversePointNode(right);
            }
            else
            {
                if (dr < bestSq) TraversePointNode(right);
                if (dl < bestSq) TraversePointNode(left);
            }
        }
    }


    public double GetShortestDistanceToSegment(LinFloat64Vector2D p, LinFloat64Vector2D q)
    {
        //if (_segments.Length == 0)
        //    return double.PositiveInfinity;

        //var bestSq = double.PositiveInfinity;

        //foreach (var (p2, q2) in _segments)
        //{
        //    var d = SegmentSegmentDistSq(p, q, p2, q2);

        //    if (bestSq > d) bestSq = d;
        //}

        //return double.IsPositiveInfinity(bestSq)
        //    ? double.PositiveInfinity
        //    : Math.Sqrt(bestSq);

        return GetShortestDistanceToSegment(Float64LineSegment2D.Create(p, q));
    }

    public double GetShortestDistanceToSegment(Float64LineSegment2D lineSegment)
    {
        if (_segments.Length == 0)
            return double.PositiveInfinity;

        var bestSq = double.PositiveInfinity;
        var p1 = lineSegment.Point1;
        var p2 = lineSegment.Point2;

        TraverseNode(0);

        return double.IsPositiveInfinity(bestSq) 
            ? double.PositiveInfinity 
            : Math.Sqrt(bestSq);

        void TraverseNode(int nodeIndex)
        {
            var node = _nodes[nodeIndex];
            var lowBound = node.Box.GetDistanceSquared(lineSegment.GetBoundingBox());
            if (lowBound >= bestSq) return;

            if (node.IsLeaf)
            {
                for (var i = node.Start; i < node.Start + node.Count; i++)
                {
                    var seg = _segments[_indices[i]];
                    var d2 = SegmentSegmentDistSq(p1, p2, seg.Point1, seg.Point2);
                    if (d2 < bestSq)
                        bestSq = d2;
                }

                return;
            }

            var left = node.Left;
            var right = node.Right;

            var dl = _nodes[left].Box.GetDistanceSquared(lineSegment.GetBoundingBox());
            var dr = _nodes[right].Box.GetDistanceSquared(lineSegment.GetBoundingBox());

            if (dl < dr)
            {
                if (dl < bestSq) TraverseNode(left);
                if (dr < bestSq) TraverseNode(right);
            }
            else
            {
                if (dr < bestSq) TraverseNode(right);
                if (dl < bestSq) TraverseNode(left);
            }
        }
    }
    
    public (LinFloat64Vector2D, LinFloat64Vector2D) GetNearestPoints(Float64LineSegment2D lineSegment)
    {
        if (_segments.Length == 0)
            return (default, default);

        var bestSq = double.PositiveInfinity;
        LinFloat64Vector2D bestA = default, bestB = default;
        var p1 = lineSegment.Point1;
        var p2 = lineSegment.Point2;

        TraverseNode(0);
        return (bestA, bestB);

        void TraverseNode(int nodeIndex)
        {
            var node = _nodes[nodeIndex];
            var lowBound = node.Box.GetDistanceSquared(lineSegment.GetBoundingBox());
            if (lowBound >= bestSq) return;

            if (node.IsLeaf)
            {
                for (var i = node.Start; i < node.Start + node.Count; i++)
                {
                    var seg = _segments[_indices[i]];

                    // Compute closest points on two segments
                    var d1 = p2 - p1;
                    var d2 = seg.Point2 - seg.Point1;
                    var r = p1 - seg.Point1;

                    double a = d1.VectorDot(d1);
                    double e = d2.VectorDot(d2);
                    double f = d2.VectorDot(r);

                    const double eps = 1e-12;
                    double s, t;

                    if (a <= eps && e <= eps)
                    {
                        // both degenerate
                        var d2Sq = p1.GetDistanceSquaredToPoint(seg.Point1);
                        if (d2Sq < bestSq)
                        {
                            bestSq = d2Sq;
                            bestA = p1;
                            bestB = seg.Point1;
                        }
                        continue;
                    }

                    if (a <= eps)
                    {
                        // this segment is a point
                        t = Math.Clamp(f / e, 0.0, 1.0);
                        var c2 = seg.Point1 + t * d2;
                        var d2Sq = p1.GetDistanceSquaredToPoint(c2);
                        if (d2Sq < bestSq)
                        {
                            bestSq = d2Sq;
                            bestA = p1;
                            bestB = c2;
                        }
                        continue;
                    }

                    if (e <= eps)
                    {
                        // other segment is a point
                        s = Math.Clamp(-r.VectorDot(d1) / a, 0.0, 1.0);
                        var c1 = p1 + s * d1;
                        var d2Sq = c1.GetDistanceSquaredToPoint(seg.Point1);
                        if (d2Sq < bestSq)
                        {
                            bestSq = d2Sq;
                            bestA = c1;
                            bestB = seg.Point1;
                        }
                        continue;
                    }

                    double c = d1.VectorDot(r);
                    double b = d1.VectorDot(d2);
                    var denom = a * e - b * b;

                    s = denom != 0.0
                        ? Math.Clamp((b * f - c * e) / denom, 0.0, 1.0)
                        : 0.0;

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

                    var c1Final = p1 + s * d1;
                    var c2Final = seg.Point1 + t * d2;
                    var distSq = c1Final.GetDistanceSquaredToPoint(c2Final);

                    if (distSq < bestSq)
                    {
                        bestSq = distSq;
                        bestA = c1Final;
                        bestB = c2Final;
                    }
                }

                return;
            }

            var left = node.Left;
            var right = node.Right;

            var dl = _nodes[left].Box.GetDistanceSquared(lineSegment.GetBoundingBox());
            var dr = _nodes[right].Box.GetDistanceSquared(lineSegment.GetBoundingBox());

            if (dl < dr)
            {
                if (dl < bestSq) TraverseNode(left);
                if (dr < bestSq) TraverseNode(right);
            }
            else
            {
                if (dr < bestSq) TraverseNode(right);
                if (dl < bestSq) TraverseNode(left);
            }
        }
    }

    /// <summary>
    /// Check that every point on segment s is at least minClearance away from
    /// every obstacle segment. Uses the exact segment-segment closest-point
    /// calculation (distance >= minClearance).
    /// </summary>
    /// <param name="s"></param>
    /// <param name="minClearance"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public bool ValidateSegmentClearance(Float64LineSegment2D s, double minClearance, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (_segments.Length == 0)
            return true;

        var minClearanceSq = minClearance * minClearance;
        var lineBox = s.GetBoundingBox();

        return TraverseNode(0);

        bool TraverseNode(int nodeIndex)
        {
            var node = _nodes[nodeIndex];
            var lowBound = node.Box.GetDistanceSquared(lineBox);

            // Skip this branch if its box is farther than the clearance threshold
            if (lowBound >= minClearanceSq - tolerance)
                return true;

            if (node.IsLeaf)
            {
                for (var i = node.Start; i < node.Start + node.Count; i++)
                {
                    var obs = _segments[_indices[i]];
                    var d2 = Float64LineSegment2DUtils.GetSegmentToSegmentDistanceSquared(
                        s.Point1, s.Point2, obs.Point1, obs.Point2
                    );

                    if (d2 < minClearanceSq - tolerance)
                        return false; // early exit — clearance violated
                }

                return true;
            }

            // Recurse into children; stop early if a violation is found
            var left = node.Left;
            var right = node.Right;

            var dl = _nodes[left].Box.GetDistanceSquared(lineBox);
            var dr = _nodes[right].Box.GetDistanceSquared(lineBox);

            if (dl < dr)
            {
                if (!TraverseNode(left)) return false;
                if (!TraverseNode(right)) return false;
            }
            else
            {
                if (!TraverseNode(right)) return false;
                if (!TraverseNode(left)) return false;
            }

            return true;
        }
    }

    ///// <summary>
    ///// Check that every point on segment s is at least minClearance away from every obstacle segment.
    ///// Uses the exact segment-segment closest-point calculation (distance >= minClearance).
    ///// </summary>
    //public bool ValidateSegmentClearance(Float64LineSegment2D s, double minClearance, double tolerance = Float64Utils.DefaultTolerance)
    //{
    //    if (Count == 0) return true;
    //    var minClearanceSq = minClearance * minClearance;

    //    foreach (var obs in _segments)
    //    {
    //        var d2 = 
    //            Float64LineSegment2DUtils.GetSegmentToSegmentDistanceSquared(s.Item1, s.Item2, obs.Item1, obs.Item2);
                
    //        if (d2 < minClearanceSq - tolerance)
    //            return false;
    //    }

    //    return true;
    //}


    public double[,] ToArray2D()
    {
        var array = new double[_segments.Length, 4];

        for (var i = 0; i < _segments.Length; i++)
        {
            var ((sx1, sy1), (sx2, sy2)) = _segments[i];

            array[i, 0] = sx1;
            array[i, 1] = sy1;
            array[i, 2] = sx2;
            array[i, 3] = sy2;
        }

        return array;
    }


    public IEnumerator<Float64LineSegment2D> GetEnumerator()
    {
        return ((IEnumerable<Float64LineSegment2D>)_segments).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
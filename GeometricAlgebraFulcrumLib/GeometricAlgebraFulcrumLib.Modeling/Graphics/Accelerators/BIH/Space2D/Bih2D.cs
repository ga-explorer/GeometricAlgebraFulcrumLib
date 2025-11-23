using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;

public sealed class Bih2D
{
    // internal node struct
    private struct Node
    {
        public Float64BoundingBox2D Box;
        public int Left;   // -1 for leaf
        public int Right;  // -1 for leaf
        public int Start;  // start index into _indices for leaf
        public int Count;  // number of primitives in leaf
    }

    private readonly Node[] _nodes;
    private readonly int[] _indices;     // primitive indices (packed per leaf)
    private readonly Float64BoundingBox2D[] _primBoxes; // bounding boxes per primitive
    private readonly int _rootIndex;
    private readonly int _leafCapacity;
    private readonly int _primCount;

    // optional stored primitives (one of these will be non-null depending on factory)
    private readonly Pair<LinFloat64Vector2D>[] _segments;
    private readonly Triplet<LinFloat64Vector2D>[] _triangles;

    private Bih2D(Float64BoundingBox2D[] primBoxes, int[] indices, Node[] nodes, int rootIndex, int leafCapacity,
        Pair<LinFloat64Vector2D>[] segments = null, Triplet<LinFloat64Vector2D>[] triangles = null)
    {
        _primBoxes = primBoxes;
        _indices = indices;
        _nodes = nodes;
        _rootIndex = rootIndex;
        _leafCapacity = leafCapacity;
        _primCount = primBoxes?.Length ?? 0;
        _segments = segments;
        _triangles = triangles;
    }

    // ---------------- Factories ----------------
    public static Bih2D BuildFromSegments(Pair<LinFloat64Vector2D>[] segments, int leafCapacity = 8)
    {
        if (segments == null) throw new ArgumentNullException(nameof(segments));
        var n = segments.Length;
        var boxes = new Float64BoundingBox2D[n];
        for (var i = 0; i < n; i++) boxes[i] = Float64BoundingBox2D.ContainingPoints(segments[i]);
        var bih = BuildFromBoxes(boxes, leafCapacity);
        return new Bih2D(boxes, bih._indices, bih._nodes, 0, leafCapacity, segments, null);
    }

    public static Bih2D BuildFromTriangles(Triplet<LinFloat64Vector2D>[] triangles, int leafCapacity = 8)
    {
        if (triangles == null) throw new ArgumentNullException(nameof(triangles));
        var n = triangles.Length;
        var boxes = new Float64BoundingBox2D[n];
        for (var i = 0; i < n; i++) boxes[i] = Float64BoundingBox2D.ContainingPoints(triangles[i]);
        var bih = BuildFromBoxes(boxes, leafCapacity);
        return new Bih2D(boxes, bih._indices, bih._nodes, 0, leafCapacity, null, triangles);
    }

    // Core iterative build (returns temporary nodes/indices arrays in a helper struct)
    private struct BuildResult { public Node[] _nodes; public int[] _indices; }
    private static BuildResult BuildFromBoxes(Float64BoundingBox2D[] primBoxes, int leafCapacity)
    {
        var nPrim = primBoxes.Length;
        if (nPrim == 0) return new BuildResult { _nodes = [], _indices = [] };
        leafCapacity = Math.Max(1, leafCapacity);
        var indices = new int[nPrim];
        for (var i = 0; i < nPrim; i++) indices[i] = i;
        var nodesList = new List<Node>(nPrim * 2);
        // initial root placeholder
        nodesList.Add(new Node());
        var taskStack = new Stack<(int start, int end, int nodeIndex)>();
        taskStack.Push((0, nPrim, 0));
        // ensure nodesList has root slot
        while (taskStack.Count > 0)
        {
            var task = taskStack.Pop();
            int start = task.start, end = task.end, nodeIdx = task.nodeIndex;
            // ensure nodesList has slot nodeIdx
            while (nodesList.Count <= nodeIdx) nodesList.Add(new Node());
            // compute bounding box for this node
            double minx = double.PositiveInfinity, miny = double.PositiveInfinity;
            double maxx = double.NegativeInfinity, maxy = double.NegativeInfinity;
            for (var i = start; i < end; i++)
            {
                var b = primBoxes[indices[i]];
                if (b.MinX < minx) minx = b.MinX;
                if (b.MinY < miny) miny = b.MinY;
                if (b.MaxX > maxx) maxx = b.MaxX;
                if (b.MaxY > maxy) maxy = b.MaxY;
            }
            var node = new Node { Box = Float64BoundingBox2D.Containing(minx, miny, maxx, maxy), Left = -1, Right = -1, Start = start, Count = end - start };
            if (end - start <= leafCapacity)
            {
                nodesList[nodeIdx] = node;
                continue;
            }
            // choose split axis by extent
            double dx = maxx - minx, dy = maxy - miny;
            var axis = dx >= dy ? 0 : 1;
            var len = end - start;
            var mid = start + len / 2;
            Array.Sort(indices, start, len, Comparer<int>.Create((i1, i2) =>
            {
                var c1 = primBoxes[i1].Center;
                var c2 = primBoxes[i2].Center;
                return axis == 0 ? c1.X.CompareTo(c2.X) : c1.Y.CompareTo(c2.Y);
            }));
            var leftIndex = nodesList.Count;
            nodesList.Add(new Node());
            var rightIndex = nodesList.Count;
            nodesList.Add(new Node());
            node.Left = leftIndex; node.Right = rightIndex;
            nodesList[nodeIdx] = node;
            // push children (right then left so left is processed next)
            taskStack.Push((mid, end, rightIndex));
            taskStack.Push((start, mid, leftIndex));
        }
        return new BuildResult { _nodes = nodesList.ToArray(), _indices = indices };
    }

    // ---------------- Access primitives ----------------
    public Pair<LinFloat64Vector2D> GetSegment(int primIdx)
    {
        if (_segments == null) throw new InvalidOperationException("Tree was not built from segments.");
        return _segments[primIdx];
    }

    public Triplet<LinFloat64Vector2D> GetTriangle(int primIdx)
    {
        if (_triangles == null) throw new InvalidOperationException("Tree was not built from triangles.");
        return _triangles[primIdx];
    }

    // ---------------- Non-alloc traversal (list-based) ----------------
    // Calls visitor for each primitive index in the tree.
    public void Traverse(Action<int> visitPrimitive)
    {
        if (visitPrimitive == null) throw new ArgumentNullException(nameof(visitPrimitive));
        if (_nodes == null || _nodes.Length == 0) return;
        var stack = new int[64];
        var sp = 0;
        stack[sp++] = _rootIndex;
        while (sp > 0)
        {
            var ni = stack[--sp];
            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++) visitPrimitive(_indices[s + i]);
            }
            else
            {
                if (sp + 2 >= stack.Length) Array.Resize(ref stack, stack.Length * 2);
                stack[sp++] = node.Left;
                stack[sp++] = node.Right;
            }
        }
    }

    // ---------------- PrimitiveVisitor ref struct ----------------
    public ref struct PrimitiveVisitor
    {
        public delegate void VisitFn(int primIdx, ref bool stop);
        private readonly VisitFn _fn;
        public PrimitiveVisitor(VisitFn fn) => _fn = fn;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke(int primIdx, ref bool stop) => _fn(primIdx, ref stop);
    }

    // ---------------- Span-based traversal ----------------
    public void TraverseAABB(in Float64BoundingBox2D q, PrimitiveVisitor visitor, Span<int> stackBuffer)
    {
        if (_nodes == null || _nodes.Length == 0) return;
        if (stackBuffer.Length < 1) throw new ArgumentException("Stack buffer too small");
        var sp = 0;
        stackBuffer[sp++] = _rootIndex;
        var stop = false;
        while (sp > 0 && !stop)
        {
            var ni = stackBuffer[--sp];
            ref var node = ref _nodes[ni];
            if (!node.Box.IntersectsBox(q)) continue;
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    visitor.Invoke(primIdx, ref stop);
                    if (stop) break;
                }
            }
            else
            {
                if (sp + 2 > stackBuffer.Length) throw new InvalidOperationException("Stack buffer overflow");
                stackBuffer[sp++] = node.Left;
                stackBuffer[sp++] = node.Right;
            }
        }
    }

    public void TraverseAll(PrimitiveVisitor visitor, Span<int> stackBuffer)
    {
        if (_nodes == null || _nodes.Length == 0) return;
        if (stackBuffer.Length < 1) throw new ArgumentException("Stack buffer too small");
        var sp = 0;
        stackBuffer[sp++] = _rootIndex;
        var stop = false;
        while (sp > 0 && !stop)
        {
            var ni = stackBuffer[--sp];
            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    visitor.Invoke(primIdx, ref stop);
                    if (stop) break;
                }
            }
            else
            {
                if (sp + 2 > stackBuffer.Length) throw new InvalidOperationException("Stack buffer overflow");
                stackBuffer[sp++] = node.Left;
                stackBuffer[sp++] = node.Right;
            }
        }
    }

    // ---------------- Query AABB (conservative) ----------------
    public void QueryAABB(in Float64BoundingBox2D q, List<int> outIndices)
    {
        if (outIndices == null) throw new ArgumentNullException(nameof(outIndices));
        if (_nodes == null || _nodes.Length == 0) return;
        var stack = new int[64];
        var sp = 0;
        stack[sp++] = _rootIndex;
        while (sp > 0)
        {
            var ni = stack[--sp];
            ref var node = ref _nodes[ni];
            if (!node.Box.IntersectsBox(q)) continue;
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++) outIndices.Add(_indices[s + i]);
            }
            else
            {
                if (sp + 2 >= stack.Length) Array.Resize(ref stack, stack.Length * 2);
                stack[sp++] = node.Left;
                stack[sp++] = node.Right;
            }
        }
    }

    // Non-alloc variant: fills caller-provided buffer; returns written count via out parameter
    public void QueryAABBNonAlloc(in Float64BoundingBox2D q, int[] buffer, ref int count)
    {
        if (buffer == null) throw new ArgumentNullException(nameof(buffer));
        count = 0;
        if (_nodes == null || _nodes.Length == 0) return;
        var stack = new int[64];
        var sp = 0;
        stack[sp++] = _rootIndex;
        while (sp > 0)
        {
            var ni = stack[--sp];
            ref var node = ref _nodes[ni];
            if (!node.Box.IntersectsBox(q)) continue;
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    if (count >= buffer.Length) return; // stop on overflow
                    buffer[count++] = _indices[s + i];
                }
            }
            else
            {
                if (sp + 2 >= stack.Length) Array.Resize(ref stack, stack.Length * 2);
                stack[sp++] = node.Left;
                stack[sp++] = node.Right;
            }
        }
    }

    // ---------------- Query segment candidates ----------------
    public void QuerySegmentCandidates(in Pair<LinFloat64Vector2D> seg, List<int> outIndices)
    {
        var q = Float64BoundingBox2D.ContainingPoints(seg);
        QueryAABB(in q, outIndices);
    }

    // ---------------- K-nearest (approx) -> returns primitive indices (approx order) ----------------
    // maxNodesVisited controls approximation (smaller => faster, less exact)
    public void QueryKNearestToPoint(double qx, double qy, int k, List<int> outIndices, int maxNodesVisited = int.MaxValue)
    {
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));
        if (outIndices == null) throw new ArgumentNullException(nameof(outIndices));
        outIndices.Clear();
        if (_nodes == null || _nodes.Length == 0) return;

        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceToPointSquared(qx, qy));
        var bestPrims = new SimpleMaxHeap(k);

        var nodesVisited = 0;
        while (nodePQ.Count > 0 && nodesVisited < maxNodesVisited)
        {
            var top = nodePQ.Pop();
            nodesVisited++;
            var ni = top.index;
            var nodeDist2 = top.key;
            if (bestPrims.IsFull && nodeDist2 >= bestPrims.MaxDistance) break;

            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    var pd2 = _primBoxes[primIdx].DistanceToPointSquared(qx, qy);
                    bestPrims.Add(pd2, primIdx);
                }
            }
            else
            {
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceToPointSquared(qx, qy);
                var dr = _nodes[r].Box.DistanceToPointSquared(qx, qy);
                nodePQ.Push(l, dl);
                nodePQ.Push(r, dr);
            }
        }

        var tmp = bestPrims.ExtractAll();
        Array.Sort(tmp, (a, b) => a.dist.CompareTo(b.dist));
        foreach (var p in tmp) outIndices.Add(p.primIdx);
    }

    // variant returning distances (approx)
    public void QueryKNearestWithDistances(double qx, double qy, int k, List<(int primIdx, double dist2)> outResults, int maxNodesVisited = int.MaxValue)
    {
        var tmpIdx = new List<int>();
        QueryKNearestToPoint(qx, qy, k, tmpIdx, maxNodesVisited);
        outResults.Clear();
        foreach (var idx in tmpIdx)
        {
            var d2 = _primBoxes[idx].DistanceToPointSquared(qx, qy);
            outResults.Add((idx, d2));
        }
        outResults.Sort((a, b) => a.dist2.CompareTo(b.dist2));
    }

    // ---------------- Exact k-NN (guaranteed) ----------------
    public void QueryKNearestExact(double qx, double qy, int k, List<(int primIdx, double dist2)> outResults)
    {
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));
        outResults.Clear();
        if (_nodes == null || _nodes.Length == 0) return;
        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceToPointSquared(qx, qy));
        var bestPrims = new SimpleMaxHeap(k);

        while (nodePQ.Count > 0)
        {
            var top = nodePQ.Pop();
            var ni = top.index;
            var nodeDist2 = top.key;
            if (bestPrims.IsFull && nodeDist2 >= bestPrims.MaxDistance) break;

            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    var pd2 = _primBoxes[primIdx].DistanceToPointSquared(qx, qy);
                    bestPrims.Add(pd2, primIdx);
                }
            }
            else
            {
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceToPointSquared(qx, qy);
                var dr = _nodes[r].Box.DistanceToPointSquared(qx, qy);
                nodePQ.Push(l, dl);
                nodePQ.Push(r, dr);
            }
        }

        var tmp = bestPrims.ExtractAll();
        Array.Sort(tmp, (a, b) => a.dist.CompareTo(b.dist));
        foreach (var p in tmp) outResults.Add((p.primIdx, p.dist));
    }

    // ---------------- Span-based nearest streaming (approx and exact) ----------------
    // Approximate streaming (DFS with heuristic ordering)
    public void TraverseKNearestApprox(double qx, double qy, int k, PrimitiveVisitor visitor, Span<int> stackBuffer)
    {
        if (_nodes == null || _nodes.Length == 0) return;
        if (stackBuffer.Length < 1) throw new ArgumentException("Stack buffer too small");
        var sp = 0;
        stackBuffer[sp++] = _rootIndex;
        var stop = false;
        var worstDist2 = double.MaxValue;
        var found = 0;
        while (sp > 0 && !stop)
        {
            var ni = stackBuffer[--sp];
            ref var node = ref _nodes[ni];
            var nd2 = node.Box.DistanceToPointSquared(qx, qy);
            if (found >= k && nd2 >= worstDist2) continue;
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    visitor.Invoke(primIdx, ref stop);
                    if (stop) break;
                    found++;
                }
            }
            else
            {
                if (sp + 2 > stackBuffer.Length) throw new InvalidOperationException("Stack buffer overflow");
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceToPointSquared(qx, qy);
                var dr = _nodes[r].Box.DistanceToPointSquared(qx, qy);
                if (dl < dr) { stackBuffer[sp++] = r; stackBuffer[sp++] = l; }
                else { stackBuffer[sp++] = l; stackBuffer[sp++] = r; }
            }
        }
    }

    // Exact streaming (best-first), streams primitives in exact nearest order until visitor stops
    public void TraverseKNearestExact(double qx, double qy, int k, PrimitiveVisitor visitor)
    {
        if (_nodes == null || _nodes.Length == 0) return;
        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceToPointSquared(qx, qy));
        var stop = false;
        var streamed = 0;
        while (nodePQ.Count > 0 && !stop)
        {
            var top = nodePQ.Pop();
            var ni = top.index;
            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    visitor.Invoke(primIdx, ref stop);
                    if (stop) break;
                    streamed++;
                    if (streamed >= k && stop) break;
                }
            }
            else
            {
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceToPointSquared(qx, qy);
                var dr = _nodes[r].Box.DistanceToPointSquared(qx, qy);
                nodePQ.Push(l, dl);
                nodePQ.Push(r, dr);
            }
        }
    }

    // ---------------- Expose primitive boxes ----------------
    public Float64BoundingBox2D[] PrimitiveBoxes => _primBoxes;

    // ---------------- Helper small heaps (min and max) ----------------
    private struct HeapNode { public int index; public double key; }
    private sealed class SimpleMinHeap
    {
        private HeapNode[] _data = new HeapNode[64];
        private int _count = 0;
        public int Count => _count;
        public void Push(int idx, double key)
        {
            if (_count >= _data.Length) Array.Resize(ref _data, _data.Length * 2);
            var i = _count++;
            _data[i] = new HeapNode { index = idx, key = key };
            while (i > 0)
            {
                var p = i - 1 >> 1;
                if (_data[p].key <= _data[i].key) break;
                var tmp = _data[p]; _data[p] = _data[i]; _data[i] = tmp;
                i = p;
            }
        }
        public (int index, double key) Pop()
        {
            var ret = _data[0];
            _count--;
            if (_count > 0)
            {
                _data[0] = _data[_count];
                var i = 0;
                while (true)
                {
                    int l = (i << 1) + 1, r = l + 1, smallest = i;
                    if (l < _count && _data[l].key < _data[smallest].key) smallest = l;
                    if (r < _count && _data[r].key < _data[smallest].key) smallest = r;
                    if (smallest == i) break;
                    var tmp = _data[i]; _data[i] = _data[smallest]; _data[smallest] = tmp;
                    i = smallest;
                }
            }
            return (ret.index, ret.key);
        }
    }

    // max-heap for best k primitives
    private sealed class SimpleMaxHeap
    {
        private readonly int _capacity;
        private (double dist, int primIdx)[] _data;
        private int _count;
        public SimpleMaxHeap(int capacity)
        {
            _capacity = Math.Max(1, capacity);
            _data = new (double, int)[_capacity + 4];
            _count = 0;
        }
        public bool IsFull => _count >= _capacity;
        public double MaxDistance => _count == 0 ? double.PositiveInfinity : _data[0].dist;
        public void Add(double dist, int primIdx)
        {
            if (_count < _capacity)
            {
                _data[_count++] = (dist, primIdx);
                SiftUp(_count - 1);
            }
            else if (dist < _data[0].dist)
            {
                _data[0] = (dist, primIdx);
                SiftDown(0);
            }
        }
        public (double dist, int primIdx)[] ExtractAll()
        {
            var res = new (double, int)[_count];
            for (var i = 0; i < _count; i++) res[i] = _data[i];
            return res;
        }
        private void SiftUp(int i)
        {
            while (i > 0)
            {
                var p = i - 1 >> 1;
                if (_data[p].dist >= _data[i].dist) break;
                var t = _data[p]; _data[p] = _data[i]; _data[i] = t;
                i = p;
            }
        }
        private void SiftDown(int i)
        {
            while (true)
            {
                int l = (i << 1) + 1, r = l + 1, largest = i;
                if (l < _count && _data[l].dist > _data[largest].dist) largest = l;
                if (r < _count && _data[r].dist > _data[largest].dist) largest = r;
                if (largest == i) break;
                var t = _data[i]; _data[i] = _data[largest]; _data[largest] = t;
                i = largest;
            }
        }
    }
}
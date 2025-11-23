using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D;

public sealed class Bih3DAdvanced
{
    // Node stored in arrays for compactness and speed
    private struct Node
    {
        public AABB3 Box;
        public int Left;  // index of left child, -1 if leaf
        public int Right; // index of right child, -1 if leaf
        public int Start; // start index in 'indices' array for leaf
        public int Count; // count of primitives in leaf
    }

    private readonly Node[] _nodes;
    private readonly int[] _indices;     // primitive indices packed per leaf
    private readonly AABB3[] _primBoxes; // bounding boxes per primitive (ordered by primitive index)
    private readonly int _rootIndex;
    private readonly int _leafCapacity;
    private readonly int _primCount;

    private Bih3DAdvanced(AABB3[] primBoxes, int[] indices, Node[] nodes, int rootIndex, int leafCapacity)
    {
        _primBoxes = primBoxes;
        _indices = indices;
        _nodes = nodes;
        _rootIndex = rootIndex;
        _leafCapacity = leafCapacity;
        _primCount = primBoxes.Length;
    }

    // ---------------- Factory: from segments ----------------
    //public static BIH3DAdvanced BuildFromSegments(Pair<LinFloat64Vector3D>[] segments, int leafCapacity = 8)
    //{
    //    if (segments == null) throw new ArgumentNullException(nameof(segments));
    //    int n = segments.Length;
    //    var boxes = new AABB3[n];
    //    for (int i = 0; i < n; i++) boxes[i] = AABB3.FromSegment(in segments[i]);
    //    return BuildFromBoxes(boxes, leafCapacity);
    //}

    //// ---------------- Factory: from triangles ----------------
    //public static BIH3DAdvanced BuildFromTriangles(Triplet<LinFloat64Vector3D>[] triangles, int leafCapacity = 8)
    //{
    //    if (triangles == null) throw new ArgumentNullException(nameof(triangles));
    //    int n = triangles.Length;
    //    var boxes = new AABB3[n];
    //    for (int i = 0; i < n; i++) boxes[i] = AABB3.FromTriangle(in triangles[i]);
    //    return BuildFromBoxes(boxes, leafCapacity);
    //}

    // ---------------- Core build (iterative, non-recursive) ----------------
    // Accepts an array of primitive AABBs and builds a BIH over indices 0..n-1
    public static Bih3DAdvanced BuildFromBoxes(AABB3[] primBoxes, int leafCapacity = 8)
    {
        if (primBoxes == null) throw new ArgumentNullException(nameof(primBoxes));
        var nPrim = primBoxes.Length;
        if (nPrim == 0)
        {
            // empty tree
            return new Bih3DAdvanced(primBoxes, [], [], -1, leafCapacity);
        }

        leafCapacity = Math.Max(1, leafCapacity);

        // indices array initially 0..n-1 (we'll partition in-place)
        var indices = new int[nPrim];
        for (var i = 0; i < nPrim; i++) indices[i] = i;

        // We'll build nodes in a List and produce final arrays.
        var nodes = new List<Node>(nPrim * 2);
        // stack of tasks: each task is [start, end) range of indices and parent node index + child side
        var stack = new List<(int start, int end, int parentNodeIndex, bool isLeft)>();
        // create root placeholder
        nodes.Add(new Node()); // root at index 0
        stack.Add((0, nPrim, 0, true)); // parent 0 used as placeholder; we'll set root separately

        var nextNodeIndex = 0; // index of node being created; nodes list contains root placeholder already
        // We'll actually treat node creation by appending nodes to list and setting parent links later.
        // For simplicity, we'll push tasks and when processing pop one and create a node.

        // To avoid recursion, use another explicit stack for processing
        var taskStack = new Stack<(int start, int end, int nodeIndex)>();
        // Create initial node
        nodes.Clear();
        nodes.Add(new Node()); // root at 0
        taskStack.Push((0, nPrim, 0));

        while (taskStack.Count > 0)
        {
            var (start, end, nodeIdx) = taskStack.Pop();
            // compute bounding box for this node
            double minx = double.PositiveInfinity, miny = double.PositiveInfinity, minz = double.PositiveInfinity;
            double maxx = double.NegativeInfinity, maxy = double.NegativeInfinity, maxz = double.NegativeInfinity;
            for (var i = start; i < end; i++)
            {
                var b = primBoxes[indices[i]];
                if (b.MinX < minx) minx = b.MinX;
                if (b.MinY < miny) miny = b.MinY;
                if (b.MinZ < minz) minz = b.MinZ;
                if (b.MaxX > maxx) maxx = b.MaxX;
                if (b.MaxY > maxy) maxy = b.MaxY;
                if (b.MaxZ > maxz) maxz = b.MaxZ;
            }
            var node = new Node
            {
                Box = new AABB3(minx, miny, minz, maxx, maxy, maxz),
                Left = -1,
                Right = -1,
                Start = start,
                Count = end - start
            };

            // leaf?
            if (end - start <= leafCapacity)
            {
                nodes[nodeIdx] = node;
                continue;
            }

            // choose axis by extent
            double dx = maxx - minx, dy = maxy - miny, dz = maxz - minz;
            var axis = 0;
            if (dy >= dx && dy >= dz) axis = 1;
            else if (dz >= dx && dz >= dy) axis = 2;

            // partition indices[start..end) by median of centers along axis
            var len = end - start;
            var mid = start + (len / 2);

            // Use Array.Sort on the subrange with custom comparer by center
            Array.Sort(indices, start, len, Comparer<int>.Create((i1, i2) =>
            {
                var c1 = primBoxes[i1].Center();
                var c2 = primBoxes[i2].Center();
                switch (axis)
                {
                    case 0: return c1.cx.CompareTo(c2.cx);
                    case 1: return c1.cy.CompareTo(c2.cy);
                    default: return c1.cz.CompareTo(c2.cz);
                }
            }));

            // create left child node index and right child node index in nodes list
            var leftIndex = nodes.Count;
            nodes.Add(new Node()); // placeholder
            var rightIndex = nodes.Count;
            nodes.Add(new Node()); // placeholder

            // set parent node to point to children
            node.Left = leftIndex;
            node.Right = rightIndex;
            // update node
            nodes[nodeIdx] = node;

            // push right then left (stack LIFO so left processed first)
            taskStack.Push((mid, end: end, rightIndex));
            taskStack.Push((start: start, mid, leftIndex));
        }

        // finalize arrays
        var nodesArr = nodes.ToArray();
        // indices array is already partitioned and contains primitive indices
        return new Bih3DAdvanced(primBoxes, indices, nodesArr, 0, leafCapacity);
    }

    // ---------------- Non-allocating traversal (visitor/callback) ----------------
    // The visitor receives primitive index for each primitive in a leaf. Keep it hot and quick.
    public void Traverse(Action<int> visitPrimitive)
    {
        if (visitPrimitive == null) throw new ArgumentNullException(nameof(visitPrimitive));
        if (_nodes == null || _nodes.Length == 0) return;
        // iterative stack of node indices
        var stack = new int[64];
        var sp = 0;
        stack[sp++] = _rootIndex;

        while (sp > 0)
        {
            var ni = stack[--sp];
            ref var node = ref _nodes[ni];
            if (node.Left == -1) // leaf
            {
                var s = node.Start;
                var cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                    visitPrimitive(_indices[s + i]);
            }
            else
            {
                // internal
                // push children (we enlarge stack if necessary)
                if (sp + 2 >= stack.Length)
                {
                    Array.Resize(ref stack, stack.Length * 2);
                }
                stack[sp++] = node.Left;
                stack[sp++] = node.Right;
            }
        }
    }

    // ---------------- Query AABB -> candidates (conservative) ----------------
    public void QueryAABB(in AABB3 q, List<int> outIndices)
    {
        if (outIndices == null) throw new ArgumentNullException(nameof(outIndices));
        if (_nodes == null || _nodes.Length == 0) return;
        // iterative traversal with local stack
        var stack = new int[64];
        var sp = 0;
        stack[sp++] = _rootIndex;
        while (sp > 0)
        {
            var ni = stack[--sp];
            ref var node = ref _nodes[ni];
            if (!node.Box.Intersects(q)) continue;
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

    // ---------------- Query segment candidates (conservative) ----------------
    public void QuerySegmentCandidates(in Pair<LinFloat64Vector3D> seg, List<int> outIndices)
    {
        var q = AABB3.FromSegment(in seg);
        QueryAABB(in q, outIndices);
    }

    // ---------------- Approximate k-nearest primitives to point ----------------
    // Returns up to k primitive indices sorted by increasing box-distance (approximate).
    // If maxNodesVisited is small, result is approximate but faster. Set to int.MaxValue for full search.
    public void QueryKNearestToPoint(double qx, double qy, double qz, int k, List<int> outIndices, int maxNodesVisited = int.MaxValue)
    {
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));
        if (outIndices == null) throw new ArgumentNullException(nameof(outIndices));
        outIndices.Clear();
        if (_nodes == null || _nodes.Length == 0) return;

        // Best-first traversal: priority queue of (nodeIndex, boxedistance)
        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceSquaredToPoint(qx, qy, qz));

        // small max-heap of best primitives found so far to maintain top-k (we store pairs (dist, primIndex))
        var bestPrims = new SimpleMaxHeap(k);

        var nodesVisited = 0;
        while (nodePQ.Count > 0 && nodesVisited < maxNodesVisited)
        {
            var top = nodePQ.Pop();
            nodesVisited++;
            var ni = top.index;
            var nodeDist2 = top.key;
            // early exit: if we already have k primitives and nodeDist2 >= worst found distance, we can stop
            if (bestPrims.IsFull && nodeDist2 >= bestPrims.MaxDistance) break;

            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                // leaf: evaluate all primitives
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    // compute precise box distance squared to get ordering (cheap)
                    var pd2 = _primBoxes[primIdx].DistanceSquaredToPoint(qx, qy, qz);
                    bestPrims.Add(pd2, primIdx);
                }
            }
            else
            {
                // push children with their box distances
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceSquaredToPoint(qx, qy, qz);
                var dr = _nodes[r].Box.DistanceSquaredToPoint(qx, qy, qz);
                nodePQ.Push(l, dl);
                nodePQ.Push(r, dr);
            }
        }

        // extract results from bestPrims (they are in a max-heap). Return sorted ascending by distance.
        var tmp = bestPrims.ExtractAll(); // returns array of (dist, prim)
        Array.Sort(tmp, (a, b) => a.dist.CompareTo(b.dist));
        foreach (var p in tmp) outIndices.Add(p.primIdx);
    }

    // expose primitives' boxes for caller's precise tests
    public AABB3[] PrimitiveBoxes => _primBoxes;


    // ---------------- Return actual primitives ----------------
    private Pair<LinFloat64Vector3D>[] _segments;
    private Triplet<LinFloat64Vector3D>[] _triangles;

    public Pair<LinFloat64Vector3D> GetSegment(int primIdx)
    {
        if (_segments == null) throw new InvalidOperationException("Tree not built from segments");
        return _segments[primIdx];
    }

    public Triplet<LinFloat64Vector3D> GetTriangle(int primIdx)
    {
        if (_triangles == null) throw new InvalidOperationException("Tree not built from triangles");
        return _triangles[primIdx];
    }

    // Update factories to store primitives
    public static Bih3DAdvanced BuildFromSegments(Pair<LinFloat64Vector3D>[] segments, int leafCapacity = 8)
    {
        var boxes = new AABB3[segments.Length];
        for (var i = 0; i < segments.Length; i++) boxes[i] = AABB3.FromSegment(in segments[i]);
        var bih = BuildFromBoxes(boxes, leafCapacity);
        bih._segments = segments;
        return bih;
    }

    public static Bih3DAdvanced BuildFromTriangles(Triplet<LinFloat64Vector3D>[] triangles, int leafCapacity = 8)
    {
        var boxes = new AABB3[triangles.Length];
        for (var i = 0; i < triangles.Length; i++) boxes[i] = AABB3.FromTriangle(in triangles[i]);
        var bih = BuildFromBoxes(boxes, leafCapacity);
        bih._triangles = triangles;
        return bih;
    }

    // ---------------- QueryKNearestToPoint with distances ----------------
    public void QueryKNearestWithDistances(double qx, double qy, double qz, int k,
        List<(int primIdx, double dist2)> outResults, int maxNodesVisited = int.MaxValue)
    {
        var tmpList = new List<int>();
        QueryKNearestToPoint(qx, qy, qz, k, tmpList, maxNodesVisited);
        outResults.Clear();
        foreach (var idx in tmpList)
        {
            var d2 = _primBoxes[idx].DistanceSquaredToPoint(qx, qy, qz);
            outResults.Add((idx, d2));
        }
        outResults.Sort((a, b) => a.dist2.CompareTo(b.dist2));
    }

    // ---------------- Exact k-NN ----------------
    public void QueryKNearestExact(double qx, double qy, double qz, int k,
        List<(int primIdx, double dist2)> outResults)
    {
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));
        outResults.Clear();
        if (_nodes == null || _nodes.Length == 0) return;

        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceSquaredToPoint(qx, qy, qz));

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
                    var pd2 = _primBoxes[primIdx].DistanceSquaredToPoint(qx, qy, qz);
                    bestPrims.Add(pd2, primIdx);
                }
            }
            else
            {
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceSquaredToPoint(qx, qy, qz);
                var dr = _nodes[r].Box.DistanceSquaredToPoint(qx, qy, qz);
                nodePQ.Push(l, dl);
                nodePQ.Push(r, dr);
            }
        }

        var tmp = bestPrims.ExtractAll();
        Array.Sort(tmp, (a, b) => a.dist.CompareTo(b.dist));
        foreach (var p in tmp) outResults.Add((p.primIdx, p.dist));
    }

    // ---------------- Non-allocating QueryAABB ----------------
    public void QueryAABBNonAlloc(in AABB3 q, int[] buffer, ref int count)
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
            if (!node.Box.Intersects(q)) continue;
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    if (count >= buffer.Length) return; // overflow: silently stop
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



    // ---------------- Helper small priority queues ----------------

    // Minimal binary min-heap for nodes (index, key)
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
            // sift up
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (_data[p].key <= _data[i].key) break;
                Swap(i, p); i = p;
            }
        }
        public (int index, double key) Pop()
        {
            var ret = _data[0];
            _count--;
            if (_count > 0)
            {
                _data[0] = _data[_count];
                // sift down
                var i = 0;
                while (true)
                {
                    var l = (i << 1) + 1;
                    var r = l + 1;
                    var smallest = i;
                    if (l < _count && _data[l].key < _data[smallest].key) smallest = l;
                    if (r < _count && _data[r].key < _data[smallest].key) smallest = r;
                    if (smallest == i) break;
                    Swap(i, smallest); i = smallest;
                }
            }
            return (ret.index, ret.key);
        }
        private void Swap(int i, int j) { var t = _data[i]; _data[i] = _data[j]; _data[j] = t; }
    }

    // Simple fixed-capacity max-heap for best primitives (store smallest k distances via a max-heap)
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
            // max-heap property: parent >= children
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (_data[p].dist >= _data[i].dist) break;
                var t = _data[p]; _data[p] = _data[i]; _data[i] = t;
                i = p;
            }
        }
        private void SiftDown(int i)
        {
            while (true)
            {
                var l = (i << 1) + 1;
                var r = l + 1;
                var largest = i;
                if (l < _count && _data[l].dist > _data[largest].dist) largest = l;
                if (r < _count && _data[r].dist > _data[largest].dist) largest = r;
                if (largest == i) break;
                var t = _data[i]; _data[i] = _data[largest]; _data[largest] = t;
                i = largest;
            }
        }
    }

    public ref struct PrimitiveVisitor
    {
        public delegate void VisitFn(int primIdx, ref bool stop);
        private readonly VisitFn _fn;
        public PrimitiveVisitor(VisitFn fn) => _fn = fn;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke(int primIdx, ref bool stop) => _fn(primIdx, ref stop);
    }


    // ---------------- Span-based traversal ----------------
    public void TraverseAABB(in AABB3 q, PrimitiveVisitor visitor, Span<int> stackBuffer)
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
            if (!node.Box.Intersects(q)) continue;

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
                if (sp + 2 > stackBuffer.Length)
                    throw new InvalidOperationException("Stack buffer overflow");
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
                if (sp + 2 > stackBuffer.Length)
                    throw new InvalidOperationException("Stack buffer overflow");
                stackBuffer[sp++] = node.Left;
                stackBuffer[sp++] = node.Right;
            }
        }
    }

    // Approximate k-nearest: depth-first with bounding box pruning
    public void TraverseKNearestApprox(
        double qx, double qy, double qz, int k,
        PrimitiveVisitor visitor, Span<int> stackBuffer)
    {
        if (_nodes == null || _nodes.Length == 0) return;
        if (stackBuffer.Length < 1) throw new ArgumentException("Stack buffer too small");

        var sp = 0;
        stackBuffer[sp++] = _rootIndex;
        var stop = false;

        // Maintain worst distance threshold
        var worstDist2 = double.MaxValue;
        var found = 0;

        while (sp > 0 && !stop)
        {
            var ni = stackBuffer[--sp];
            ref var node = ref _nodes[ni];
            var nd2 = node.Box.DistanceSquaredToPoint(qx, qy, qz);
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
                    // Caller is responsible for computing actual primitive distances
                    // and can track their own worstDist2 if desired.
                }
            }
            else
            {
                if (sp + 2 > stackBuffer.Length)
                    throw new InvalidOperationException("Stack buffer overflow");
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceSquaredToPoint(qx, qy, qz);
                var dr = _nodes[r].Box.DistanceSquaredToPoint(qx, qy, qz);
                // Push closer first for better pruning
                if (dl < dr)
                {
                    stackBuffer[sp++] = r;
                    stackBuffer[sp++] = l;
                }
                else
                {
                    stackBuffer[sp++] = l;
                    stackBuffer[sp++] = r;
                }
            }
        }
    }

    // Exact k-nearest: best-first traversal
    public void TraverseKNearestExact(
        double qx, double qy, double qz, int k,
        PrimitiveVisitor visitor)
    {
        if (_nodes == null || _nodes.Length == 0) return;

        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceSquaredToPoint(qx, qy, qz));

        var found = 0;
        var stop = false;

        while (nodePQ.Count > 0 && !stop)
        {
            var top = nodePQ.Pop();
            var ni = top.index;
            var nodeDist2 = top.key;

            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    visitor.Invoke(primIdx, ref stop);
                    if (stop) break;
                    found++;
                    if (found >= k && stop) break;
                }
            }
            else
            {
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceSquaredToPoint(qx, qy, qz);
                var dr = _nodes[r].Box.DistanceSquaredToPoint(qx, qy, qz);
                nodePQ.Push(l, dl);
                nodePQ.Push(r, dr);
            }
        }
    }

}
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.SpaceND;

public sealed class BihND
{
    private struct Node
    {
        public AABBND Box;
        public int Left;
        public int Right;
        public int Start;
        public int Count;
    }

    private readonly Node[] _nodes;
    private readonly int[] _indices;
    private readonly AABBND[] _primBoxes;
    private readonly int _rootIndex;
    private readonly int _leafCapacity;
    private readonly int _dim;

    private BihND(AABBND[] primBoxes, int[] indices, Node[] nodes, int rootIndex, int leafCapacity)
    {
        _primBoxes = primBoxes;
        _indices = indices;
        _nodes = nodes;
        _rootIndex = rootIndex;
        _leafCapacity = leafCapacity;
        _dim = primBoxes.Length > 0 ? primBoxes[0].Dim : 0;
    }

    // -------- Factory --------
    public static BihND Build(AABBND[] primBoxes, int leafCapacity = 8)
    {
        if (primBoxes == null) 
            throw new ArgumentNullException(nameof(primBoxes));
        
        var n = primBoxes.Length;
        var indices = new int[n];
        for (var i = 0; i < n; i++) indices[i] = i;
        var nodesList = new List<Node>(n * 2) 
        { 
            new Node() // root placeholder
        };

        var stack = new Stack<(int start, int end, int nodeIndex)>();
        stack.Push((0, n, 0));

        var dim = n > 0 ? primBoxes[0].Dim : 0;

        while (stack.Count > 0)
        {
            var task = stack.Pop();
            int start = task.start, end = task.end, nodeIdx = task.nodeIndex;

            while (nodesList.Count <= nodeIdx) nodesList.Add(new Node());

            // compute box
            var nodeBox = new AABBND(dim);
            for (var i = start; i < end; i++)
            {
                var b = primBoxes[indices[i]];
                for (var d = 0; d < dim; d++)
                {
                    if (b.Min[d] < nodeBox.Min[d]) nodeBox.Min[d] = b.Min[d];
                    if (b.Max[d] > nodeBox.Max[d]) nodeBox.Max[d] = b.Max[d];
                }
            }

            var node = new Node { Box = nodeBox, Left = -1, Right = -1, Start = start, Count = end - start };

            if (end - start <= leafCapacity)
            {
                nodesList[nodeIdx] = node;
                continue;
            }

            // choose split axis = widest extent
            var bestExtent = -1.0;
            var axis = 0;
            for (var d = 0; d < dim; d++)
            {
                var extent = nodeBox.Max[d] - nodeBox.Min[d];
                if (extent > bestExtent) { bestExtent = extent; axis = d; }
            }

            var len = end - start;
            var mid = start + (len / 2);
            Array.Sort(indices, start, len, Comparer<int>.Create((i1, i2) =>
            {
                var c1 = primBoxes[i1].Center(axis);
                var c2 = primBoxes[i2].Center(axis);
                return c1.CompareTo(c2);
            }));

            var leftIndex = nodesList.Count; nodesList.Add(new Node());
            var rightIndex = nodesList.Count; nodesList.Add(new Node());
            node.Left = leftIndex; node.Right = rightIndex;
            nodesList[nodeIdx] = node;

            stack.Push((mid, end, rightIndex));
            stack.Push((start, mid, leftIndex));
        }

        return new BihND(primBoxes, indices, nodesList.ToArray(), 0, leafCapacity);
    }

    // -------- Traversal --------
    public void Traverse(Action<int> visitPrimitive)
    {
        if (_nodes.Length == 0) return;
        var stack = new int[64];
        var sp = 0;
        stack[sp++] = _rootIndex;
        while (sp > 0)
        {
            var ni = stack[--sp];
            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                for (var i = 0; i < node.Count; i++) visitPrimitive(_indices[node.Start + i]);
            }
            else
            {
                if (sp + 2 >= stack.Length) Array.Resize(ref stack, stack.Length * 2);
                stack[sp++] = node.Left;
                stack[sp++] = node.Right;
            }
        }
    }

    // -------- AABB query --------
    public void QueryAABB(AABBND q, List<int> outIndices)
    {
        if (outIndices == null) throw new ArgumentNullException(nameof(outIndices));
        if (_nodes.Length == 0) return;
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
                for (var i = 0; i < node.Count; i++) outIndices.Add(_indices[node.Start + i]);
            }
            else
            {
                if (sp + 2 >= stack.Length) Array.Resize(ref stack, stack.Length * 2);
                stack[sp++] = node.Left;
                stack[sp++] = node.Right;
            }
        }
    }

    // Non-alloc variant
    public void QueryAABBNonAlloc(AABBND q, int[] buffer, ref int count)
    {
        count = 0;
        if (_nodes.Length == 0) return;
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
                for (var i = 0; i < node.Count; i++)
                {
                    if (count >= buffer.Length) return;
                    buffer[count++] = _indices[node.Start + i];
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

    // -------- k-NN exact (best-first) --------
    public void QueryKNearestExact(double[] query, int k, List<(int primIdx, double dist2)> outResults)
    {
        outResults.Clear();
        if (_nodes.Length == 0) return;

        var nodePQ = new SimpleMinHeap();
        nodePQ.Push(_rootIndex, _nodes[_rootIndex].Box.DistanceSquaredToPoint(query));
        var bestPrims = new SimpleMaxHeap(k);

        while (nodePQ.Count > 0)
        {
            var (ni, d2) = nodePQ.Pop();
            if (bestPrims.IsFull && d2 >= bestPrims.MaxDistance) break;

            ref var node = ref _nodes[ni];
            if (node.Left == -1)
            {
                for (var i = 0; i < node.Count; i++)
                {
                    var primIdx = _indices[node.Start + i];
                    var pd2 = _primBoxes[primIdx].DistanceSquaredToPoint(query);
                    bestPrims.Add(pd2, primIdx);
                }
            }
            else
            {
                int l = node.Left, r = node.Right;
                nodePQ.Push(l, _nodes[l].Box.DistanceSquaredToPoint(query));
                nodePQ.Push(r, _nodes[r].Box.DistanceSquaredToPoint(query));
            }
        }

        var tmp = bestPrims.ExtractAll();
        Array.Sort(tmp, (a, b) => a.dist.CompareTo(b.dist));
        foreach (var p in tmp) outResults.Add((p.primIdx, p.dist));
    }

    // ---------------- helper heaps ----------------
    private struct HeapNode { public int index; public double key; }
    private sealed class SimpleMinHeap
    {
        private HeapNode[] _data = new HeapNode[64];

        public int Count { get; private set; }

        public void Push(int idx, double key)
        {
            if (Count >= _data.Length) Array.Resize(ref _data, _data.Length * 2);
            var i = Count++;
            _data[i] = new HeapNode { index = idx, key = key };
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (_data[p].key <= _data[i].key) break;
                (_data[p], _data[i]) = (_data[i], _data[p]);
                i = p;
            }
        }
        public (int index, double key) Pop()
        {
            var ret = _data[0];
            Count--;
            if (Count > 0)
            {
                _data[0] = _data[Count];
                var i = 0;
                while (true)
                {
                    int l = (i << 1) + 1, r = l + 1, smallest = i;
                    if (l < Count && _data[l].key < _data[smallest].key) smallest = l;
                    if (r < Count && _data[r].key < _data[smallest].key) smallest = r;
                    if (smallest == i) break;
                    (_data[i], _data[smallest]) = (_data[smallest], _data[i]);
                    i = smallest;
                }
            }
            return (ret.index, ret.key);
        }
    }

    private sealed class SimpleMaxHeap
    {
        private readonly int _capacity;
        private readonly (double dist, int primIdx)[] _data;
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
                var p = (i - 1) >> 1;
                if (_data[p].dist >= _data[i].dist) break;
                (_data[p], _data[i]) = (_data[i], _data[p]);
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
                (_data[i], _data[largest]) = (_data[largest], _data[i]);
                i = largest;
            }
        }
    }


    // ==================== Non-alloc Traversal ====================

    public void TraverseAll(PrimitiveVisitorND visitor, Span<int> stackBuffer)
    {
        if (_nodes.Length == 0) return;
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
    // ==================== Visitor definition ====================
    public ref struct PrimitiveVisitorND
    {
        public delegate void VisitFn(int primIdx, ref bool stop);
        private readonly VisitFn _fn;
        public PrimitiveVisitorND(VisitFn fn) => _fn = fn;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke(int primIdx, ref bool stop) => _fn(primIdx, ref stop);
    }
    public void TraverseAABB(AABBND q, PrimitiveVisitorND visitor, Span<int> stackBuffer)
    {
        if (_nodes.Length == 0) return;
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

    public void TraverseKNearest(
        double[] query,
        PrimitiveVisitorND visitor,
        Span<int> stackBuffer,
        int k,
        bool exact = true)
    {
        if (_nodes.Length == 0) return;
        if (query.Length != _dim) throw new ArgumentException("Query dimension mismatch");

        var stop = false;

        // Simple binary heap for (node index, dist2)
        var heap = new (int node, double key)[stackBuffer.Length];
        var heapCount = 0;

        void Push(int nodeIdx, double key)
        {
            if (heapCount >= heap.Length)
                throw new InvalidOperationException("Heap buffer overflow");
            var i = heapCount++;
            heap[i] = (nodeIdx, key);
            // sift up
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (heap[p].key <= heap[i].key) break;
                (heap[p], heap[i]) = (heap[i], heap[p]);
                i = p;
            }
        }

        (int node, double key) Pop()
        {
            var top = heap[0];
            heapCount--;
            if (heapCount > 0)
            {
                heap[0] = heap[heapCount];
                var i = 0;
                while (true)
                {
                    int l = (i << 1) + 1, r = l + 1, smallest = i;
                    if (l < heapCount && heap[l].key < heap[smallest].key) smallest = l;
                    if (r < heapCount && heap[r].key < heap[smallest].key) smallest = r;
                    if (smallest == i) break;
                    (heap[i], heap[smallest]) = (heap[smallest], heap[i]);
                    i = smallest;
                }
            }
            return top;
        }

        // Initialize with root
        Push(_rootIndex, _nodes[_rootIndex].Box.DistanceSquaredToPoint(query));

        var emitted = 0;
        while (heapCount > 0 && !stop && emitted < k)
        {
            var (ni, d2) = Pop();
            ref var node = ref _nodes[ni];

            if (node.Left == -1)
            {
                // Leaf → visit primitives
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    var pd2 = _primBoxes[primIdx].DistanceSquaredToPoint(query);
                    visitor.Invoke(primIdx, ref stop);
                    emitted++;
                    if (stop || emitted >= k) break;
                }
            }
            else
            {
                // Internal → push children
                int l = node.Left, r = node.Right;
                Push(l, _nodes[l].Box.DistanceSquaredToPoint(query));
                Push(r, _nodes[r].Box.DistanceSquaredToPoint(query));
            }
        }
    }


    public void TraverseKNearestApprox(
        double[] query,
        PrimitiveVisitorND visitor,
        Span<int> stackBuffer,
        int k,
        int maxNodes = 64)
    {
        if (_nodes.Length == 0) return;
        if (query.Length != _dim) throw new ArgumentException("Query dimension mismatch");
        if (stackBuffer.Length < maxNodes) throw new ArgumentException("Stack buffer too small");

        var stop = false;
        var emitted = 0;
        var sp = 0;

        // Start with root
        stackBuffer[sp++] = _rootIndex;
        var expandedNodes = 0;

        while (sp > 0 && !stop && emitted < k && expandedNodes < maxNodes)
        {
            // Pop last node (LIFO → greedy depth-first)
            var ni = stackBuffer[--sp];
            ref var node = ref _nodes[ni];
            expandedNodes++;

            if (node.Left == -1)
            {
                // Leaf → visit primitives
                int s = node.Start, cnt = node.Count;
                for (var i = 0; i < cnt; i++)
                {
                    var primIdx = _indices[s + i];
                    visitor.Invoke(primIdx, ref stop);
                    emitted++;
                    if (stop || emitted >= k) break;
                }
            }
            else
            {
                // Internal → push closer child first
                int l = node.Left, r = node.Right;
                var dl = _nodes[l].Box.DistanceSquaredToPoint(query);
                var dr = _nodes[r].Box.DistanceSquaredToPoint(query);

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

}
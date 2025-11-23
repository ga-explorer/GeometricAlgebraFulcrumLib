using System.Buffers;
using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space3D;

public sealed class Octree<T> where T : class, ISpatialPrimitive
{
    readonly OctreeNode _root;
    readonly int _nodeCapacity;
    readonly int _maxDepth;
    public int Count { get; private set; }

    // Reusable buffers for NN heaps to reduce allocations on repeated calls
    readonly ArrayPool<HeapNode<T>> _heapPool = ArrayPool<HeapNode<T>>.Shared;
    readonly ArrayPool<HeapNode<OctreeNode>> _nodeHeapPool = ArrayPool<HeapNode<OctreeNode>>.Shared;

    public Octree(Bounds3 worldBounds, int nodeCapacity = 8, int maxDepth = 8)
    {
        if (nodeCapacity <= 0) throw new ArgumentOutOfRangeException(nameof(nodeCapacity));
        if (maxDepth < 1) throw new ArgumentOutOfRangeException(nameof(maxDepth));
        _root = new OctreeNode(worldBounds, nodeCapacity, 0, maxDepth);
        _nodeCapacity = nodeCapacity;
        _maxDepth = maxDepth;
    }

    public bool Insert(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (!_root.Bounds.Intersects(item.Bounds)) return false; // outside world
        var inserted = _root.Insert(item);
        if (inserted) Count++;
        return inserted;
    }

    public bool Remove(T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        var removed = _root.Remove(item);
        if (removed) Count--;
        return removed;
    }

    public void Clear()
    {
        _root.Clear();
        Count = 0;
    }

    // Non-allocing range query into a Span buffer. Returns number of items written.
    // Caller provides a Span<T> buffer with sufficient capacity.
    public int QueryRange(Bounds3 range, Span<T> outBuffer)
    {
        if (outBuffer.Length == 0) return 0;
        var written = 0;
        if (!_root.Bounds.Intersects(range)) return 0;
        _root.QueryRange(range, outBuffer, ref written);
        return written;
    }

    // Visitor-based range query (zero-allocation). visitor called for each matching item.
    // If visitor returns false, traversal stops early and QueryRangeVisit returns false.
    public bool QueryRangeVisit(Bounds3 range, Func<T, bool> visitor)
    {
        if (visitor == null) throw new ArgumentNullException(nameof(visitor));
        if (!_root.Bounds.Intersects(range)) return true;
        return _root.QueryRangeVisit(range, visitor);
    }

    // Simple visitor variant (no early out)
    public void QueryRangeVisit(Bounds3 range, Action<T> visitor)
    {
        if (visitor == null) throw new ArgumentNullException(nameof(visitor));
        QueryRangeVisit(range, t => { visitor(t); return true; });
    }

    // Ray query: returns nearest hit item and distance, or null
    public T Raycast(Ray ray, out float tHit)
    {
        tHit = float.PositiveInfinity;
        T best = null;

        // stack-based depth-first traversal to avoid allocations
        var stack = new Stack<OctreeNode>(16);
        stack.Push(_root);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (!RayIntersectsAABB(ray, node.Bounds, 0, tHit)) continue;

            // check items in node
            for (var i = 0; i < node.Items.Count; i++)
            {
                var prim = node.Items[i];
                if (prim.Intersects(ray, out var t) && t < tHit)
                {
                    tHit = t;
                    best = prim;
                }
            }

            // push children
            if (node.HasChildren)
            {
                // push nearer children last to improve early-out chances
                // we simply push all children; for more pruning compute child distance
                for (var c = 0; c < 8; c++) stack.Push(node.Children[c]);
            }
        }

        if (best == null) tHit = float.PositiveInfinity;
        return best;
    }

    // Nearest-neighbor (k): exact, optimized with reusable heaps to avoid System.PriorityQueue.
    // Returns up to k nearest items (k >= 1). Uses min-heap for nodes and max-heap (bounded) for candidates.
    public List<T> Nearest(Vector3 point, int k = 1)
    {
        if (k < 1) throw new ArgumentOutOfRangeException(nameof(k));
        var result = new List<T>(Math.Min(k, 16));

        // Min-heap of nodes by distance to node bounds
        var nodeHeap = new BinaryMinHeap<OctreeNode>(_nodeHeapPool.Rent(256));
        nodeHeap.ClearAndUseArraySize(0);
        nodeHeap.Push(_root, _root.Bounds.DistanceSquaredToPoint(point));

        // Max-heap of candidate primitives by squared distance (we keep size <= k)
        var candHeap = new BinaryMaxHeap<T>(_heapPool.Rent(Math.Max(256, k * 2)));
        candHeap.ClearAndUseArraySize(0);

        while (nodeHeap.Count > 0)
        {
            nodeHeap.TryPop(out var node, out var nodeDist);

            // early exit: if we already have k candidates and the closest node is farther than farthest candidate, stop
            if (candHeap.Count >= k)
            {
                var worst = candHeap.PeekPriority();
                if (nodeDist > worst) break;
            }

            // check items
            for (var i = 0; i < node.Items.Count; i++)
            {
                var p = node.Items[i];
                var d2 = p.DistanceSquared(point);
                if (candHeap.Count < k)
                {
                    candHeap.Push(p, d2);
                }
                else
                {
                    var worst = candHeap.PeekPriority();
                    if (d2 < worst)
                    {
                        candHeap.Pop();
                        candHeap.Push(p, d2);
                    }
                }
            }

            // enqueue children
            if (node.HasChildren)
            {
                for (var c = 0; c < 8; c++)
                {
                    var child = node.Children[c];
                    var d = child.Bounds.DistanceSquaredToPoint(point);
                    nodeHeap.Push(child, d);
                }
            }
        }

        // extract candidates from max-heap (unsorted) into list
        while (candHeap.Count > 0)
        {
            candHeap.Pop(out var p, out var d);
            result.Add(p);
        }

        // result has farthest-first (because popped from max-heap), so reverse to ascending distance
        result.Reverse();

        // return at most k
        if (result.Count > k) result.RemoveRange(k, result.Count - k);

        // return arrays used for heap storage back to pool
        _heapPool.Return(candHeap.ReleaseArray(), clearArray: false);
        _nodeHeapPool.Return(nodeHeap.ReleaseArray(), clearArray: false);

        return result;
    }

    #region Internal Node

    sealed class OctreeNode
    {
        public Bounds3 Bounds { get; }
        public readonly List<T> Items;

        public OctreeNode[] Children;
        public bool HasChildren => Children != null;

        readonly int _capacity;
        readonly int _depth;
        readonly int _maxDepth;

        public OctreeNode(Bounds3 bounds, int capacity, int depth, int maxDepth)
        {
            Bounds = bounds;
            _capacity = capacity;
            _depth = depth;
            _maxDepth = maxDepth;
            Items = new List<T>(capacity);
        }

        public bool Insert(T item)
        {
            // If node has children, try put into a child that fully contains the item's bounds
            if (HasChildren)
            {
                var idx = ChildIndexThatFullyContains(item.Bounds);
                if (idx >= 0) return Children[idx].Insert(item);
                // otherwise falls through and stays in this node
            }

            Items.Add(item);

            if (Items.Count > _capacity && _depth < _maxDepth)
            {
                if (!HasChildren) Subdivide();

                // try move items to children where possible
                for (var i = Items.Count - 1; i >= 0; i--)
                {
                    var it = Items[i];
                    var idx = ChildIndexThatFullyContains(it.Bounds);
                    if (idx >= 0)
                    {
                        Items.RemoveAt(i);
                        Children[idx].Insert(it);
                    }
                }
            }

            return true;
        }

        public bool Remove(T item)
        {
            if (HasChildren)
            {
                var idx = ChildIndexThatFullyContains(item.Bounds);
                if (idx >= 0) return Children[idx].Remove(item);
                // else maybe stored here
            }

            return Items.Remove(item);
        }

        public void Clear()
        {
            Items.Clear();
            if (HasChildren)
            {
                for (var i = 0; i < 8; i++)
                {
                    Children[i].Clear();
                    Children[i] = null;
                }
                Children = null;
            }
        }

        // QueryRange into Span buffer
        public void QueryRange(Bounds3 range, Span<T> outBuffer, ref int writeIndex)
        {
            if (!Bounds.Intersects(range)) return;

            for (var i = 0; i < Items.Count; i++)
            {
                var it = Items[i];
                if (range.Intersects(it.Bounds))
                {
                    if (writeIndex < outBuffer.Length)
                    {
                        outBuffer[writeIndex++] = it;
                    }
                    else return; // buffer full -> abort early
                }
            }

            if (!HasChildren) return;

            for (var c = 0; c < 8; c++) Children[c].QueryRange(range, outBuffer, ref writeIndex);
        }

        // Visitor with early-exit
        public bool QueryRangeVisit(Bounds3 range, Func<T, bool> visitor)
        {
            if (!Bounds.Intersects(range)) return true;

            for (var i = 0; i < Items.Count; i++)
            {
                var it = Items[i];
                if (range.Intersects(it.Bounds))
                {
                    if (!visitor(it)) return false;
                }
            }

            if (!HasChildren) return true;

            for (var c = 0; c < 8; c++)
            {
                if (!Children[c].QueryRangeVisit(range, visitor)) return false;
            }

            return true;
        }

        int ChildIndexThatFullyContains(Bounds3 b)
        {
            var center = Bounds.Center;
            var ix = b.Min.X >= center.X ? 1 : 0;
            var iy = b.Min.Y >= center.Y ? 1 : 0;
            var iz = b.Min.Z >= center.Z ? 1 : 0;

            // child box
            var childMin = new Vector3(
                ix == 0 ? Bounds.Min.X : center.X,
                iy == 0 ? Bounds.Min.Y : center.Y,
                iz == 0 ? Bounds.Min.Z : center.Z);
            var childMax = new Vector3(
                ix == 0 ? center.X : Bounds.Max.X,
                iy == 0 ? center.Y : Bounds.Max.Y,
                iz == 0 ? center.Z : Bounds.Max.Z);
            var childBounds = new Bounds3(childMin, childMax);

            if (childBounds.Contains(b))
            {
                return iz << 2 | iy << 1 | ix; // z*4 + y*2 + x
            }
            return -1;
        }

        void Subdivide()
        {
            Children = new OctreeNode[8];
            var c = Bounds.Center;
            var min = Bounds.Min;
            var max = Bounds.Max;

            // iterate through 8 child indices
            for (var z = 0; z < 2; z++)
            for (var y = 0; y < 2; y++)
            for (var x = 0; x < 2; x++)
            {
                var childMin = new Vector3(
                    x == 0 ? min.X : c.X,
                    y == 0 ? min.Y : c.Y,
                    z == 0 ? min.Z : c.Z);
                var childMax = new Vector3(
                    x == 0 ? c.X : max.X,
                    y == 0 ? c.Y : max.Y,
                    z == 0 ? c.Z : max.Z);

                var idx = z << 2 | y << 1 | x;
                Children[idx] = new OctreeNode(new Bounds3(childMin, childMax), _capacity, _depth + 1, _maxDepth);
            }
        }
    }

    #endregion

    #region Utilities & Heaps

    // Generic heap node holder used with ArrayPool rented arrays
    struct HeapNode<U>
    {
        public U Item;
        public float Priority;
    }

    // Min-heap specialized for OctreeNode
    sealed class BinaryMinHeap<U>
    {
        HeapNode<U>[] _arr;
        int _count;

        public BinaryMinHeap(HeapNode<U>[] backing)
        {
            _arr = backing ?? throw new ArgumentNullException(nameof(backing));
            _count = 0;
        }

        public int Count => _count;

        public void ClearAndUseArraySize(int newCount)
        {
            _count = newCount;
        }

        public void Push(U item, float priority)
        {
            if (_count >= _arr.Length) Array.Resize(ref _arr, _arr.Length * 2);
            _arr[_count].Item = item;
            _arr[_count].Priority = priority;
            var i = _count;
            _count++;
            while (i > 0)
            {
                var p = i - 1 >> 1;
                if (_arr[p].Priority <= _arr[i].Priority) break;
                Swap(p, i);
                i = p;
            }
        }

        public bool TryPop(out U item, out float priority)
        {
            if (_count == 0) { item = default; priority = 0; return false; }
            item = _arr[0].Item;
            priority = _arr[0].Priority;
            _count--;
            if (_count > 0)
            {
                _arr[0] = _arr[_count];
                var i = 0;
                while (true)
                {
                    var l = (i << 1) + 1;
                    var r = l + 1;
                    var smallest = i;
                    if (l < _count && _arr[l].Priority < _arr[smallest].Priority) smallest = l;
                    if (r < _count && _arr[r].Priority < _arr[smallest].Priority) smallest = r;
                    if (smallest == i) break;
                    Swap(i, smallest);
                    i = smallest;
                }
            }
            return true;
        }

        void Swap(int a, int b)
        {
            var tmp = _arr[a]; _arr[a] = _arr[b]; _arr[b] = tmp;
        }

        public HeapNode<U>[] ReleaseArray() => _arr;
    }

    // Max-heap specialized for primitives (bounded by k)
    sealed class BinaryMaxHeap<U>
    {
        HeapNode<U>[] _arr;
        int _count;

        public BinaryMaxHeap(HeapNode<U>[] backing)
        {
            _arr = backing ?? throw new ArgumentNullException(nameof(backing));
            _count = 0;
        }

        public int Count => _count;

        public void ClearAndUseArraySize(int newCount)
        {
            _count = newCount;
        }

        public void Push(U item, float priority)
        {
            if (_count >= _arr.Length) Array.Resize(ref _arr, _arr.Length * 2);
            _arr[_count].Item = item;
            _arr[_count].Priority = priority;
            var i = _count;
            _count++;
            while (i > 0)
            {
                var p = i - 1 >> 1;
                if (_arr[p].Priority >= _arr[i].Priority) break; // max-heap
                Swap(p, i);
                i = p;
            }
        }

        public float PeekPriority()
        {
            if (_count == 0) return float.PositiveInfinity;
            return _arr[0].Priority;
        }

        public void Pop()
        {
            if (_count == 0) return;
            _count--;
            if (_count > 0)
            {
                _arr[0] = _arr[_count];
                var i = 0;
                while (true)
                {
                    var l = (i << 1) + 1;
                    var r = l + 1;
                    var largest = i;
                    if (l < _count && _arr[l].Priority > _arr[largest].Priority) largest = l;
                    if (r < _count && _arr[r].Priority > _arr[largest].Priority) largest = r;
                    if (largest == i) break;
                    Swap(i, largest);
                    i = largest;
                }
            }
        }

        public void Pop(out U item, out float priority)
        {
            if (_count == 0) { item = default; priority = 0; return; }
            item = _arr[0].Item;
            priority = _arr[0].Priority;
            Pop();
        }

        public HeapNode<U>[] ReleaseArray() => _arr;

        void Swap(int a, int b)
        {
            var tmp = _arr[a]; _arr[a] = _arr[b]; _arr[b] = tmp;
        }
    }

    static bool RayIntersectsAABB(Ray ray, Bounds3 box, float tMin, float tMax)
    {
        // slab method with robust checks
        var invDir = new Vector3(1f / ray.Direction.X, 1f / ray.Direction.Y, 1f / ray.Direction.Z);

        var t1 = (box.Min.X - ray.Origin.X) * invDir.X;
        var t2 = (box.Max.X - ray.Origin.X) * invDir.X;
        var tmin = Math.Min(t1, t2);
        var tmax = Math.Max(t1, t2);

        t1 = (box.Min.Y - ray.Origin.Y) * invDir.Y;
        t2 = (box.Max.Y - ray.Origin.Y) * invDir.Y;
        tmin = Math.Max(tmin, Math.Min(t1, t2));
        tmax = Math.Min(tmax, Math.Max(t1, t2));

        t1 = (box.Min.Z - ray.Origin.Z) * invDir.Z;
        t2 = (box.Max.Z - ray.Origin.Z) * invDir.Z;
        tmin = Math.Max(tmin, Math.Min(t1, t2));
        tmax = Math.Min(tmax, Math.Max(t1, t2));

        return tmax >= Math.Max(tmin, 0f) && tmin <= tMax;
    }

    #endregion

    #region GPU Export / Layout Suggestions

    /*
        GPU-friendly layout suggestions (notes):
        - Store vertex positions in a tightly packed float3 buffer (float[] or Vector3[] contiguous).
        - Store triangle indices as uint/int array packed tightly.
        - Store per-primitive AABBs as two float3 (min,max) pairs; upload as StructuredBuffer of 6 floats.
        - For GPU culling/queries: upload a compact BVH-like node array instead of pointer-based nodes. Each node should be a struct like:
              struct GpuNode { float3 bboxMin; float3 bboxMax; int childBase; uint childMask; int start; int count; }
          where childMask indicates leaf/child presence. Use index-based addressing.
        - If using compute shaders for traversal, avoid recursion: implement iterative stack traversal in shader using an index stack.
        - Consider reordering triangles to improve locality (Morton order / z-order) before uploading.

        The helper below flattens primitives' AABBs and triangle vertex lists for upload. It is intentionally simple; for production you may
        want a streaming writer or reuse buffers to avoid allocations.
        */

    // Example exporter that collects all primitives' Bounds into an array for GPU upload.
    public void ExportBoundsForGpu(out Bounds3[] boundsArray)
    {
        var list = new List<Bounds3>(Count);
        QueryRangeVisit(_root.Bounds, (t) => { list.Add(t.Bounds); return true; });
        boundsArray = list.ToArray();
    }

    #endregion
}
using System.Buffers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.SpacePartitions
{
    public sealed class Float64DynamicPartitionTree2D(IEnumerable<IFloat64BoundedShape2D> shapes, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64PartitionTreeDynamic<IFloat64BoundedShape2D, Float64BoundingBox2D, LinFloat64Vector2D>(shapes, 2, leafCapacity, rebuildImbalance) { }

    public sealed class Float64DynamicPartitionTree3D(IEnumerable<IFloat64BoundedShape3D> shapes, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64PartitionTreeDynamic<IFloat64BoundedShape3D, Float64BoundingBox3D, LinFloat64Vector3D>(shapes, 3, leafCapacity, rebuildImbalance) { }

    public sealed class Float64DynamicPartitionTree4D(IEnumerable<IFloat64BoundedShape4D> shapes, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64PartitionTreeDynamic<IFloat64BoundedShape4D, Float64BoundingBox4D, LinFloat64Vector4D>(shapes, 4, leafCapacity, rebuildImbalance) { }

    public sealed class Float64DynamicPartitionTree(IEnumerable<IFloat64BoundedShape> shapes, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64PartitionTreeDynamic<IFloat64BoundedShape, Float64BoundingBox, LinFloat64Vector>(shapes, Math.Max(1, dimensions), leafCapacity, rebuildImbalance) { }

    /// <summary>
    /// Dynamic partition tree (2^n branching factor) with:
    /// - parent pointers
    /// - ArrayPool-backed leaf storage
    /// - exact containment insertion
    /// - local subtree rebuild heuristics
    /// - IDisposable (returns pooled arrays on Dispose)
    /// </summary>
    public class Float64PartitionTreeDynamic<TGeom, TBox, TVector> : 
        IFloat64SpacePartition<TBox, TVector>,
        IDisposable
        where TGeom : IFloat64BoundedShape<TBox>
        where TBox : IFloat64BoundingBox<TBox, TVector>
        where TVector : ILinFloat64Vector
    {
        private const double Epsilon = 1e-12;

        private class Node(Node? parent, TBox box)
        {
            public TBox Box = box;
            public Node? Parent = parent;
            public Node?[]? Children = null; // null if leaf
            public int[]? Indices = null;    // pooled array when leaf
            public int IndicesCount = 0;
            public int Count = 0;        // count of primitives in subtree (including those in this node)

            public bool IsLeaf => Children == null;

            public void MakeLeaf(int[] arr, int count)
            {
                Children = null;
                Indices = arr;
                IndicesCount = count;
                Count = count;
            }

            public void MakeInternal(Node?[] children)
            {
                Children = children;
                Indices = null;
                IndicesCount = 0;
                Count = 0; // will be recomputed by caller
            }

            public void ReleaseLeaf(ArrayPool<int> pool)
            {
                if (Indices != null)
                {
                    pool.Return(Indices);
                    Indices = null;
                    IndicesCount = 0;
                }
            }
        }

        private readonly List<TGeom> _objects = [];
        private readonly List<TBox> _boxes = [];
        private readonly List<Node?> _nodeForIndex = [];

        private readonly int _dimensions;
        private readonly int _childCount;
        private readonly int _leafCapacity;
        private readonly double _rebuildImbalance;
        private readonly ArrayPool<int> _pool = ArrayPool<int>.Shared;

        private Node _root;
        private bool _disposed;

        public Float64PartitionTreeDynamic(IEnumerable<TGeom> shapes, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        {
            _dimensions = Math.Max(1, dimensions);
            _childCount = 1 << _dimensions;
            _leafCapacity = Math.Max(1, leafCapacity);
            _rebuildImbalance = Math.Clamp(rebuildImbalance, 0.55, 0.95);

            foreach (var o in shapes)
            {
                _objects.Add(o);
                _boxes.Add(o.GetBoundingBox());
                _nodeForIndex.Add(null);
            }

            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            if (valid.Length == 0)
            {
                var empty = TBox.Empty(_dimensions);
                _root = new Node(null, empty);
                var arr = _pool.Rent(_leafCapacity);
                _root.MakeLeaf(arr, 0);
            }
            else
            {
                _root = BuildRecursive(null, valid);
            }
        }

        #region --- Build & Helpers ---

        // Build subtree recursively (used at initialization and local rebuilds).
        // Updates _nodeForIndex mapping for leaves.
        private Node BuildRecursive(Node? parent, int[] indices)
        {
            var box = indices.Length > 0 ? TBox.Containing(indices.Select(i => _boxes[i])) : TBox.Empty(_dimensions);
            var node = new Node(parent, box);

            if (indices.Length <= _leafCapacity)
            {
                var arr = _pool.Rent(Math.Max(_leafCapacity, indices.Length));
                Array.Copy(indices, arr, indices.Length);
                node.MakeLeaf(arr, indices.Length);
                for (var i = 0; i < node.IndicesCount; i++) _nodeForIndex[node.Indices![i]] = node;
                return node;
            }

            // Prepare children lists
            var childLists = new List<int>[_childCount];
            for (var c = 0; c < _childCount; c++) childLists[c] = [];

            var stay = new List<int>();

            // Precompute node min/max from center/size
            var center = node.Box.Center;
            var size = node.Box.Size;
            var half = new double[_dimensions];
            for (var d = 0; d < _dimensions; d++) half[d] = size[d] * 0.5;
            var nodeMin = new double[_dimensions];
            var nodeMax = new double[_dimensions];
            for (var d = 0; d < _dimensions; d++)
            {
                nodeMin[d] = center[d] - half[d];
                nodeMax[d] = center[d] + half[d];
            }

            foreach (var idx in indices)
            {
                if (_boxes[idx].IsEmpty) { stay.Add(idx); continue; }

                // Compute primitive min/max
                var pc = _boxes[idx].Center;
                var ps = _boxes[idx].Size;
                var ph = new double[_dimensions];
                for (var d = 0; d < _dimensions; d++) ph[d] = ps[d] * 0.5;

                var pmin = new double[_dimensions];
                var pmax = new double[_dimensions];
                for (var d = 0; d < _dimensions; d++) { pmin[d] = pc[d] - ph[d]; pmax[d] = pc[d] + ph[d]; }

                var fitChild = -1;
                for (var c = 0; c < _childCount; c++)
                {
                    var fits = true;
                    for (var d = 0; d < _dimensions; d++)
                    {
                        var bit = (c >> d) & 1;
                        var cmin = bit == 0 ? nodeMin[d] : center[d];
                        var cmax = bit == 0 ? center[d] : nodeMax[d];
                        if (pmin[d] < cmin - Epsilon || pmax[d] > cmax + Epsilon) { fits = false; break; }
                    }
                    if (fits) { fitChild = c; break; }
                }

                if (fitChild >= 0) childLists[fitChild].Add(idx);
                else stay.Add(idx);
            }

            // Create children recursively
            node.Children = new Node[_childCount];
            var subtreeCount = 0;
            for (var c = 0; c < _childCount; c++)
            {
                var cl = childLists[c];
                if (cl.Count == 0)
                {
                    node.Children[c] = null;
                    continue;
                }

                var childIndices = cl.ToArray();
                var child = BuildRecursive(node, childIndices);
                node.Children[c] = child;
                subtreeCount += child.Count;
            }

            // assign stay indices to this node's leaf storage
            if (stay.Count > 0)
            {
                var arr = _pool.Rent(Math.Max(_leafCapacity, stay.Count));
                stay.CopyTo(arr);
                node.Indices = arr;
                node.IndicesCount = stay.Count;
                for (var i = 0; i < node.IndicesCount; i++) _nodeForIndex[node.Indices[i]] = node;
                subtreeCount += node.IndicesCount;
            }
            else
            {
                var arr = _pool.Rent(_leafCapacity);
                node.Indices = arr;
                node.IndicesCount = 0;
            }

            node.Count = subtreeCount;
            return node;
        }

        // Collect indices under a node (for rebuild)
        private List<int> CollectIndices(Node node)
        {
            var list = new List<int>(Math.Max(4, node.Count));
            var stack = new Stack<Node>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                var n = stack.Pop();
                if (n.IsLeaf)
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices![i];
                        if (!_boxes[idx].IsEmpty) list.Add(idx);
                    }
                }
                else
                {
                    for (var c = 0; c < n.Children!.Length; c++)
                        if (n.Children[c] != null) stack.Push(n.Children[c]!);

                    // also include indices stored in this internal node, if any (rare in this design)
                    if (n.IndicesCount > 0)
                    {
                        for (var i = 0; i < n.IndicesCount; i++)
                        {
                            var idx = n.Indices![i];
                            if (!_boxes[idx].IsEmpty) list.Add(idx);
                        }
                    }
                }
            }

            return list;
        }

        // Rebuild subtree rooted at 'node' (local rebuild). Keeps parent's links intact.
        private void RebuildSubtree(Node node)
        {
            var parent = node.Parent;
            var indices = CollectIndices(node).ToArray();

            // free pooled arrays under node (they will be re-rented during buildRecursive)
            ReturnPoolsRecursive(node);

            // Build new subtree with parent pointer same as original parent
            var newNode = BuildRecursive(parent, indices);

            if (parent == null)
            {
                // node was root
                _root = newNode;
                return;
            }

            // attach newNode into parent's child array in the right slot
            for (var c = 0; c < parent.Children!.Length; c++)
            {
                if (parent.Children[c] == node)
                {
                    parent.Children[c] = newNode;
                    newNode.Parent = parent;
                    break;
                }
            }

            // update ancestor counts
            var p = parent;
            while (p != null)
            {
                p.Count = 0;
                // recompute count as sum of children + own indices
                if (p.Children != null)
                {
                    for (var cc = 0; cc < p.Children.Length; cc++)
                        if (p.Children[cc] != null) 
                            p.Count += p.Children[cc]!.Count;
                }
                if (p.Indices != null) p.Count += p.IndicesCount;
                p = p.Parent;
            }
        }

        // Return pooled arrays under a node
        private void ReturnPoolsRecursive(Node node)
        {
            if (node == null) return;
            if (node.Indices != null)
            {
                _pool.Return(node.Indices);
                node.Indices = null;
                node.IndicesCount = 0;
            }

            if (node is { IsLeaf: false, Children: not null })
            {
                for (var c = 0; c < node.Children.Length; c++)
                    if (node.Children[c] != null) 
                        ReturnPoolsRecursive(node.Children[c]!);
            }
        }

        // Heuristic to decide if a node needs local rebuild because one child dominates
        private bool NeedsRebuild(Node node)
        {
            if (node.IsLeaf || node.Children == null) return false;
            var total = 0;
            var maxChild = 0;
            for (var c = 0; c < node.Children.Length; c++)
            {
                var ch = node.Children[c];
                if (ch == null) continue;

                total += ch.Count;
                maxChild = Math.Max(maxChild, ch.Count);
            }
            // include node's own indices
            total += node.IndicesCount;
            return total > 1 && maxChild > _rebuildImbalance * total;
        }

        // Get child index that fully contains box if any; otherwise return -1.
        // If no child exists, yet we still compute which child would fully contain box.
        private int GetExactContainingChildIndex(Node node, TBox box)
        {
            // compute node center/size
            var center = node.Box.Center;
            var size = node.Box.Size;
            var half = new double[_dimensions];
            for (var d = 0; d < _dimensions; d++) half[d] = size[d] * 0.5;
            var nodeMin = new double[_dimensions];
            var nodeMax = new double[_dimensions];
            for (var d = 0; d < _dimensions; d++)
            {
                nodeMin[d] = center[d] - half[d];
                nodeMax[d] = center[d] + half[d];
            }

            // primitive min/max
            var pc = box.Center;
            var ps = box.Size;
            var ph = new double[_dimensions];
            for (var d = 0; d < _dimensions; d++) ph[d] = ps[d] * 0.5;
            var pmin = new double[_dimensions];
            var pmax = new double[_dimensions];
            for (var d = 0; d < _dimensions; d++) { pmin[d] = pc[d] - ph[d]; pmax[d] = pc[d] + ph[d]; }

            var fitChild = -1;
            for (var c = 0; c < _childCount; c++)
            {
                var fits = true;
                for (var d = 0; d < _dimensions; d++)
                {
                    var bit = (c >> d) & 1;
                    var cmin = bit == 0 ? nodeMin[d] : center[d];
                    var cmax = bit == 0 ? center[d] : nodeMax[d];
                    if (pmin[d] < cmin - Epsilon || pmax[d] > cmax + Epsilon) { fits = false; break; }
                }
                if (fits) { fitChild = c; break; }
            }

            return fitChild;
        }

        #endregion

        #region --- Insert / Remove / Update ---

        /// <summary>
        /// Insert object, returns its index.
        /// </summary>
        public int Insert(TGeom obj)
        {
            var idx = _objects.Count;
            _objects.Add(obj);
            _boxes.Add(obj.GetBoundingBox());
            _nodeForIndex.Add(null);

            if (_boxes[idx].IsEmpty) return idx;

            InsertIndex(idx);

            return idx;
        }

        // Main insert by index; uses exact containment when pushing down
        private void InsertIndex(int idx)
        {
            var box = _boxes[idx];
            var node = _root;

            // walk as far as possible into children that fully contain the box
            while (!node.IsLeaf)
            {
                var childIndex = GetExactContainingChildIndex(node, box);
                if (childIndex < 0) break; // no child fully contains box -> store here
                if (node.Children![childIndex] == null)
                {
                    // create child placeholder with empty leaf
                    var childBox = box; // initial bounding box
                    var child = new Node(node, childBox);
                    var arr = _pool.Rent(_leafCapacity);
                    child.MakeLeaf(arr, 0);
                    node.Children[childIndex] = child;
                }

                node = node.Children[childIndex]!;
            }

            // Now node is where we should insert (leaf or internal but cannot push into child)
            if (node.IsLeaf)
            {
                // ensure Indices exists
                if (node.Indices == null)
                {
                    node.Indices = _pool.Rent(_leafCapacity);
                    node.IndicesCount = 0;
                }

                if (node.IndicesCount < node.Indices.Length)
                {
                    node.Indices[node.IndicesCount++] = idx;
                    _nodeForIndex[idx] = node;
                    // update counts up the tree
                    var p = node;
                    while (p != null) { p.Count++; p = p.Parent; }
                }
                else
                {
                    // leaf overflow: split by creating children and redistributing using exact containment
                    var all = new int[node.IndicesCount + 1];
                    Array.Copy(node.Indices!, all, node.IndicesCount);
                    all[all.Length - 1] = idx;

                    // free old buffer
                    _pool.Return(node.Indices!);
                    node.Indices = null;
                    node.IndicesCount = 0;

                    // create children array
                    node.Children = new Node[_childCount];
                    // initialize child slots to null

                    // redistribute items by exact containment; items that don't fit into any child stay in this node's indices
                    var stayList = new List<int>();
                    var childBuffers = new List<int>[_childCount];
                    for (var c = 0; c < _childCount; c++) childBuffers[c] = [];

                    var center = node.Box.Center;
                    var size = node.Box.Size;
                    var half = new double[_dimensions];
                    for (var d = 0; d < _dimensions; d++) half[d] = size[d] * 0.5;
                    var nodeMin = new double[_dimensions];
                    var nodeMax = new double[_dimensions];
                    for (var d = 0; d < _dimensions; d++) { nodeMin[d] = center[d] - half[d]; nodeMax[d] = center[d] + half[d]; }

                    foreach (var id in all)
                    {
                        if (_boxes[id].IsEmpty) { stayList.Add(id); continue; }

                        // check containment
                        var pc = _boxes[id].Center;
                        var ps = _boxes[id].Size;
                        var ph = new double[_dimensions];
                        for (var d = 0; d < _dimensions; d++) ph[d] = ps[d] * 0.5;
                        var pmin = new double[_dimensions];
                        var pmax = new double[_dimensions];
                        for (var d = 0; d < _dimensions; d++) { pmin[d] = pc[d] - ph[d]; pmax[d] = pc[d] + ph[d]; }

                        var fit = -1;
                        for (var c = 0; c < _childCount; c++)
                        {
                            var fits = true;
                            for (var d = 0; d < _dimensions; d++)
                            {
                                var bit = (c >> d) & 1;
                                var cmin = bit == 0 ? nodeMin[d] : center[d];
                                var cmax = bit == 0 ? center[d] : nodeMax[d];
                                if (pmin[d] < cmin - Epsilon || pmax[d] > cmax + Epsilon) { fits = false; break; }
                            }
                            if (fits) { fit = c; break; }
                        }

                        if (fit >= 0) childBuffers[fit].Add(id);
                        else stayList.Add(id);
                    }

                    // create children nodes for non-empty childBuffers and distribute
                    var subtreeCount = 0;
                    for (var c = 0; c < _childCount; c++)
                    {
                        var list = childBuffers[c];
                        if (list.Count == 0) { node.Children[c] = null; continue; }
                        var childArray = list.ToArray();
                        var childBox = TBox.Containing(childArray.Select(i => _boxes[i]));
                        var child = BuildRecursive(node, childArray); // reuse buildRecursive for balanced child
                        node.Children[c] = child;
                        subtreeCount += child.Count;
                    }

                    // keep stayList in this node as leaf
                    if (stayList.Count > 0)
                    {
                        var arr = _pool.Rent(Math.Max(_leafCapacity, stayList.Count));
                        stayList.CopyTo(arr);
                        node.Indices = arr;
                        node.IndicesCount = stayList.Count;
                        for (var i = 0; i < node.IndicesCount; i++) _nodeForIndex[node.Indices[i]] = node;
                        subtreeCount += node.IndicesCount;
                    }
                    else
                    {
                        // no stay items
                        var arr = _pool.Rent(_leafCapacity);
                        node.Indices = arr;
                        node.IndicesCount = 0;
                    }

                    // set subtree count and update ancestor counts
                    node.Count = subtreeCount;
                    var p2 = node.Parent;
                    while (p2 != null)
                    {
                        p2.Count = 0;
                        if (p2.Children != null)
                            for (var cc = 0; cc < p2.Children.Length; cc++)
                                if (p2.Children[cc] != null) p2.Count += p2.Children[cc]!.Count;
                        if (p2.Indices != null) p2.Count += p2.IndicesCount;
                        p2 = p2.Parent;
                    }

                    // If any ancestor needs rebuild due to imbalance, rebuild them
                    var up = node;
                    while (up != null)
                    {
                        if (NeedsRebuild(up))
                        {
                            RebuildSubtree(up);
                            break;
                        }
                        up = up.Parent;
                    }
                }
            }
            else
            {
                // internal node: attempt to push into exact containing child; if that child doesn't exist, create it
                var childIndex = GetExactContainingChildIndex(node, box);
                if (childIndex < 0)
                {
                    // put at this internal node (store in its Indices buffer)
                    if (node.Indices == null)
                    {
                        node.Indices = _pool.Rent(_leafCapacity);
                        node.IndicesCount = 0;
                    }
                    node.Indices[node.IndicesCount++] = idx;
                    _nodeForIndex[idx] = node;

                    // update counts upward
                    var p = node;
                    while (p != null) { p.Count++; p = p.Parent; }

                    // maybe rebalance current node if needed
                    if (NeedsRebuild(node)) RebuildSubtree(node);
                }
                else
                {
                    if (node.Children![childIndex] == null)
                    {
                        // create child leaf
                        var childBox = _boxes[idx];
                        var child = new Node(node, childBox);
                        var arr = _pool.Rent(_leafCapacity);
                        child.MakeLeaf(arr, 0);
                        node.Children[childIndex] = child;
                    }

                    InsertIndexIntoChild(node.Children[childIndex]!, idx);
                }
            }
        }

        private void InsertIndexIntoChild(Node inChild, int idx)
        {
            // wrapper to increment count and forward to InsertIndex's leaf handling
            // We will walk into child similarly as InsertIndex; this avoids duplicating big logic
            // But to keep counts correct, call InsertIndex (which starts from root). So implement a small loop:
            var node = inChild;
            var box = _boxes[idx];

            // similar to earlier while loop
            while (!node.IsLeaf)
            {
                var childIndex = GetExactContainingChildIndex(node, box);
                if (childIndex < 0) break;
                if (node.Children![childIndex] == null)
                {
                    var newChild = new Node(node, box);
                    var arr = _pool.Rent(_leafCapacity);
                    newChild.MakeLeaf(arr, 0);
                    node.Children[childIndex] = newChild;
                }
                node = node.Children[childIndex]!;
            }

            if (node.Indices == null)
            {
                node.Indices = _pool.Rent(_leafCapacity);
                node.IndicesCount = 0;
            }

            if (node.IndicesCount < node.Indices.Length)
            {
                node.Indices[node.IndicesCount++] = idx;
                _nodeForIndex[idx] = node;
                var p = node;
                while (p != null) { p.Count++; p = p.Parent; }
            }
            else
            {
                // overflow -> reuse same splitting logic as in InsertIndex by calling InsertIndex for idx but starting at this node
                // We'll collect existing indices + idx and rebuild this node (local rebuild)
                var all = new int[node.IndicesCount + 1];
                Array.Copy(node.Indices!, all, node.IndicesCount);
                all[^1] = idx;
                _pool.Return(node.Indices!);
                node.Indices = null;
                node.IndicesCount = 0;
                node.Children = new Node[_childCount];

                // redistribute using BuildRecursive calls for each child
                var stayList = new List<int>();
                var childBuffers = new List<int>[_childCount];
                for (var c = 0; c < _childCount; c++) childBuffers[c] = [];

                var center = node.Box.Center;
                var size = node.Box.Size;
                var half = new double[_dimensions];
                for (var d = 0; d < _dimensions; d++) half[d] = size[d] * 0.5;
                var nodeMin = new double[_dimensions];
                var nodeMax = new double[_dimensions];
                for (var d = 0; d < _dimensions; d++) { nodeMin[d] = center[d] - half[d]; nodeMax[d] = center[d] + half[d]; }

                foreach (var id in all)
                {
                    if (_boxes[id].IsEmpty) { stayList.Add(id); continue; }

                    var pc = _boxes[id].Center;
                    var ps = _boxes[id].Size;
                    var ph = new double[_dimensions];
                    for (var d = 0; d < _dimensions; d++) ph[d] = ps[d] * 0.5;
                    var pmin = new double[_dimensions];
                    var pmax = new double[_dimensions];
                    for (var d = 0; d < _dimensions; d++) { pmin[d] = pc[d] - ph[d]; pmax[d] = pc[d] + ph[d]; }

                    var fit = -1;
                    for (var c = 0; c < _childCount; c++)
                    {
                        var fits = true;
                        for (var d = 0; d < _dimensions; d++)
                        {
                            var bit = (c >> d) & 1;
                            var cmin = bit == 0 ? nodeMin[d] : center[d];
                            var cmax = bit == 0 ? center[d] : nodeMax[d];
                            if (pmin[d] < cmin - Epsilon || pmax[d] > cmax + Epsilon) { fits = false; break; }
                        }
                        if (fits) { fit = c; break; }
                    }
                    if (fit >= 0) childBuffers[fit].Add(id);
                    else stayList.Add(id);
                }

                var subtreeCount = 0;
                for (var c = 0; c < _childCount; c++)
                {
                    var list = childBuffers[c];
                    if (list.Count == 0) { node.Children[c] = null; continue; }
                    var childArray = list.ToArray();
                    var child = BuildRecursive(node, childArray);
                    node.Children[c] = child;
                    subtreeCount += child.Count;
                }

                if (stayList.Count > 0)
                {
                    var arr = _pool.Rent(Math.Max(_leafCapacity, stayList.Count));
                    stayList.CopyTo(arr);
                    node.Indices = arr;
                    node.IndicesCount = stayList.Count;
                    for (var i = 0; i < node.IndicesCount; i++) _nodeForIndex[node.Indices[i]] = node;
                    subtreeCount += node.IndicesCount;
                }
                else
                {
                    node.Indices = _pool.Rent(_leafCapacity);
                    node.IndicesCount = 0;
                }

                node.Count = subtreeCount;
                var p2 = node.Parent;
                while (p2 != null)
                {
                    p2.Count = 0;
                    if (p2.Children != null)
                        for (var cc = 0; cc < p2.Children.Length; cc++)
                            if (p2.Children[cc] != null) p2.Count += p2.Children[cc]!.Count;
                    if (p2.Indices != null) p2.Count += p2.IndicesCount;
                    p2 = p2.Parent;
                }

                // local rebuild check
                var up = node;
                while (up != null)
                {
                    if (NeedsRebuild(up))
                    {
                        RebuildSubtree(up);
                        break;
                    }
                    up = up.Parent;
                }
            }
        }

        /// <summary>
        /// Remove an object by its index. Returns true if removed.
        /// </summary>
        public bool Remove(int index)
        {
            if (index < 0 || index >= _objects.Count) return false;
            if (_boxes[index].IsEmpty) return false;

            var leaf = _nodeForIndex[index];
            if (leaf == null || leaf.Indices == null) return false;

            var pos = Array.IndexOf(leaf.Indices, index, 0, leaf.IndicesCount);
            if (pos < 0) return false;

            // remove from leaf
            leaf.IndicesCount--;
            if (pos < leaf.IndicesCount)
                leaf.Indices[pos] = leaf.Indices[leaf.IndicesCount];

            _nodeForIndex[index] = null;
            _boxes[index] = TBox.Empty(_dimensions);

            // decrement counts upwards
            var p = leaf;
            while (p != null)
            {
                p.Count--;
                p = p.Parent;
            }

            // optionally collapse small internal nodes (if all children null and indices small)
            var parent = leaf.Parent;
            while (parent != null)
            {
                if (!parent.IsLeaf)
                {
                    // if all children null and indices small, make leaf
                    var anyChild = false;
                    var total = parent.IndicesCount;
                    for (var c = 0; c < parent.Children!.Length; c++)
                    {
                        var ch = parent.Children[c];
                        if (ch != null) { anyChild = true; total += ch.Count; }
                    }

                    if (!anyChild && total <= _leafCapacity)
                    {
                        // collect indices from this node and make leaf
                        var gather = new List<int>();
                        if (parent.IndicesCount > 0)
                        {
                            for (var i = 0; i < parent.IndicesCount; i++)
                                gather.Add(parent.Indices![i]);
                            _pool.Return(parent.Indices!);
                        }

                        var arr = _pool.Rent(Math.Max(_leafCapacity, gather.Count));
                        gather.CopyTo(arr);
                        parent.Children = null;
                        parent.Indices = arr;
                        parent.IndicesCount = gather.Count;
                        parent.Count = gather.Count;
                        for (var i = 0; i < parent.IndicesCount; i++) _nodeForIndex[parent.Indices[i]] = parent;
                    }
                }

                // maybe rebalance parent
                if (NeedsRebuild(parent))
                {
                    RebuildSubtree(parent);
                    break;
                }

                parent = parent.Parent;
            }

            return true;
        }

        /// <summary>
        /// Update object at index with a new geometry (remove & reinsert). Optionally trigger local rebuilds.
        /// </summary>
        public void Update(int index, TGeom newObj, bool localRebuild = true)
        {
            if (index < 0 || index >= _objects.Count) throw new ArgumentOutOfRangeException(nameof(index));

            // remove from current leaf if present
            var leaf = _nodeForIndex[index];
            if (leaf is { Indices: not null })
            {
                var pos = Array.IndexOf(leaf.Indices, index, 0, leaf.IndicesCount);
                if (pos >= 0)
                {
                    leaf.IndicesCount--;
                    if (pos < leaf.IndicesCount) leaf.Indices[pos] = leaf.Indices[leaf.IndicesCount];
                    _nodeForIndex[index] = null;
                }
            }

            _objects[index] = newObj;
            _boxes[index] = newObj.GetBoundingBox();

            if (!_boxes[index].IsEmpty) InsertIndex(index);

            if (localRebuild && leaf != null)
            {
                var p = leaf;
                while (p != null)
                {
                    if (NeedsRebuild(p)) { RebuildSubtree(p); break; }
                    p = p.Parent;
                }
            }
        }

        #endregion

        #region --- Queries (iterative) ---

        public void QueryCandidates(TBox query, List<int> outIndices)
        {
            outIndices.Clear();
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var n = stack.Pop();
                if (n.Box.IsEmpty || !n.Box.IntersectsBox(query)) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (!_boxes[idx].IsEmpty) outIndices.Add(idx);
                    }
                }

                if (n is { IsLeaf: false, Children: not null })
                {
                    for (var c = 0; c < n.Children.Length; c++)
                        if (n.Children[c] != null) stack.Push(n.Children[c]!);
                }
            }
        }

        public void QueryPoint(TVector point, List<int> outIndices)
        {
            outIndices.Clear();
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var n = stack.Pop();
                if (n.Box.IsEmpty || !n.Box.ContainsPoint(point)) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].ContainsPoint(point)) outIndices.Add(idx);
                    }
                }

                if (n is not { IsLeaf: false, Children: not null }) continue;
                
                for (var c = 0; c < n.Children.Length; c++)
                    if (n.Children[c] != null && n.Children[c]!.Box.ContainsPoint(point)) 
                        stack.Push(n.Children[c]!);
            }
        }

        public void QueryRay(IFloat64Ray<TVector> ray, List<int> outIndices)
        {
            outIndices.Clear();
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var n = stack.Pop();
                if (n.Box.IsEmpty || !n.Box.IntersectsRay(ray)) continue;

                // check indices stored in this node
                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].IntersectsRay(ray))
                            outIndices.Add(idx);
                    }
                }

                // push children that exist (their boxes will be tested when they pop)
                if (n is not { IsLeaf: false, Children: not null }) continue;
                
                foreach (var t in n.Children)
                    if (t != null) stack.Push(t!);
            }
        }


        public int? QueryNearest(TVector point, double maxDistance = double.PositiveInfinity)
        {
            var bestDist2 = maxDistance * maxDistance;
            int? bestIdx = null;

            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var n = stack.Pop();
                if (n.Box.IsEmpty || n.Box.DistanceSquaredToPoint(point) > bestDist2) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (_boxes[idx].IsEmpty) continue;
                        var d2 = _boxes[idx].DistanceSquaredToPoint(point);
                        if (d2 < bestDist2)
                        {
                            bestDist2 = d2;
                            bestIdx = idx;
                        }
                    }
                }

                if (n is { IsLeaf: false, Children: not null })
                {
                    var candidateChildren = new List<(Node child, double d2)>(n.Children.Length);
                    for (var c = 0; c < n.Children.Length; c++)
                    {
                        var ch = n.Children[c];
                        if (ch == null || ch.Box.IsEmpty) continue;
                        var d2 = ch.Box.DistanceSquaredToPoint(point);
                        if (d2 <= bestDist2) candidateChildren.Add((ch, d2));
                    }

                    candidateChildren.Sort((a, b) => a.d2.CompareTo(b.d2));
                    for (var i = candidateChildren.Count - 1; i >= 0; i--) stack.Push(candidateChildren[i].child);
                }
            }

            return bestIdx;
        }

        public void QueryIntersections(TBox query, List<int> outIndices)
        {
            outIndices.Clear();
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var n = stack.Pop();
                if (n.Box.IsEmpty || !n.Box.IntersectsBox(query)) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (_boxes[idx].IntersectsBox(query)) outIndices.Add(idx);
                    }
                }

                if (n is { IsLeaf: false, Children: not null })
                {
                    for (var c = 0; c < n.Children.Length; c++)
                        if (n.Children[c] != null) stack.Push(n.Children[c]!);
                }
            }
        }

        #endregion

        #region --- Refit / FullRebuild / Pools / Dispose ---

        /// <summary>
        /// Recompute bounding boxes bottom-up starting from the entire tree or a chosen node.
        /// </summary>
        public void Refit()
        {
            RefitRecursive(_root);
        }

        private void RefitRecursive(Node node)
        {
            if (node.IsLeaf)
            {
                if (node.IndicesCount == 0) node.Box = TBox.Empty(_dimensions);
                else
                {
                    var boxes = new List<TBox>(node.IndicesCount);
                    for (var i = 0; i < node.IndicesCount; i++) boxes.Add(_boxes[node.Indices![i]]);
                    node.Box = TBox.Containing(boxes);
                }
                node.Count = node.IndicesCount;
                return;
            }

            var childBoxes = new List<TBox>();
            var total = node.IndicesCount;
            if (node.IndicesCount > 0)
            {
                for (var i = 0; i < node.IndicesCount; i++) childBoxes.Add(_boxes[node.Indices![i]]);
            }

            for (var c = 0; c < node.Children!.Length; c++)
            {
                var ch = node.Children[c];
                if (ch != null)
                {
                    RefitRecursive(ch);
                    childBoxes.Add(ch.Box);
                    total += ch.Count;
                }
            }

            node.Box = childBoxes.Count > 0 ? TBox.Containing(childBoxes) : TBox.Empty(_dimensions);
            node.Count = total;
        }

        /// <summary>
        /// Full rebuild (returns all pools then rebuilds).
        /// </summary>
        public void FullRebuild()
        {
            ReturnPools();
            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            _root = BuildRecursive(null, valid);
        }

        /// <summary>
        /// Return pooled arrays used by leaves (call before discarding tree).
        /// </summary>
        public void ReturnPools()
        {
            ReturnPoolsRecursive(_root);
        }

        //private void ReturnPoolsRecursive(Node node)
        //{
        //    if (node == null) return;
        //    if (node.Indices != null)
        //    {
        //        _pool.Return(node.Indices);
        //        node.Indices = null;
        //        node.IndicesCount = 0;
        //    }
        //    if (!node.IsLeaf && node.Children != null)
        //    {
        //        for (int c = 0; c < node.Children.Length; c++)
        //            if (node.Children[c] != null) ReturnPoolsRecursive(node.Children[c]!);
        //    }
        //}

        /// <summary>
        /// Dispose returns pooled arrays automatically.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            ReturnPools();
        }

        #endregion

        #region --- Accessors ---

        public IReadOnlyList<TGeom> Objects => _objects;
        public IReadOnlyList<TBox> Boxes => _boxes;

        #endregion
    }
}

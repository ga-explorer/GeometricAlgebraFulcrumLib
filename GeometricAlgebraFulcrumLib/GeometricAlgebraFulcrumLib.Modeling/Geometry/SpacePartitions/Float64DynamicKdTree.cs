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
    public sealed class Float64DynamicKdTree4D(IEnumerable<IFloat64BoundedShape4D> shapes, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64DynamicKdTree<IFloat64BoundedShape4D, Float64BoundingBox4D, LinFloat64Vector4D>(shapes, dimensions, leafCapacity, rebuildImbalance);

    public sealed class Float64DynamicKdTree3D(IEnumerable<IFloat64BoundedShape3D> shapes, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64DynamicKdTree<IFloat64BoundedShape3D, Float64BoundingBox3D, LinFloat64Vector3D>(shapes, dimensions, leafCapacity, rebuildImbalance);

    public sealed class Float64DynamicKdTree2D(IEnumerable<IFloat64BoundedShape2D> shapes, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64DynamicKdTree<IFloat64BoundedShape2D, Float64BoundingBox2D, LinFloat64Vector2D>(shapes, dimensions, leafCapacity, rebuildImbalance);

    public sealed class Float64DynamicKdTree(IEnumerable<IFloat64BoundedShape> shapes, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        : Float64DynamicKdTree<IFloat64BoundedShape, Float64BoundingBox, LinFloat64Vector>(shapes, dimensions, leafCapacity, rebuildImbalance);

    /// <summary>
    /// Fully dynamic kd-tree with parent pointers and pooled leaf storage.
    /// Supports efficient Insert, Remove, Update, and queries.
    /// </summary>
    public class Float64DynamicKdTree<TGeom, TBox, TVector> :
        IFloat64SpacePartition<TBox, TVector>
        where TGeom : IFloat64BoundedShape<TBox>
        where TBox : IFloat64BoundingBox<TBox, TVector>
        where TVector : ILinFloat64Vector
    {
        private class Node
        {
            public TBox? Box;
            public Node? Parent;

            // Leaf storage
            public int[]? Indices;
            public int IndicesCount;

            // Internal node
            public int SplitAxis;
            public double SplitValue;
            public Node? Left;
            public Node? Right;

            public int Count;

            public bool IsLeaf => Indices != null;

            public void MakeLeaf(int[] arr, int count)
            {
                Left = Right = null;
                SplitAxis = -1;
                SplitValue = 0.0;
                Indices = arr;
                IndicesCount = count;
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
        private readonly int _dimensions;
        private readonly int _leafCapacity;
        private readonly double _rebuildImbalance;
        private Node _root;
        private readonly List<Node?> _nodeForIndex = [];
        private readonly ArrayPool<int> _pool = ArrayPool<int>.Shared;

        public Float64DynamicKdTree(IEnumerable<TGeom> objects, int dimensions, int leafCapacity = 8, double rebuildImbalance = 0.75)
        {
            _dimensions = Math.Max(1, dimensions);
            _leafCapacity = Math.Max(1, leafCapacity);
            _rebuildImbalance = Math.Clamp(rebuildImbalance, 0.55, 0.95);

            foreach (var o in objects)
            {
                _objects.Add(o);
                _boxes.Add(o.GetBoundingBox());
            }
            for (var i = 0; i < _objects.Count; i++) _nodeForIndex.Add(null);

            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            _root = BuildSubtree(valid, null);
        }

        #region --- Build Helpers ---
        private Node BuildSubtree(int[] indices, Node? parent)
        {
            var n = new Node { Parent = parent };
            BuildRecursive(n, indices);
            return n;
        }

        private void BuildRecursive(Node node, int[] indices)
        {
            node.Box = TBox.Containing(indices.Select(i => _boxes[i]));
            node.Count = indices.Length;

            if (indices.Length <= _leafCapacity)
            {
                var arr = _pool.Rent(Math.Max(_leafCapacity, indices.Length));
                Array.Copy(indices, arr, indices.Length);
                node.MakeLeaf(arr, indices.Length);
                foreach (var idx in indices) _nodeForIndex[idx] = node;
                return;
            }

            var size = node.Box.Size;
            var axis = 0; var maxLen = size[0];
            for (var d = 1; d < _dimensions; d++)
                if (size[d] > maxLen) { maxLen = size[d]; axis = d; }

            if (maxLen <= 0.0)
            {
                var arr = _pool.Rent(Math.Max(_leafCapacity, indices.Length));
                Array.Copy(indices, arr, indices.Length);
                node.MakeLeaf(arr, indices.Length);
                foreach (var idx in indices) _nodeForIndex[idx] = node;
                return;
            }

            Array.Sort(indices, (i1, i2) => _boxes[i1].Center[axis].CompareTo(_boxes[i2].Center[axis]));
            var mid = indices.Length / 2;
            if (mid == 0 || _boxes[indices[0]].Center[axis] == _boxes[indices[^1]].Center[axis])
            {
                var arr = _pool.Rent(Math.Max(_leafCapacity, indices.Length));
                Array.Copy(indices, arr, indices.Length);
                node.MakeLeaf(arr, indices.Length);
                foreach (var idx in indices) _nodeForIndex[idx] = node;
                return;
            }

            var leftIdx = indices.Take(mid).ToArray();
            var rightIdx = indices.Skip(mid).ToArray();

            node.SplitAxis = axis;
            node.SplitValue = (_boxes[leftIdx[^1]].Center[axis] + _boxes[rightIdx[0]].Center[axis]) * 0.5;

            node.Left = BuildSubtree(leftIdx, node);
            node.Right = BuildSubtree(rightIdx, node);
        }
        #endregion

        #region --- Insert ---
        public int Insert(TGeom obj)
        {
            var idx = _objects.Count;
            _objects.Add(obj);
            _boxes.Add(obj.GetBoundingBox());
            _nodeForIndex.Add(null);

            if (_boxes[idx].IsEmpty) return idx;

            var node = _root;
            while (!node.IsLeaf)
            {
                node.Count++;
                node.Box = TBox.Containing([node.Box, _boxes[idx]]);
                node = (_boxes[idx].Center[node.SplitAxis] <= node.SplitValue) ? node.Left! : node.Right!;
            }

            if (node.IndicesCount == node.Indices!.Length)
                SplitLeaf(node, idx);
            else
            {
                node.Indices[node.IndicesCount++] = idx;
                node.Count++;
                _nodeForIndex[idx] = node;
                node.Box = TBox.Containing([node.Box!, _boxes[idx]]);

                var p = node.Parent;
                while (p != null)
                {
                    p.Box = TBox.Containing([p.Box!, _boxes[idx]]);
                    p = p.Parent;
                }
            }
            return idx;
        }

        private void SplitLeaf(Node leaf, int newIdx)
        {
            var total = new int[leaf.IndicesCount + 1];
            Array.Copy(leaf.Indices!, total, leaf.IndicesCount);
            total[leaf.IndicesCount] = newIdx;
            leaf.ReleaseLeaf(_pool);
            BuildRecursive(leaf, total);
        }
        #endregion

        #region --- Remove ---
        public bool Remove(int index)
        {
            if (index < 0 || index >= _objects.Count) return false;
            if (_boxes[index].IsEmpty) return false;

            _boxes[index] = TBox.Empty(_dimensions);
            var leaf = _nodeForIndex[index];
            if (leaf == null || !leaf.IsLeaf) return false;

            var pos = Array.IndexOf(leaf.Indices!, index, 0, leaf.IndicesCount);
            if (pos < 0) return false;

            leaf.IndicesCount--;
            if (pos < leaf.IndicesCount)
                leaf.Indices![pos] = leaf.Indices[leaf.IndicesCount];

            _nodeForIndex[index] = null;
            leaf.Count--;

            RecomputeBox(leaf);
            var p = leaf.Parent;
            while (p != null)
            {
                p.Count--;
                RecomputeBox(p);
                p = p.Parent;
            }
            return true;
        }
        #endregion

        #region --- Update ---
        public void Update(int index, TGeom obj)
        {
            if (index < 0 || index >= _objects.Count) throw new ArgumentOutOfRangeException();
            _objects[index] = obj;
            _boxes[index] = obj.GetBoundingBox();

            var node = _nodeForIndex[index];
            if (node == null) return;

            RecomputeBox(node);
            var p = node.Parent;
            while (p != null)
            {
                RecomputeBox(p);
                p = p.Parent;
            }
        }
        #endregion

        #region --- Queries ---
        public void QueryCandidates(TBox query, List<int> outIndices)
        {
            var stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node.Box == null || !node.Box.IntersectsBox(query)) continue;
                if (node.IsLeaf)
                {
                    for (var i = 0; i < node.IndicesCount; i++)
                    {
                        var idx = node.Indices![i];
                        if (!_boxes[idx].IsEmpty) outIndices.Add(idx);
                    }
                }
                else
                {
                    if (node.Left != null) stack.Push(node.Left);
                    if (node.Right != null) stack.Push(node.Right);
                }
            }
        }

        public void QueryPoint(TVector point, List<int> outIndices)
        {
            var stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node.Box == null || !node.Box.ContainsPoint(point)) continue;
                if (node.IsLeaf)
                {
                    for (var i = 0; i < node.IndicesCount; i++)
                    {
                        var idx = node.Indices![i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].ContainsPoint(point))
                            outIndices.Add(idx);
                    }
                }
                else
                {
                    var axis = node.SplitAxis;
                    var coord = point[axis];
                    if (coord <= node.SplitValue)
                    {
                        if (node.Left != null) stack.Push(node.Left);
                        if (node.Right != null) stack.Push(node.Right);
                    }
                    else
                    {
                        if (node.Right != null) stack.Push(node.Right);
                        if (node.Left != null) stack.Push(node.Left);
                    }
                }
            }
        }

        public void QueryRay(IFloat64Ray<TVector> ray, List<int> outIndices)
        {
            outIndices.Clear();
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node.Box == null || !node.Box.IntersectsRay(ray)) 
                    continue;

                if (node.IsLeaf)
                {
                    for (var i = 0; i < node.IndicesCount; i++)
                    {
                        var idx = node.Indices![i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].IntersectsRay(ray))
                            outIndices.Add(idx);
                    }
                }
                else
                {
                    // push both children if present; checking box intersection above already prunes using node.Box,
                    // but children may still be null or contain empty boxes, so check for null
                    if (node.Left != null) stack.Push(node.Left);
                    if (node.Right != null) stack.Push(node.Right);
                }
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
                var node = stack.Pop();
                if (node.Box == null || node.Box.DistanceSquaredToPoint(point) > bestDist2) continue;
                if (node.IsLeaf)
                {
                    for (var i = 0; i < node.IndicesCount; i++)
                    {
                        var idx = node.Indices![i];
                        if (_boxes[idx].IsEmpty) continue;
                        var d2 = _boxes[idx].DistanceSquaredToPoint(point);
                        if (d2 < bestDist2)
                        {
                            bestDist2 = d2;
                            bestIdx = idx;
                        }
                    }
                }
                else
                {
                    var left = node.Left;
                    var right = node.Right;
                    if (left != null && right != null)
                    {
                        var dl = left.Box!.DistanceSquaredToPoint(point);
                        var dr = right.Box!.DistanceSquaredToPoint(point);
                        if (dl < dr)
                        {
                            if (dr <= bestDist2) stack.Push(right);
                            if (dl <= bestDist2) stack.Push(left);
                        }
                        else
                        {
                            if (dl <= bestDist2) stack.Push(left);
                            if (dr <= bestDist2) stack.Push(right);
                        }
                    }
                    else
                    {
                        if (left != null && left.Box!.DistanceSquaredToPoint(point) <= bestDist2) stack.Push(left);
                        if (right != null && right.Box!.DistanceSquaredToPoint(point) <= bestDist2) stack.Push(right);
                    }
                }
            }
            return bestIdx;
        }

        public void QueryIntersections(TBox query, List<int> outIndices)
        {
            var candidates = new List<int>();
            QueryCandidates(query, candidates);
            foreach (var i in candidates)
                if (_boxes[i].IntersectsBox(query)) outIndices.Add(i);
        }
        #endregion

        #region --- Utilities ---
        private void RecomputeBox(Node node)
        {
            if (node.IsLeaf)
            {
                node.Box = node.IndicesCount == 0 
                    ? TBox.Empty(_dimensions) 
                    : TBox.Containing(
                        Enumerable
                            .Range(0, node.IndicesCount)
                            .Select(i => _boxes[node.Indices![i]])
                    );
            }
            else
            {
                var boxes = new List<TBox>();

                if (node.Left is not null && node.Left.Box is not null)
                    boxes.Add(node.Left.Box);

                if (node.Right is not null && node.Right.Box is not null) 
                    boxes.Add(node.Right.Box);

                node.Box = TBox.Containing(boxes);
            }
        }

        public IReadOnlyList<TGeom> Objects => _objects;

        public IReadOnlyList<TBox> Boxes => _boxes;

        public void FullRebuild()
        {
            foreach (var n in _nodeForIndex) n?.ReleaseLeaf(_pool);
            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            _root = BuildSubtree(valid, null);
        }
        #endregion
    }
}

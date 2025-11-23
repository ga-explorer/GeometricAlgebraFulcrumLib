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
    public sealed class Float64StaticKdTree4D(IEnumerable<IFloat64BoundedShape4D> shapes, int dimensions, int leafCapacity = 8)
        : Float64StaticKdTree<IFloat64BoundedShape4D, Float64BoundingBox4D, LinFloat64Vector4D>(shapes, dimensions, leafCapacity);

    public sealed class Float64StaticKdTree3D(IEnumerable<IFloat64BoundedShape3D> shapes, int dimensions, int leafCapacity = 8)
        : Float64StaticKdTree<IFloat64BoundedShape3D, Float64BoundingBox3D, LinFloat64Vector3D>(shapes, dimensions, leafCapacity);

    public sealed class Float64StaticKdTree2D(IEnumerable<IFloat64BoundedShape2D> shapes, int dimensions, int leafCapacity = 8)
        : Float64StaticKdTree<IFloat64BoundedShape2D, Float64BoundingBox2D, LinFloat64Vector2D>(shapes, dimensions, leafCapacity);

    public sealed class Float64StaticKdTree(IEnumerable<IFloat64BoundedShape> shapes, int dimensions, int leafCapacity = 8)
        : Float64StaticKdTree<IFloat64BoundedShape, Float64BoundingBox, LinFloat64Vector>(shapes, dimensions, leafCapacity);

    /// <summary>
    /// Dimension-agnostic static kd-tree over bounded shapes.
    /// </summary>
    public class Float64StaticKdTree<TGeom, TBox, TVector> : 
        IFloat64SpacePartition<TBox, TVector>
        where TGeom : IFloat64BoundedShape<TBox>
        where TBox : IFloat64BoundingBox<TBox, TVector>
        where TVector : ILinFloat64Vector
    {
        private class Node
        {
            // Bounding box of this node (over its child shapes)
            public TBox? Box;

            // Leaf
            public int[]? Indices;

            // Internal node:
            public int SplitAxis;
            public double SplitValue;

            public Node? Left, Right;

            public bool IsLeaf => Indices != null;
        }

        private readonly List<TGeom> _objects;
        private readonly List<TBox> _boxes;
        private readonly int _dimensions;
        private readonly int _leafCapacity;

        private Node _root;

        public Float64StaticKdTree(IEnumerable<TGeom> objects, int dimensions, int leafCapacity = 8)
        {
            _dimensions = Math.Max(1, dimensions);
            _leafCapacity = Math.Max(1, leafCapacity);

            _objects = new List<TGeom>(objects);
            _boxes = _objects.Select(o => o.GetBoundingBox()).ToList();

            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            _root = BuildIterative(valid);
        }

        #region --- Build ---

        private Node BuildIterative(int[] indices)
        {
            var root = new Node();
            var stack = new Stack<(Node node, int[] idxs)>();
            stack.Push((root, indices));

            while (stack.Count > 0)
            {
                var (node, indexArray) = stack.Pop();

                // Compute node bounding box
                node.Box = TBox.Containing(indexArray.Select(i => _boxes[i]));

                // Leaf test
                if (indexArray.Length <= _leafCapacity)
                {
                    node.Indices = indexArray;
                    continue;
                }

                // Choose split axis: longest axis of node.Box.Size
                var size = node.Box.Size;
                var axis = 0;
                var maxLen = size[0];
                for (var d = 1; d < _dimensions; d++)
                {
                    if (size[d] > maxLen)
                    {
                        maxLen = size[d];
                        axis = d;
                    }
                }

                // Sort indices by box center along axis
                Array.Sort(indexArray, (i1, i2) =>
                    _boxes[i1].Center[axis].CompareTo(_boxes[i2].Center[axis]));

                // Split at median
                var mid = indexArray.Length / 2;

                // If all centers are identical along the axis, make this a leaf to avoid degenerate split
                if (mid == 0 || _boxes[indexArray[0]].Center[axis] == _boxes[indexArray[^1]].Center[axis])
                {
                    node.Indices = indexArray;
                    continue;
                }

                var leftIndices = indexArray.Take(mid).ToArray();
                var rightIndices = indexArray.Skip(mid).ToArray();

                // Split value: median center coordinate between left last and right first
                var leftMax = _boxes[leftIndices[^1]].Center[axis];
                var rightMin = _boxes[rightIndices[0]].Center[axis];
                var splitValue = (leftMax + rightMin) * 0.5;

                node.SplitAxis = axis;
                node.SplitValue = splitValue;
                node.Left = new Node();
                node.Right = new Node();

                // Push children to stack (right first so left is processed first similar to recursion)
                stack.Push((node.Right, rightIndices));
                stack.Push((node.Left, leftIndices));
            }

            return root;
        }

        /// <summary>
        /// Force rebuild the tree (useful if you changed objects externally).
        /// </summary>
        public void Rebuild()
        {
            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            _root = BuildIterative(valid);
        }

        #endregion

        #region --- Queries ---

        /// <summary>
        /// Collect candidate indices whose node bounding boxes intersect the query box.
        /// This is a conservative candidate collector; final intersection test should check individual boxes.
        /// </summary>
        public void QueryCandidates(TBox query, List<int> outIndices)
        {
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!node.Box!.IntersectsBox(query)) continue;

                if (node.IsLeaf)
                {
                    outIndices.AddRange(node.Indices!);
                }
                else
                {
                    if (node.Left != null) stack.Push(node.Left);
                    if (node.Right != null) stack.Push(node.Right);
                }
            }
        }

        /// <summary>
        /// Collect candidate indices whose node bounding boxes contain the point.
        /// </summary>
        public void QueryPoint(TVector point, List<int> outIndices)
        {
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!node.Box!.ContainsPoint(point)) continue;

                if (node.IsLeaf) outIndices.AddRange(node.Indices!);
                else
                {
                    // Heuristic: visit the child that likely contains the point first
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
                    for (var i = 0; i < node.Indices?.Length; i++)
                    {
                        var idx = node.Indices![i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].IntersectsRay(ray))
                            outIndices.Add(idx);
                    }
                }
                else
                {
                    if (node.Left != null) stack.Push(node.Left);
                    if (node.Right != null) stack.Push(node.Right);
                }
            }
        }


        /// <summary>
        /// Nearest neighbor search by bounding-box distance. Returns the index of the nearest shape
        /// (based on bounding-box distance) or null if tree empty. maxDistance optional limit.
        /// </summary>
        public int? QueryNearest(TVector point, double maxDistance = double.PositiveInfinity)
        {
            var bestDist2 = maxDistance * maxDistance;
            int? bestIdx = null;

            // Stack for traversal
            var stack = new Stack<Node>();
            stack.Push(_root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                // Prune by node box distance
                if (node.Box!.DistanceSquaredToPoint(point) > bestDist2) continue;

                if (node.IsLeaf)
                {
                    foreach (var i in node.Indices!)
                    {
                        var d2 = _boxes[i].DistanceSquaredToPoint(point);
                        if (d2 < bestDist2)
                        {
                            bestDist2 = d2;
                            bestIdx = i;
                        }
                    }
                }
                else
                {
                    // Visit nearer child first (by box distance)
                    var left = node.Left;
                    var right = node.Right;

                    if (left == null && right == null) continue;

                    if (left != null && right != null)
                    {
                        var dl = left.Box!.DistanceSquaredToPoint(point);
                        var dr = right.Box!.DistanceSquaredToPoint(point);

                        // push the farther first so nearer is processed next
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
                    else if (left != null)
                    {
                        if (left.Box!.DistanceSquaredToPoint(point) <= bestDist2) stack.Push(left);
                    }
                    else if (right != null)
                    {
                        if (right.Box!.DistanceSquaredToPoint(point) <= bestDist2) stack.Push(right);
                    }
                }
            }

            return bestIdx;
        }

        /// <summary>
        /// Query shapes whose boxes actually intersect the query box (returns filtered indices).
        /// </summary>
        public void QueryIntersections(TBox query, List<int> outIndices)
        {
            var candidates = new List<int>();
            QueryCandidates(query, candidates);

            foreach (var i in candidates)
            {
                if (_boxes[i].IntersectsBox(query)) outIndices.Add(i);
            }
        }

        #endregion

        #region --- Utilities / Accessors ---

        public IReadOnlyList<TGeom> Objects => _objects;
        public IReadOnlyList<TBox> Boxes => _boxes;

        #endregion
    }
}

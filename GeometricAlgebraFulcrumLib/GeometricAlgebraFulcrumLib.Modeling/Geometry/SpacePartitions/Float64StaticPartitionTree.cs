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
    // Convenience wrappers
    public sealed class Float64StaticPartitionTree2D(IEnumerable<IFloat64BoundedShape2D> shapes, int leafCapacity = 8, bool assignByCenter = false)
        : Float64StaticPartitionTree<IFloat64BoundedShape2D, Float64BoundingBox2D, LinFloat64Vector2D>(shapes, 2, leafCapacity, assignByCenter) { }

    public sealed class Float64StaticPartitionTree3D(IEnumerable<IFloat64BoundedShape3D> shapes, int leafCapacity = 8, bool assignByCenter = false)
        : Float64StaticPartitionTree<IFloat64BoundedShape3D, Float64BoundingBox3D, LinFloat64Vector3D>(shapes, 3, leafCapacity, assignByCenter) { }

    public sealed class Float64StaticPartitionTree4D(IEnumerable<IFloat64BoundedShape4D> shapes, int leafCapacity = 8, bool assignByCenter = false)
        : Float64StaticPartitionTree<IFloat64BoundedShape4D, Float64BoundingBox4D, LinFloat64Vector4D>(shapes, 4, leafCapacity, assignByCenter) { }

    public sealed class Float64StaticPartitionTree(IEnumerable<IFloat64BoundedShape> shapes, int dimensions, int leafCapacity = 8, bool assignByCenter = false)
        : Float64StaticPartitionTree<IFloat64BoundedShape, Float64BoundingBox, LinFloat64Vector>(shapes, Math.Max(1, dimensions), leafCapacity, assignByCenter) { }

    /// <summary>
    /// Dimension-agnostic static partition tree (quadtree, octree, 16-ary, …).
    /// Stores nodes in a flattened array for cache-friendly queries.
    /// </summary>
    public class Float64StaticPartitionTree<TGeom, TBox, TVector> : 
        IFloat64SpacePartition<TBox, TVector>, 
        IDisposable
        where TGeom : IFloat64BoundedShape<TBox>
        where TBox : IFloat64BoundingBox<TBox, TVector>
        where TVector : ILinFloat64Vector
    {
        #region Node struct
        private struct Node
        {
            public TBox Box;
            public int[]? Indices;     // pooled
            public int IndicesCount;
            public int FirstChild;     // index into flat array (-1 if leaf)
            public int ChildCount;

            public bool IsLeaf => FirstChild < 0;
        }
        #endregion

        private readonly List<TGeom> _objects;
        private readonly List<TBox> _boxes;
        private readonly int _dimensions;
        private readonly int _childCount;
        private readonly int _leafCapacity;
        private readonly bool _assignByCenter;
        private readonly ArrayPool<int> _pool = ArrayPool<int>.Shared;

        private Node[] _nodes = [];
        private int _rootIndex;
        private bool _disposed;

        public Float64StaticPartitionTree(IEnumerable<TGeom> shapes, int dimensions, int leafCapacity = 8, bool assignByCenter = false)
        {
            _dimensions = Math.Max(1, dimensions);
            _childCount = 1 << _dimensions;
            _leafCapacity = Math.Max(1, leafCapacity);
            _assignByCenter = assignByCenter;

            _objects = new List<TGeom>(shapes);
            _boxes = _objects.Select(o => o.GetBoundingBox()).ToList();

            var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
            if (valid.Length == 0)
            {
                _nodes = new Node[1];
                _nodes[0].Box = TBox.Empty(_dimensions);
                _nodes[0].Indices = _pool.Rent(_leafCapacity);
                _nodes[0].IndicesCount = 0;
                _nodes[0].FirstChild = -1;
                _nodes[0].ChildCount = 0;
                _rootIndex = 0;
            }
            else
            {
                BuildFlat(valid);
            }
        }
        #region --- Flat builder ---
        private class BuildTask
        {
            public int ParentIndex;
            public int ChildSlot;
            public int[] Indices;
            public TBox Box;
        }

        private void BuildFlat(int[] rootIndices)
        {
            var rootBox = TBox.Containing(rootIndices.Select(i => _boxes[i]));
            var nodeList = new List<Node> {
                // create root
                new Node { Box = rootBox } };

            _rootIndex = 0;

            var stack = new Stack<BuildTask>();
            stack.Push(new BuildTask
            {
                ParentIndex = -1,
                ChildSlot = -1,
                Indices = rootIndices,
                Box = rootBox
            });

            while (stack.Count > 0)
            {
                var task = stack.Pop();
                var currentIndex = nodeList.Count - 1; // last added node
                var node = nodeList[currentIndex];
                node.Box = task.Box;

                // leaf?
                if (task.Indices.Length <= _leafCapacity)
                {
                    var arr = _pool.Rent(Math.Max(_leafCapacity, task.Indices.Length));
                    Array.Copy(task.Indices, arr, task.Indices.Length);
                    node.Indices = arr;
                    node.IndicesCount = task.Indices.Length;
                    node.FirstChild = -1;
                    node.ChildCount = 0;
                    nodeList[currentIndex] = node;
                    continue;
                }

                // subdivide
                var childrenIndices = new List<int>[_childCount];
                for (var c = 0; c < _childCount; c++) childrenIndices[c] = [];

                var stay = new List<int>();

                var center = task.Box.Center;
                var size = task.Box.Size;
                var half = new double[_dimensions];
                for (var d = 0; d < _dimensions; d++) half[d] = size[d] * 0.5;
                var nodeMin = new double[_dimensions];
                var nodeMax = new double[_dimensions];
                for (var d = 0; d < _dimensions; d++)
                {
                    nodeMin[d] = center[d] - half[d];
                    nodeMax[d] = center[d] + half[d];
                }

                foreach (var idx in task.Indices)
                {
                    if (_boxes[idx].IsEmpty) { stay.Add(idx); continue; }

                    if (_assignByCenter)
                    {
                        var pc = _boxes[idx].Center;
                        var c = 0;
                        for (var d = 0; d < _dimensions; d++)
                            if (pc[d] >= center[d]) c |= 1 << d;
                        childrenIndices[c].Add(idx);
                    }
                    else
                    {
                        var pc = _boxes[idx].Center;
                        var ps = _boxes[idx].Size;
                        var ph = new double[_dimensions];
                        for (var d = 0; d < _dimensions; d++) ph[d] = ps[d] * 0.5;

                        var pmin = new double[_dimensions];
                        var pmax = new double[_dimensions];
                        for (var d = 0; d < _dimensions; d++)
                        {
                            pmin[d] = pc[d] - ph[d];
                            pmax[d] = pc[d] + ph[d];
                        }

                        var fit = -1;
                        for (var c = 0; c < _childCount; c++)
                        {
                            var fits = true;
                            for (var d = 0; d < _dimensions; d++)
                            {
                                var bit = (c >> d) & 1;
                                var cmin = bit == 0 ? nodeMin[d] : center[d];
                                var cmax = bit == 0 ? center[d] : nodeMax[d];
                                if (pmin[d] < cmin - 1e-12 || pmax[d] > cmax + 1e-12) { fits = false; break; }
                            }
                            if (fits) { fit = c; break; }
                        }
                        if (fit >= 0) childrenIndices[fit].Add(idx);
                        else stay.Add(idx);
                    }
                }

                // finalize node
                if (stay.Count > 0)
                {
                    var arr = _pool.Rent(Math.Max(_leafCapacity, stay.Count));
                    stay.CopyTo(arr);
                    node.Indices = arr;
                    node.IndicesCount = stay.Count;
                }
                else
                {
                    var arr = _pool.Rent(_leafCapacity);
                    node.Indices = arr;
                    node.IndicesCount = 0;
                }

                node.FirstChild = nodeList.Count;
                node.ChildCount = _childCount;
                nodeList[currentIndex] = node;

                // enqueue children
                for (var c = 0; c < _childCount; c++)
                {
                    if (childrenIndices[c].Count == 0)
                    {
                        nodeList.Add(new Node
                        {
                            Box = default!,
                            Indices = null,
                            IndicesCount = 0,
                            FirstChild = -1,
                            ChildCount = 0
                        });
                        continue;
                    }

                    var childBox = TBox.Containing(childrenIndices[c].Select(i => _boxes[i]));
                    nodeList.Add(new Node { Box = childBox });
                    stack.Push(new BuildTask
                    {
                        ParentIndex = currentIndex,
                        ChildSlot = c,
                        Indices = childrenIndices[c].ToArray(),
                        Box = childBox
                    });
                }
            }

            _nodes = nodeList.ToArray();
        }
        #endregion
        #region --- Queries (flat traversal) ---

        /// <summary>Collect candidate indices whose node boxes intersect the query.</summary>
        public void QueryCandidates(TBox query, List<int> outIndices)
        {
            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];
                if (!n.Box.IntersectsBox(query)) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                        outIndices.Add(n.Indices[i]);
                }

                if (!n.IsLeaf)
                {
                    for (var c = 0; c < n.ChildCount; c++)
                        stack.Push(n.FirstChild + c);
                }
            }
        }

        /// <summary>Return indices of objects whose boxes contain the point.</summary>
        public void QueryPoint(TVector point, List<int> outIndices)
        {
            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];
                if (!n.Box.ContainsPoint(point)) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].ContainsPoint(point))
                            outIndices.Add(idx);
                    }
                }

                if (!n.IsLeaf)
                {
                    for (var c = 0; c < n.ChildCount; c++)
                        stack.Push(n.FirstChild + c);
                }
            }
        }

        public void QueryRay(IFloat64Ray<TVector> ray, List<int> outIndices)
        {
            outIndices.Clear();
            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                if (n.Box.IsEmpty || !n.Box.IntersectsRay(ray))
                    continue;

                // Check stored indices
                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (!_boxes[idx].IsEmpty && _boxes[idx].IntersectsRay(ray))
                            outIndices.Add(idx);
                    }
                }

                // Traverse children if internal node
                if (n.IsLeaf) continue;

                for (var c = 0; c < n.ChildCount; c++)
                    stack.Push(n.FirstChild + c);
            }
        }
        /// <summary>
        /// Collect object indices intersected by the ray, sorted by intersection distance.
        /// Optionally limits the number of returned intersections (default 1).
        /// Useful for nearest-hit queries in ray tracing.
        /// </summary>
        public void QueryRaySorted(IFloat64Ray<TVector> ray, List<(int Index, double T)> outHits, int maxIntersections = 1)
        {
            outHits.Clear();
            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                if (n.Box.IsEmpty) continue;

                // Get ray-box entry distance
                if (!n.Box.TryIntersectRay(ray, out var tEnter, out var tExit))
                    continue;

                // Process leaf objects
                if (n.IndicesCount > 0 && n.Indices != null)
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (_boxes[idx].IsEmpty) continue;

                        if (_boxes[idx].TryIntersectRay(ray, out var tObjEnter, out var tObjExit))
                        {
                            if (tObjExit >= 0) // intersects in front of ray origin
                                outHits.Add((idx, Math.Max(0, tObjEnter)));
                        }
                    }
                }

                // Traverse children
                if (!n.IsLeaf)
                {
                    for (var c = 0; c < n.ChildCount; c++)
                        stack.Push(n.FirstChild + c);
                }
            }

            // Sort hits by ray parameter (nearest first)
            outHits.Sort((a, b) => a.T.CompareTo(b.T));

            // Truncate if maxIntersections requested
            if (maxIntersections > 0 && outHits.Count > maxIntersections)
                outHits.RemoveRange(maxIntersections, outHits.Count - maxIntersections);
        }



        /// <summary>Nearest neighbor search by bounding-box distance.</summary>
        public int? QueryNearest(TVector point, double maxDistance = double.PositiveInfinity)
        {
            var bestDist2 = maxDistance * maxDistance;
            int? bestIdx = null;
            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];
                if (n.Box.DistanceSquaredToPoint(point) > bestDist2) continue;

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

                if (!n.IsLeaf)
                {
                    var candidates = new List<(int ni, double d2)>(n.ChildCount);
                    for (var c = 0; c < n.ChildCount; c++)
                    {
                        var ci = n.FirstChild + c;
                        ref var child = ref _nodes[ci];
                        var d2 = child.Box.DistanceSquaredToPoint(point);
                        if (d2 <= bestDist2) candidates.Add((ci, d2));
                    }
                    candidates.Sort((a, b) => a.d2.CompareTo(b.d2));
                    for (var i = candidates.Count - 1; i >= 0; i--)
                        stack.Push(candidates[i].ni);
                }
            }

            return bestIdx;
        }

        /// <summary>Return indices of objects whose boxes actually intersect the query box.</summary>
        public void QueryIntersections(TBox query, List<int> outIndices)
        {
            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];
                if (!n.Box.IntersectsBox(query)) continue;

                if (n is { IndicesCount: > 0, Indices: not null })
                {
                    for (var i = 0; i < n.IndicesCount; i++)
                    {
                        var idx = n.Indices[i];
                        if (_boxes[idx].IntersectsBox(query))
                            outIndices.Add(idx);
                    }
                }

                if (!n.IsLeaf)
                {
                    for (var c = 0; c < n.ChildCount; c++)
                        stack.Push(n.FirstChild + c);
                }
            }
        }

        #endregion

        #region --- Dispose ---
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            foreach (ref var n in _nodes.AsSpan())
            {
                if (n.Indices != null) _pool.Return(n.Indices);
                n.Indices = null;
            }
            _nodes = [];
        }
        #endregion

        #region --- Bulk Queries ---

        /// <summary>
        /// Query many bounding boxes at once.
        /// Each query index in 'queries' corresponds to one result list in 'results'.
        /// </summary>
        public void BulkQueryCandidates(IReadOnlyList<TBox> queries, List<int>[] results)
        {
            if (results.Length != queries.Count)
                throw new ArgumentException("Results array length must match queries count.");

            for (var i = 0; i < results.Length; i++)
                results[i].Clear();

            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                // test against all queries
                for (var qi = 0; qi < queries.Count; qi++)
                {
                    if (!n.Box.IntersectsBox(queries[qi])) continue;

                    if (n is { IndicesCount: > 0, Indices: not null })
                    {
                        for (var j = 0; j < n.IndicesCount; j++)
                            results[qi].Add(n.Indices[j]);
                    }

                    if (!n.IsLeaf)
                    {
                        for (var c = 0; c < n.ChildCount; c++)
                            stack.Push(n.FirstChild + c);
                    }
                }
            }
        }

        /// <summary>
        /// Query many points at once: returns indices of objects containing each point.
        /// </summary>
        public void BulkQueryPoints(IReadOnlyList<TVector> points, List<int>[] results)
        {
            if (results.Length != points.Count)
                throw new ArgumentException("Results array length must match points count.");

            for (var i = 0; i < results.Length; i++)
                results[i].Clear();

            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                for (var pi = 0; pi < points.Count; pi++)
                {
                    var p = points[pi];
                    if (!n.Box.ContainsPoint(p)) continue;

                    if (n is { IndicesCount: > 0, Indices: not null })
                    {
                        for (var j = 0; j < n.IndicesCount; j++)
                        {
                            var idx = n.Indices[j];
                            if (!_boxes[idx].IsEmpty && _boxes[idx].ContainsPoint(p))
                                results[pi].Add(idx);
                        }
                    }

                    if (!n.IsLeaf)
                    {
                        for (var c = 0; c < n.ChildCount; c++)
                            stack.Push(n.FirstChild + c);
                    }
                }
            }
        }

        /// <summary>
        /// Bulk nearest neighbor queries for multiple points.
        /// Returns an array of object indices (null if none found).
        /// </summary>
        public int?[] BulkQueryNearest(IReadOnlyList<TVector> points, double maxDistance = double.PositiveInfinity)
        {
            var bestDist2 = new double[points.Count];
            var bestIdx = new int?[points.Count];
            for (var i = 0; i < points.Count; i++)
            {
                bestDist2[i] = maxDistance * maxDistance;
                bestIdx[i] = null;
            }

            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                for (var pi = 0; pi < points.Count; pi++)
                {
                    var p = points[pi];
                    if (n.Box.DistanceSquaredToPoint(p) > bestDist2[pi]) continue;

                    if (n is { IndicesCount: > 0, Indices: not null })
                    {
                        for (var j = 0; j < n.IndicesCount; j++)
                        {
                            var idx = n.Indices[j];
                            if (_boxes[idx].IsEmpty) continue;
                            var d2 = _boxes[idx].DistanceSquaredToPoint(p);
                            if (d2 < bestDist2[pi])
                            {
                                bestDist2[pi] = d2;
                                bestIdx[pi] = idx;
                            }
                        }
                    }

                    if (!n.IsLeaf)
                    {
                        for (var c = 0; c < n.ChildCount; c++)
                            stack.Push(n.FirstChild + c);
                    }
                }
            }

            return bestIdx;
        }

        
        /// <summary>
        /// Bulk nearest neighbor queries for multiple points.
        /// Caller provides result buffers (GC-free).
        /// - bestIdx[i] will contain the nearest object index for point[i], or -1 if none.
        /// - bestDist2[i] will contain the squared distance to that nearest object.
        /// </summary>
        public void BulkQueryNearest(
            IReadOnlyList<TVector> points,
            double maxDistance,
            int[] bestIdx,
            double[] bestDist2
        )
        {
            if (bestIdx.Length != points.Count || bestDist2.Length != points.Count)
                throw new ArgumentException("Result arrays must match query count.");

            // Initialize buffers
            for (var i = 0; i < points.Count; i++)
            {
                bestIdx[i] = -1;
                bestDist2[i] = maxDistance * maxDistance;
            }

            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                for (var pi = 0; pi < points.Count; pi++)
                {
                    var p = points[pi];
                    if (n.Box.DistanceSquaredToPoint(p) > bestDist2[pi]) continue;

                    if (n is { IndicesCount: > 0, Indices: not null })
                    {
                        for (var j = 0; j < n.IndicesCount; j++)
                        {
                            var idx = n.Indices[j];
                            if (_boxes[idx].IsEmpty) continue;

                            var d2 = _boxes[idx].DistanceSquaredToPoint(p);
                            if (d2 < bestDist2[pi])
                            {
                                bestDist2[pi] = d2;
                                bestIdx[pi] = idx;
                            }
                        }
                    }

                    if (!n.IsLeaf)
                    {
                        for (var c = 0; c < n.ChildCount; c++)
                            stack.Push(n.FirstChild + c);
                    }
                }
            }
        }

        /// <summary>
        /// Bulk intersection queries for multiple boxes (GC-free).
        /// Caller provides results array of lists (one per query box).
        /// Each results[i] will be cleared and filled with indices of objects intersecting queries[i].
        /// </summary>
        public void BulkQueryIntersections(
            IReadOnlyList<TBox> queries,
            List<int>[] results
        )
        {
            if (results.Length != queries.Count)
                throw new ArgumentException("Results array length must match query count.");

            // Clear result lists
            for (var i = 0; i < results.Length; i++)
                results[i].Clear();

            var stack = new Stack<int>();
            stack.Push(_rootIndex);

            while (stack.Count > 0)
            {
                var ni = stack.Pop();
                ref var n = ref _nodes[ni];

                for (var qi = 0; qi < queries.Count; qi++)
                {
                    var q = queries[qi];
                    if (!n.Box.IntersectsBox(q)) continue;

                    if (n is { IndicesCount: > 0, Indices: not null })
                    {
                        for (var j = 0; j < n.IndicesCount; j++)
                        {
                            var idx = n.Indices[j];
                            if (_boxes[idx].IntersectsBox(q))
                                results[qi].Add(idx);
                        }
                    }

                    if (!n.IsLeaf)
                    {
                        for (var c = 0; c < n.ChildCount; c++)
                            stack.Push(n.FirstChild + c);
                    }
                }
            }
        }

        #endregion

        public IReadOnlyList<TGeom> Objects => _objects;
        public IReadOnlyList<TBox> Boxes => _boxes;
    }
}

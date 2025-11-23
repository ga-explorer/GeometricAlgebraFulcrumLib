using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Rays;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.SpacePartitions;

public sealed class Float64Bih2D(IEnumerable<IFloat64BoundedShape2D> shapes, int leafCapacity = 8)
    : Float64Bih<IFloat64BoundedShape2D, Float64BoundingBox2D, LinFloat64Vector2D>(shapes, 2, leafCapacity);

public sealed class Float64Bih3D(IEnumerable<IFloat64BoundedShape3D> shapes, int leafCapacity = 8)
    : Float64Bih<IFloat64BoundedShape3D, Float64BoundingBox3D, LinFloat64Vector3D>(shapes, 3, leafCapacity);

public sealed class Float64Bih4D(IEnumerable<IFloat64BoundedShape4D> shapes, int leafCapacity = 8)
    : Float64Bih<IFloat64BoundedShape4D, Float64BoundingBox4D, LinFloat64Vector4D>(shapes, 4, leafCapacity);

public sealed class Float64Bih(IEnumerable<IFloat64BoundedShape> shapes, int dimensions, int leafCapacity = 8)
    : Float64Bih<IFloat64BoundedShape, Float64BoundingBox, LinFloat64Vector>(shapes, dimensions, leafCapacity);

public class Float64Bih<TGeom, TBox, TVector> :
    IFloat64SpacePartition<TBox, TVector>
    where TGeom   : IFloat64BoundedShape<TBox>
    where TBox    : IFloat64BoundingBox<TBox, TVector>
    where TVector : ILinFloat64Vector
{
    private class Node
    {
        public TBox? Box;
        public int[]? Indices;
        public Node? Left, Right;
    }

    private readonly List<TGeom> _objects;
    private readonly List<TBox> _boxes;
    private Node _root;
    private readonly int _leafCapacity;
    private readonly int _dimensions;

    public Float64Bih(IEnumerable<TGeom> objects, int dimensions, int leafCapacity = 8)
    {
        _objects = new List<TGeom>(objects);
        _boxes   = _objects.Select(o => o.GetBoundingBox()).ToList();
        _leafCapacity = Math.Max(1, leafCapacity);
        _dimensions = dimensions;
        _root = BuildIterative(
            Enumerable
                .Range(0, _objects.Count)
                .Where(i => !_boxes[i].IsEmpty)
                .ToArray()
        );
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
            var box = TBox.Containing(indexArray.Select(i => _boxes[i]));
            node.Box = box;

            if (indexArray.Length <= _leafCapacity)
            {
                node.Indices = indexArray;
                continue;
            }

            var size = box.Size;
            var axis = 0;
            var maxLen = size[0];
            for (var d = 1; d < _dimensions; d++)
                if (size[d] > maxLen) { maxLen = size[d]; axis = d; }

            Array.Sort(indexArray, (i1, i2) => _boxes[i1].Center[axis].CompareTo(_boxes[i2].Center[axis]));

            var mid = indexArray.Length / 2;
            node.Left  = new Node();
            node.Right = new Node();
            stack.Push((node.Right, indexArray.Skip(mid).ToArray()));
            stack.Push((node.Left,  indexArray.Take(mid).ToArray()));
        }

        return root;
    }

    public void Rebuild()
    {
        var valid = Enumerable.Range(0, _objects.Count).Where(i => !_boxes[i].IsEmpty).ToArray();
        _root = BuildIterative(valid);
    }

    public void Refit()
    {
        var stack = new Stack<Node>();
        var postOrder = new Stack<Node>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            var n = stack.Pop();
            postOrder.Push(n);
            if (n.Left != null) stack.Push(n.Left);
            if (n.Right != null) stack.Push(n.Right);
        }

        while (postOrder.Count > 0)
        {
            var n = postOrder.Pop();

            n.Box = TBox.Containing(
                n.Indices != null 
                    ? n.Indices.Select(i => _boxes[i]) 
                    : [n.Left!.Box!, n.Right!.Box!]
            );
        }
    }
    #endregion

    #region --- Dynamic Ops ---
    public int Insert(TGeom obj, bool refitInsteadOfRebuild = false)
    {
        var idx = _objects.Count;
        _objects.Add(obj);
        _boxes.Add(obj.GetBoundingBox());
        if (refitInsteadOfRebuild) Refit(); else Rebuild();
        return idx;
    }

    public void Remove(int index, bool refitInsteadOfRebuild = false)
    {
        _boxes[index] = TBox.Empty(_dimensions);
        if (refitInsteadOfRebuild) Refit(); else Rebuild();
    }

    public void Update(int index, TGeom obj, bool refitInsteadOfRebuild = true)
    {
        _objects[index] = obj;
        _boxes[index] = obj.GetBoundingBox();
        if (refitInsteadOfRebuild) Refit(); else Rebuild();
    }
    #endregion

    #region --- Queries (Iterative) ---
    public void QueryCandidates(TBox query, List<int> outIndices)
    {
        var stack = new Stack<Node>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (!node.Box!.IntersectsBox(query)) continue;

            if (node.Indices != null) outIndices.AddRange(node.Indices);
            else
            {
                if (node.Left  != null) stack.Push(node.Left);
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
            if (!node.Box!.ContainsPoint(point)) continue;

            if (node.Indices != null) outIndices.AddRange(node.Indices);
            else
            {
                if (node.Left  != null) stack.Push(node.Left);
                if (node.Right != null) stack.Push(node.Right);
            }
        }
    }

    public void QueryRay(IFloat64Ray<TVector> ray, List<int> outIndices)
    {
        var stack = new Stack<Node>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (!node.Box!.IntersectsRay(ray)) continue;

            if (node.Indices != null) outIndices.AddRange(node.Indices);
            else
            {
                if (node.Left  != null) stack.Push(node.Left);
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
            if (node.Box!.DistanceSquaredToPoint(point) > bestDist2) continue;

            if (node.Indices != null)
            {
                foreach (var i in node.Indices)
                {
                    var d2 = _boxes[i].DistanceSquaredToPoint(point);

                    if (!(d2 < bestDist2)) continue;

                    bestDist2 = d2; 
                    bestIdx = i;
                }
            }
            else
            {
                if (node.Left  != null) stack.Push(node.Left);
                if (node.Right != null) stack.Push(node.Right);
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
}

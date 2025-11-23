using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Trees.Space2D;

public sealed class BinaryTree2D<T> where T : class, ISpatialPrimitive2D
{
    readonly Node _root;
    public int Count { get; private set; }

    public BinaryTree2D(Bounds2 worldBounds, int maxDepth = 16)
    {
        _root = new Node(worldBounds, 0, maxDepth);
    }

    public bool Insert(T item)
    {
        if (!_root.Bounds.Intersects(item.Bounds)) return false;
        var inserted = _root.Insert(item);
        if (inserted) Count++;
        return inserted;
    }

    public void QueryRange(Bounds2 range, List<T> results)
    {
        _root.QueryRange(range, results);
    }

    public void QueryRangeVisit(Bounds2 range, Action<T> visitor)
    {
        _root.QueryRangeVisit(range, visitor);
    }

    public int QueryRange(Bounds2 range, Span<T> output)
    {
        var count = 0;
        _root.QueryRangeSpan(range, output, ref count);
        return count;
    }

    public T Nearest(Vector2 point)
    {
        var bestDist = float.PositiveInfinity;
        T best = null;

        var stack = new Stack<Node>();
        stack.Push(_root);

        while (stack.Count > 0)
        {
            var n = stack.Pop();
            var nodeDist = n.Bounds.DistanceSquaredToPoint(point);
            if (nodeDist > bestDist) continue;

            for (var i = 0; i < n.Items.Count; i++)
            {
                var it = n.Items[i];
                var d2 = it.DistanceSquared(point);
                if (d2 < bestDist)
                {
                    bestDist = d2;
                    best = it;
                }
            }

            if (n.HasChildren)
            {
                stack.Push(n.Left);
                stack.Push(n.Right);
            }
        }

        return best;
    }

    sealed class Node
    {
        public Bounds2 Bounds { get; }
        public readonly List<T> Items;
        public Node Left;
        public Node Right;
        public bool HasChildren => Left != null;
        readonly int _depth;
        readonly int _maxDepth;

        public Node(Bounds2 bounds, int depth, int maxDepth)
        {
            Bounds = bounds;
            Items = new List<T>();
            _depth = depth;
            _maxDepth = maxDepth;
        }

        public bool Insert(T item)
        {
            if (_depth >= _maxDepth)
            {
                Items.Add(item);
                return true;
            }

            if (!HasChildren)
                Subdivide();

            if (Left.Bounds.Contains(item.Bounds.Center()))
                return Left.Insert(item);
            else if (Right.Bounds.Contains(item.Bounds.Center()))
                return Right.Insert(item);

            Items.Add(item);
            return true;
        }

        public void QueryRange(Bounds2 range, List<T> results)
        {
            if (!Bounds.Intersects(range)) return;
            foreach (var it in Items)
                if (range.Intersects(it.Bounds)) results.Add(it);
            if (HasChildren)
            {
                Left.QueryRange(range, results);
                Right.QueryRange(range, results);
            }
        }

        public void QueryRangeVisit(Bounds2 range, Action<T> visitor)
        {
            if (!Bounds.Intersects(range)) return;
            foreach (var it in Items)
                if (range.Intersects(it.Bounds)) visitor(it);
            if (HasChildren)
            {
                Left.QueryRangeVisit(range, visitor);
                Right.QueryRangeVisit(range, visitor);
            }
        }

        public void QueryRangeSpan(Bounds2 range, Span<T> outSpan, ref int count)
        {
            if (!Bounds.Intersects(range)) return;
            foreach (var it in Items)
            {
                if (range.Intersects(it.Bounds))
                {
                    if (count < outSpan.Length)
                        outSpan[count++] = it;
                }
            }
            if (HasChildren)
            {
                Left.QueryRangeSpan(range, outSpan, ref count);
                Right.QueryRangeSpan(range, outSpan, ref count);
            }
        }

        void Subdivide()
        {
            var ext = Bounds.Extents;
            var c = Bounds.Center;
            var splitX = ext.X >= ext.Y;

            if (splitX)
            {
                var leftB = new Bounds2(Bounds.Min, new Vector2(c.X, Bounds.Max.Y));
                var rightB = new Bounds2(new Vector2(c.X, Bounds.Min.Y), Bounds.Max);
                Left = new Node(leftB, _depth + 1, _maxDepth);
                Right = new Node(rightB, _depth + 1, _maxDepth);
            }
            else
            {
                var bottomB = new Bounds2(Bounds.Min, new Vector2(Bounds.Max.X, c.Y));
                var topB = new Bounds2(new Vector2(Bounds.Min.X, c.Y), Bounds.Max);
                Left = new Node(bottomB, _depth + 1, _maxDepth);
                Right = new Node(topB, _depth + 1, _maxDepth);
            }
        }
    }
}
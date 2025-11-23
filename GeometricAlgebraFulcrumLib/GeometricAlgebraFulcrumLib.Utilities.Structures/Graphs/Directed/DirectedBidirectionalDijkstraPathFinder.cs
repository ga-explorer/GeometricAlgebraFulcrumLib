namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed
{
    /// <summary>
    /// Computes the shortest path between two nodes in a directed graph
    /// using Bidirectional Dijkstra's algorithm.
    /// 
    /// Characteristics:
    /// - Runs Dijkstra simultaneously from the source and the target until searches meet.
    /// - Often faster than standard Dijkstra, especially for large sparse graphs.
    /// - Time complexity: approximately half of Dijkstra, O((V + E) log V / 2).
    /// - Space complexity: O(V).
    /// 
    /// Recommended when solving single-source single-target queries on large directed graphs.
    /// </summary>
    public class DirectedBidirectionalDijkstraPathFinder<TNode>(DirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        DirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            var forwardPq = new PriorityQueue<TNode, double>();
            var backwardPq = new PriorityQueue<TNode, double>();

            var forwardDist = new Dictionary<TNode, double> { [start] = 0 };
            var backwardDist = new Dictionary<TNode, double> { [goal] = 0 };

            var forwardParent = new Dictionary<TNode, TNode>();
            var backwardParent = new Dictionary<TNode, TNode>();

            forwardPq.Enqueue(start, 0);
            backwardPq.Enqueue(goal, 0);

            var best = double.PositiveInfinity;
            TNode? meeting = default;

            while (forwardPq.Count > 0 && backwardPq.Count > 0)
            {
                if (Step(forwardPq, forwardDist, forwardParent, backwardDist, true, ref best, ref meeting)) break;
                if (Step(backwardPq, backwardDist, backwardParent, forwardDist, false, ref best, ref meeting)) break;
            }

            if (meeting is null) 
                return ([], double.PositiveInfinity);

            return (Reconstruct(forwardParent, backwardParent, start, goal, meeting), best);
        }

        private bool Step(
            PriorityQueue<TNode, double> pq,
            Dictionary<TNode, double> dist,
            Dictionary<TNode, TNode> parent,
            Dictionary<TNode, double> otherDist,
            bool forward,
            ref double best,
            ref TNode? meeting)
        {
            if (pq.Count == 0) return false;

            var current = pq.Dequeue();
            var d = dist[current];

            var neighbors = forward
                ? Graph.GetNodeWeightedOutNeighbors(current, EdgeWeightFunc)
                : Graph.GetNodeWeightedInNeighbors(current, EdgeWeightFunc);

            foreach (var (w, neighbor) in neighbors)
            {
                var nd = d + w;

                if (!dist.TryGetValue(neighbor, out var old) || nd < old)
                {
                    dist[neighbor] = nd;
                    parent[neighbor] = current;
                    pq.Enqueue(neighbor, nd);
                }

                if (otherDist.TryGetValue(neighbor, out var od))
                {
                    var total = nd + od;
                    if (total < best)
                    {
                        best = total;
                        meeting = neighbor;
                    }
                }
            }

            return false;
        }

        private List<TNode> Reconstruct(
            Dictionary<TNode, TNode> forwardParent,
            Dictionary<TNode, TNode> backwardParent,
            TNode start, TNode goal, TNode meeting)
        {
            var path = new List<TNode>();

            var stack = new Stack<TNode>();
            var cur = meeting;
            while (!Graph.NodeComparer.Equals(cur, start) && forwardParent.ContainsKey(cur))
            {
                stack.Push(cur);
                cur = forwardParent[cur];
            }
            stack.Push(start);
            while (stack.Count > 0) path.Add(stack.Pop());

            cur = meeting;
            while (!Graph.NodeComparer.Equals(cur, goal) && backwardParent.ContainsKey(cur))
            {
                cur = backwardParent[cur];
                path.Add(cur);
            }

            return path;
        }
    }
}

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes the shortest path between two nodes using Bidirectional Dijkstra's algorithm.
    /// 
    /// Characteristics:
    /// - Runs Dijkstra simultaneously from the source and the target until searches meet.
    /// - Often faster than standard Dijkstra, especially for large sparse graphs.
    /// - Time complexity: roughly O((V + E) log V / 2) in practice.
    /// - Space complexity: O(V).
    /// 
    /// Recommended when solving single-source single-target queries on large graphs.
    /// </summary>
    public class UndirectedBidirectionalDijkstraPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        UndirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            var forwardPq = new PriorityQueue<TNode, double>();
            var backwardPq = new PriorityQueue<TNode, double>();
            var forwardDist = new Dictionary<TNode, double>(Graph.Count) { [start] = 0 };
            var backwardDist = new Dictionary<TNode, double>(Graph.Count) { [goal] = 0 };
            var forwardParent = new Dictionary<TNode, TNode>();
            var backwardParent = new Dictionary<TNode, TNode>();

            forwardPq.Enqueue(start, 0);
            backwardPq.Enqueue(goal, 0);

            var best = double.PositiveInfinity;
            TNode? meeting = default;

            while (forwardPq.Count > 0 && backwardPq.Count > 0)
            {
                if (Step(forwardPq, forwardDist, forwardParent, backwardDist, ref best, ref meeting)) break;
                if (Step(backwardPq, backwardDist, backwardParent, forwardDist, ref best, ref meeting)) break;
            }

            if (meeting is null) return (new List<TNode>(), double.PositiveInfinity);

            var path = Reconstruct(forwardParent, backwardParent, start, goal, meeting);
            return (path, best);
        }

        private bool Step(
            PriorityQueue<TNode, double> pq,
            Dictionary<TNode, double> dist,
            Dictionary<TNode, TNode> parent,
            Dictionary<TNode, double> otherDist,
            ref double best,
            ref TNode? meeting)
        {
            if (pq.Count == 0) return false;

            var current = pq.Dequeue();
            var d = dist[current];

            foreach (var (w, neighbor) in Graph.GetNodeWeightedNeighbors(current, EdgeWeightFunc))
            {
                var nd = d + w;
                if (!dist.TryGetValue(neighbor, out var old) || nd < old)
                {
                    dist[neighbor] = nd;
                    parent[neighbor] = current;
                    pq.Enqueue(neighbor, nd);
                }

                if (otherDist.TryGetValue(neighbor, out var otherD))
                {
                    var total = nd + otherD;
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
            Dictionary<TNode, TNode> parentF,
            Dictionary<TNode, TNode> parentB,
            TNode start, TNode goal, TNode meeting)
        {
            var path = new List<TNode>();

            var stack = new Stack<TNode>();
            var cur = meeting;
            while (!Graph.NodeComparer.Equals(cur, start) && parentF.ContainsKey(cur))
            {
                stack.Push(cur);
                cur = parentF[cur];
            }
            stack.Push(start);
            while (stack.Count > 0) path.Add(stack.Pop());

            cur = meeting;
            while (!Graph.NodeComparer.Equals(cur, goal) && parentB.ContainsKey(cur))
            {
                cur = parentB[cur];
                path.Add(cur);
            }

            return path;
        }
    }
}

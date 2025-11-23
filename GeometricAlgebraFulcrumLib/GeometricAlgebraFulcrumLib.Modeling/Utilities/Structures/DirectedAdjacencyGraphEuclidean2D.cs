using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Utilities.Structures
{
    public class DirectedAdjacencyGraphEuclidean2D :
        DirectedAdjacencyGraph<LinFloat64Vector2D>
    {
        public static double DefaultDistanceFunction(LinFloat64Vector2D p1, LinFloat64Vector2D p2)
            => p1.GetDistanceToPoint(p2);


        
        public static DirectedAdjacencyGraphEuclidean2D Create()
        {
            return new DirectedAdjacencyGraphEuclidean2D();
        }

        public static DirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer)
        {
            return new DirectedAdjacencyGraphEuclidean2D(comparer);
        }
        
        public static DirectedAdjacencyGraphEuclidean2D Create(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> edgeList)
        {
            var graph = new DirectedAdjacencyGraphEuclidean2D();
            graph.AddEdges(edgeList);
            return graph;
        }

        public static DirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer, IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> edgeList)
        {
            var graph = new DirectedAdjacencyGraphEuclidean2D(comparer);
            graph.AddEdges(edgeList);
            return graph;
        }
        
        public static DirectedAdjacencyGraphEuclidean2D Create(params (LinFloat64Vector2D, LinFloat64Vector2D)[] edgeList)
        {
            var graph = new DirectedAdjacencyGraphEuclidean2D();
            graph.AddEdges(edgeList);
            return graph;
        }

        public static DirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer, params (LinFloat64Vector2D, LinFloat64Vector2D)[] edgeList)
        {
            var graph = new DirectedAdjacencyGraphEuclidean2D(comparer);
            graph.AddEdges(edgeList);
            return graph;
        }


        private DirectedAdjacencyGraphEuclidean2D()
            : base(LinFloat64Vector2D.DefaultToleranceComparer)
        {
        }

        private DirectedAdjacencyGraphEuclidean2D(IEqualityComparer<LinFloat64Vector2D> comparer) 
            : base(comparer)
        {
        }

        
        public LinFloat64Vector2D GetNearestNode(LinFloat64Vector2D node)
        {
            return base.GetNearestNode(
                node,
                DefaultDistanceFunction
            );
        }
        
        public Pair<LinFloat64Vector2D> GetNearestNodePair(LinFloat64Vector2D node1, LinFloat64Vector2D node2)
        {
            return new Pair<LinFloat64Vector2D>(
                GetNearestNode(node1), 
                GetNearestNode(node2)
            );
        }
        
        public Pair<LinFloat64Vector2D> GetNearestNodePair(IPair<LinFloat64Vector2D> nodePair)
        {
            return new Pair<LinFloat64Vector2D>(
                GetNearestNode(nodePair.Item1), 
                GetNearestNode(nodePair.Item2)
            );
        }

        public Pair<LinFloat64Vector2D> GetNearestNodePair((LinFloat64Vector2D, LinFloat64Vector2D) nodePair)
        {
            return new Pair<LinFloat64Vector2D>(
                GetNearestNode(nodePair.Item1), 
                GetNearestNode(nodePair.Item2)
            );
        }

    }
}

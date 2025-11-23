using GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Utilities.Structures
{
    public class UndirectedAdjacencyGraphEuclidean2D :
        UndirectedAdjacencyGraph<LinFloat64Vector2D>
    {
        public static double DefaultDistanceFunction(LinFloat64Vector2D p1, LinFloat64Vector2D p2)
            => p1.GetDistanceToPoint(p2);

        
        public static UndirectedAdjacencyGraphEuclidean2D Create()
        {
            return new UndirectedAdjacencyGraphEuclidean2D();
        }

        public static UndirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer)
        {
            return new UndirectedAdjacencyGraphEuclidean2D(comparer);
        }
        
        public static UndirectedAdjacencyGraphEuclidean2D Create(IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D();

            graph.AddEdges(edgeList);

            return graph;
        }

        public static UndirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer, IEnumerable<(LinFloat64Vector2D, LinFloat64Vector2D)> edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D(comparer);

            graph.AddEdges(edgeList);

            return graph;
        }
        
        public static UndirectedAdjacencyGraphEuclidean2D Create(params (LinFloat64Vector2D, LinFloat64Vector2D)[] edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D();

            graph.AddEdges(edgeList);

            return graph;
        }
        
        public static UndirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer, params (LinFloat64Vector2D, LinFloat64Vector2D)[] edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D(comparer);

            graph.AddEdges(edgeList);

            return graph;
        }

        public static UndirectedAdjacencyGraphEuclidean2D Create(IEnumerable<IPair<LinFloat64Vector2D>> edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D();

            graph.AddEdges(edgeList);

            return graph;
        }
        
        public static UndirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer, IEnumerable<IPair<LinFloat64Vector2D>> edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D(comparer);

            graph.AddEdges(edgeList);

            return graph;
        }
        
        public static UndirectedAdjacencyGraphEuclidean2D Create(LinFloat64Vector2D.ToleranceComparer comparer, IEnumerable<Float64LineSegment2D> edgeList)
        {
            var graph = new UndirectedAdjacencyGraphEuclidean2D(comparer);

            graph.AddEdges(edgeList.Cast<IPair<LinFloat64Vector2D>>());

            return graph;
        }


        private UndirectedAdjacencyGraphEuclidean2D()
            : base(LinFloat64Vector2D.DefaultToleranceComparer)
        {
        }

        private UndirectedAdjacencyGraphEuclidean2D(IEqualityComparer<LinFloat64Vector2D> comparer)
            : base(comparer)
        {
        }

        
        public (bool, LinFloat64Vector2D) TryGetNearestPoint(LinFloat64Vector2D point, Float64LineSegment2DBvh obstacles, double safetyDistance)
        {
            var pointList =
                Nodes.Where(p2 => 
                    obstacles.GetShortestDistanceToSegment(point, p2) > safetyDistance
                );

            var minDistance = double.PositiveInfinity;
            var point2 = point;
            foreach (var p2 in pointList)
            {
                var distance = obstacles.GetShortestDistanceToSegment(point, p2);

                if (minDistance > distance)
                {
                    minDistance = distance;
                    point2 = p2;
                } 
            }

            return (minDistance.IsFinite(), point2);
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

        
        public UndirectedAStarPathFinderEuclidean2D GetAStarPathFinder()
        {
            return new UndirectedAStarPathFinderEuclidean2D(this);
        }

        public UndirectedAStarWithLandmarksPathFinder<LinFloat64Vector2D> GetAStarWithLandmarksPathFinder(int landmarkCount)
        {
            var pathFinder = new UndirectedAStarWithLandmarksPathFinder<LinFloat64Vector2D>(
                this,
                DefaultDistanceFunction
            );

            pathFinder.Preprocess(landmarkCount);

            return pathFinder;
        }

        public UndirectedBidirectionalAStarPathFinder<LinFloat64Vector2D> GetBidirectionalAStarPathFinder()
        {
            return new UndirectedBidirectionalAStarPathFinder<LinFloat64Vector2D>(
                this,
                DefaultDistanceFunction,
                DefaultDistanceFunction
            );
        }

        public UndirectedBidirectionalDijkstraPathFinder<LinFloat64Vector2D> GetBidirectionalDijkstraPathFinder()
        {
            return new UndirectedBidirectionalDijkstraPathFinder<LinFloat64Vector2D>(
                this,
                DefaultDistanceFunction
            );
        }

        public UndirectedContractionHierarchiesPathFinder<LinFloat64Vector2D> GetContractionHierarchiesPathFinder()
        {
            var pathFinder = new UndirectedContractionHierarchiesPathFinder<LinFloat64Vector2D>(
                this,
                DefaultDistanceFunction
            );

            pathFinder.Preprocess();

            return pathFinder;
        }

        public UndirectedDijkstraPathFinder<LinFloat64Vector2D> GetDijkstraPathFinder()
        {
            return new UndirectedDijkstraPathFinder<LinFloat64Vector2D>(
                this,
                DefaultDistanceFunction
            );
        }

        public UndirectedHubLabelingPathFinder<LinFloat64Vector2D> GetHubLabelingPathFinder()
        {
            var pathFinder = new UndirectedHubLabelingPathFinder<LinFloat64Vector2D>(
                this,
                DefaultDistanceFunction
            );

            pathFinder.Preprocess();

            return pathFinder;
        }

    }
}
